using System;
using System.IO;
using System.Threading.Tasks;
using PCRCaculator.DataSources;
using PCRCaculator.SQL;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Update
{
    /// <summary>
    /// 数据更新面板。国服数据库已移除：这里的版本号都指向日服 master 数据库。
    /// 「更新」按钮保持原语义：把面板上的版本号写进配置并提示重启；
    /// 「下载最新数据库」按钮负责按所选来源下载日服数据库，下载完成后把
    /// 角色数据版本与 Boss 底层版本一起改成刚下载的版本。
    /// </summary>
    public class UpdateManager : MonoBehaviour
    {
        public InputField verCharaJP, verBossJP, verBossCN;
        public Toggle qa, jp, newab, useLatestProd;
        public Button prodUpdateButton, jpBossIsShow;
        private bool isProdUpdated = false;
        public GameObject jpBossPanel;

        private Button downloadButton;
        private Text downloadButtonText;
        private Button sourceButtonCiallo;
        private Button sourceButtonRoboninon;
        private Button sourceButtonTaiwan;
        private int lookupGeneration;
        private string RegionName => selectedMirror == (int)JpDatabaseMirror.Taiwan ? "台服" : "日服";
        private readonly AssetDownloadUtil.ByteProgress downloadProgress = new AssetDownloadUtil.ByteProgress();
        private bool downloading;
        private RectTransform contentRoot;
        private int selectedMirror = (int)JpDatabaseMirror.CialloWorld;
        private string statusMessage = string.Empty;

        public void StartCheck()
        {
            this.gameObject.SetActive(true);

            // 部分控件（三个 Toggle、是否qa）本身就挂在「行」上，它们的父节点就是内容容器；
            // 隐藏行时要避免把整个内容容器关掉。先记住内容容器，再隐藏多余的行。
            contentRoot = RowOf(verBossCN)?.parent as RectTransform;

            // 角色数据版本、Boss 底层版本、以及下面三个开关都不再暴露，只保留日服数据库版本与下载入口。
            HideRow(verCharaJP);
            HideRow(verBossJP);
            HideRow(jp);
            HideRow(newab);
            HideRow(useLatestProd);
            jpBossPanel.SetActive(false);

            var version = MainManager.Instance != null ? MainManager.Instance.Version : null;
            if (version != null)
            {
                selectedMirror = version.JpDatabaseSource;
                lookupGeneration++;
                statusMessage = string.Empty;
                if (verBossCN != null) verBossCN.text = version.DatabaseVersion.ToString();

                // 隐藏的控件仍要回填，保存时才有正确的值。
                if (jp != null) jp.isOn = version.useJP;
                if (newab != null) newab.isOn = version.newAB;
                if (useLatestProd != null) useLatestProd.isOn = version.useLatestProd;
            }
            if (qa != null)
            {
                qa.isOn = false;
                qa.gameObject.SetActive(false);
            }

            RelayoutContent();
            EnsureDownloadUi();
            RefreshSourceButtons();
            RefreshSourceLabels();
            RefreshDownloadLabel();

            isProdUpdated = false;
            if (prodUpdateButton != null)
            {
                prodUpdateButton.GetComponentInChildren<Text>().text = "获取";
                prodUpdateButton.interactable = true;
            }
        }

        public void CancelButton()
        {
            RefreshSourceButtons();
            gameObject.SetActive(false);
            prodUpdateButton.GetComponentInChildren<Text>().text = "获取";
            prodUpdateButton.interactable = true;
            isProdUpdated = false;
        }

        /// <summary>
        /// 原「更新」语义：把面板上的日服数据库版本写进配置，提示重启生效。
        /// </summary>
        public void UpdateButton()
        {
            if (downloading) return;
            long databaseVersion;
            if (!TryParseVersion(verBossCN, out databaseVersion, RegionName + "数据库版本")) return;

            var version = MainManager.Instance.Version;
            version.JpDatabaseSource = selectedMirror;
            version.DatabaseVersion = databaseVersion;
            if (jp != null) version.useJP = jp.isOn;
            if (newab != null) version.newAB = newab.isOn;
            version.useLatestProd = false;
            SaveManager.Save(version);

            RefreshSourceButtons();
            gameObject.SetActive(false);
            Debug.Log($"更新已保存: 数据库={databaseVersion}, 来源={selectedMirror}");
            MainManager.Instance.WindowConfigMessage("更新完成，重启摸轴器生效", Application.Quit, Application.Quit);
        }

        /// <summary>
        /// 「获取」：点一次把日服最新数据库版本号显示在按钮上，再点一次覆盖到日服数据库版本输入框。
        /// </summary>
        public async void ProdUpdateButton()
        {
            if (downloading)
                return;

            var buttonText = prodUpdateButton.GetComponentInChildren<Text>();
            int generation = ++lookupGeneration;
            if (!isProdUpdated)
            {
                buttonText.text = "获取中...";
                prodUpdateButton.interactable = false;
                try
                {
                    var latestVersion = await JpDatabaseDownloader.FetchLatestVersionAsync((JpDatabaseMirror)selectedMirror);
                    if (generation != lookupGeneration) return;
                    buttonText.text = latestVersion.ToString();
                    SetStatus($"{RegionName}最新数据库版本号：{latestVersion}");
                }
                catch (Exception exception)
                {
                    if (generation != lookupGeneration) return;
                    Debug.LogError($"获取{RegionName}最新版本号失败: {exception}");
                    buttonText.text = "获取失败";
                    SetStatus("获取最新版本号失败：" + exception.Message);
                    return;
                }
                finally
                {
                    if (generation == lookupGeneration) prodUpdateButton.interactable = !downloading;
                }

                isProdUpdated = true;
            }
            else
            {
                verBossCN.text = buttonText.text;
                buttonText.text = "已覆盖版本号";
                prodUpdateButton.interactable = false;
            }
        }

        public void jpBossIsShowButton()
        {
            jpBossPanel.transform.SetAsLastSibling();
            jpBossPanel.SetActive(!jpBossPanel.activeSelf);
        }

        private void Update()
        {
            if (!downloading)
                return;

            RefreshDownloadLabel();
        }

        // ---------------- 下载最新数据库 ----------------

        public void DownloadLatestButton()
        {
            if (!downloading)
                _ = BeginDownloadLatestAsync();
        }

        private void OnSourcePicked(int mirror)
        {
            if (downloading) return;
            bool changingRegion = (mirror == (int)JpDatabaseMirror.Taiwan) != (selectedMirror == (int)JpDatabaseMirror.Taiwan);
            if (changingRegion)
            {
                var saved = MainManager.Instance.Version;
                long previous = mirror == (int)JpDatabaseMirror.Taiwan ? saved.TwDatabaseVersion : saved.JpDatabaseVersion;
                verBossCN.text = previous > 0 ? previous.ToString() : string.Empty;
            }
            lookupGeneration++;
            isProdUpdated = false;
            SetText(prodUpdateButton, "获取");
            prodUpdateButton.interactable = true;
            selectedMirror = mirror;
            statusMessage = string.Empty;
            RefreshSourceButtons();
            RefreshSourceLabels();
            RefreshDownloadLabel();
        }

        private async Task BeginDownloadLatestAsync()
        {
            var mirror = (JpDatabaseMirror)selectedMirror;
            long latestVersion;
            lookupGeneration++;
            downloadProgress.Reset();
            downloading = true;
            SetInteractable(false);
            try
            {
                statusMessage = $"正在获取{RegionName}最新数据库版本号...";
                RefreshDownloadLabel();
                latestVersion = await JpDatabaseDownloader.FetchLatestVersionAsync(mirror);
            }
            catch (Exception exception)
            {
                Debug.LogError($"获取{RegionName}最新版本号失败: {exception}");
                SetStatus("获取最新版本号失败：" + exception.Message);
                return;
            }
            finally
            {
                downloading = false;
                SetInteractable(true);
                RefreshDownloadLabel();
            }

            var destination = SQLiteTool.ResolveSourceDatabasePath(new JpDatabaseManager(latestVersion, mirror));
            if (!string.IsNullOrEmpty(destination) && File.Exists(destination))
            {
                var existingLength = new FileInfo(destination).Length;
                MainManager.Instance.WindowConfigMessage(
                    $"{RegionName}数据库 {latestVersion} 已经存在（{FormatMegabytes(existingLength)}），是否继续下载并覆盖？",
                    () => _ = DownloadAndApplyAsync(latestVersion, mirror),
                    null);
                return;
            }

            await DownloadAndApplyAsync(latestVersion, mirror);
        }

        private async Task DownloadAndApplyAsync(long version, JpDatabaseMirror mirror)
        {
            if (downloading)
                return;

            var manager = new JpDatabaseManager(version, mirror);
            var destination = SQLiteTool.ResolveSourceDatabasePath(manager);
            if (string.IsNullOrEmpty(destination))
            {
                SetStatus("无法确定数据库保存路径。");
                return;
            }

            var directory = Path.GetDirectoryName(destination);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            var staging = $"{destination}.download-{Guid.NewGuid():N}.tmp";
            downloadProgress.Reset();
            downloading = true;
            SetInteractable(false);
            RefreshDownloadLabel();
            try
            {
                var bytes = await Task.Run(() => AssetDownloadUtil.RunSync(
                    () => JpDatabaseDownloader.DownloadAsync(mirror, version, downloadProgress)));
                File.WriteAllBytes(staging, bytes);

                if (File.Exists(destination))
                    File.Replace(staging, destination, null);
                else
                    File.Move(staging, destination);
                Debug.Log($"数据库已保存: {destination} ({bytes.Length} 字节)");

                var settings = MainManager.Instance.Version;
                settings.JpDatabaseSource = (int)mirror;
                settings.DatabaseVersion = version;
                settings.useLatestProd = false;
                SaveManager.Save(settings);

                if (verBossCN != null) verBossCN.text = version.ToString();

                statusMessage = $"数据库 {version} 下载完成（{FormatMegabytes(bytes.Length)}），版本号已更新";
                MainManager.Instance.WindowConfigMessage(
                    $"{RegionName}数据库 {version} 下载完成，重启摸轴器生效",
                    Application.Quit,
                    null);
            }
            catch (Exception exception)
            {
                Debug.LogError($"下载{RegionName}数据库失败: {exception}");
                statusMessage = "下载失败：" + exception.Message;
                try
                {
                    if (File.Exists(staging))
                        File.Delete(staging);
                }
                catch (Exception cleanupException)
                {
                    Debug.LogWarning($"清理日服数据库临时文件失败: {cleanupException.Message}");
                }
            }
            finally
            {
                downloading = false;
                SetInteractable(true);
                RefreshDownloadLabel();
            }
        }

        // ---------------- 运行时 UI ----------------

        /// <summary>
        /// 面板重排：角色/Boss 两行版本号与三个开关都已隐藏，只剩「日服数据库版本」「日服数据库最新版本号」「下载最新数据库」，
        /// 把它们按内容高度垂直居中排布（内容容器 480×480，anchor 居中）。
        /// </summary>
        private void RelayoutContent()
        {
            var versionRow = RowOf(verBossCN);
            var refreshRow = RowOf(prodUpdateButton);
            if (versionRow == null || refreshRow == null)
                return;

            // 可见内容共三行（版本、最新版本号、下载按钮），整体中心对齐 y=0。
            const float firstRowY = 100f;
            const float rowPitch = 50f;

            SetRowPosition(versionRow, firstRowY);
            SetRowPosition(refreshRow, firstRowY - rowPitch);
        }

        private static RectTransform RowOf(Component control)
        {
            if (control == null)
                return null;

            return control.transform.parent as RectTransform;
        }

        private static void SetRowPosition(RectTransform row, float y)
        {
            var position = row.anchoredPosition;
            row.anchoredPosition = new Vector2(position.x, y);
        }

        /// <summary>
        /// 场景里没有现成的下载控件，这里复制「获取」按钮来做：
        /// 位置基准取「日服数据库最新版本号」那一行（它的父级就是面板内容容器），
        /// 主按钮放在该行下面一行，两个来源项做成贴在下方的下拉列表。
        /// </summary>
        private void EnsureDownloadUi()
        {
            if (downloadButton != null)
                return;

            var refreshRect = prodUpdateButton.GetComponent<RectTransform>();
            var refreshRow = refreshRect != null ? refreshRect.parent as RectTransform : null;
            var container = refreshRow != null ? refreshRow.parent : null;
            if (container == null)
                return;

            if (prodUpdateButton.GetComponentInChildren<Text>() == null)
                return;

            var rowPosition = refreshRow.anchoredPosition;
            var rowSize = refreshRow.sizeDelta;
            const float rowPitch = 50f;
            const float menuPitch = 34f;

            downloadButton = CloneButton(container, "JpDatabaseDownloadButton",
                new Vector2(rowPosition.x, rowPosition.y - rowPitch),
                new Vector2(rowSize.x, rowSize.y), DownloadLatestButton);
            downloadButtonText = downloadButton != null ? downloadButton.GetComponentInChildren<Text>() : null;

            sourceButtonCiallo = CloneButton(container, "JpDatabaseSourceSonder",
                new Vector2(rowPosition.x, rowPosition.y - rowPitch - menuPitch),
                new Vector2(rowSize.x, rowSize.y),
                () => OnSourcePicked((int)JpDatabaseMirror.CialloWorld));
            sourceButtonRoboninon = CloneButton(container, "JpDatabaseSourceRoboninon",
                new Vector2(rowPosition.x, rowPosition.y - rowPitch - menuPitch * 2f),
                new Vector2(rowSize.x, rowSize.y),
                () => OnSourcePicked((int)JpDatabaseMirror.Roboninon));

            SetText(sourceButtonCiallo, "来源1 CialloWorld（日文文本）");
            sourceButtonTaiwan = CloneButton(container, "TaiwanDatabaseSource",
                new Vector2(rowPosition.x, rowPosition.y - rowPitch - menuPitch * 3f),
                new Vector2(rowSize.x, rowSize.y), () => OnSourcePicked((int)JpDatabaseMirror.Taiwan));
            SetText(sourceButtonRoboninon, "来源2 roboninon（英文文本）");
            sourceButtonCiallo.gameObject.SetActive(true);
            sourceButtonRoboninon.gameObject.SetActive(true);
        }

        private Button CloneButton(Transform parent, string name, Vector2 position, Vector2 size, Action onClick)
        {
            var buttonObject = Instantiate(prodUpdateButton.gameObject, parent, false);
            buttonObject.name = name;
            PlaceRect(buttonObject.GetComponent<RectTransform>(), position, size);

            var button = buttonObject.GetComponent<Button>();
            if (button != null)
            {
#if UNITY_5_3_OR_NEWER
                button.onClick = new Button.ButtonClickedEvent();
#else
                button.onClick.RemoveAllListeners();
#endif
                if (onClick != null)
                    button.onClick.AddListener(() => onClick());
            }

            var text = buttonObject.GetComponentInChildren<Text>();
            if (text != null)
                text.text = string.Empty;
            return button;
        }

        private static void SetText(Button button, string message)
        {
            if (button == null)
                return;

            var text = button.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = message;
                MakeSingleLine(text);
            }
        }

        private void RefreshSourceButtons()
        {
            if (sourceButtonTaiwan != null)
                sourceButtonTaiwan.interactable = !downloading && selectedMirror != (int)JpDatabaseMirror.Taiwan;
            if (sourceButtonCiallo != null)
                sourceButtonCiallo.interactable = !downloading && selectedMirror != (int)JpDatabaseMirror.CialloWorld;
            if (sourceButtonRoboninon != null)
                sourceButtonRoboninon.interactable = !downloading && selectedMirror != (int)JpDatabaseMirror.Roboninon;
        }

        private void RefreshSourceLabels()
        {
            SetText(sourceButtonTaiwan, selectedMirror == (int)JpDatabaseMirror.Taiwan ? "已选台服 CialloWorld" : "台服 CialloWorld");
            SetText(sourceButtonCiallo, selectedMirror == (int)JpDatabaseMirror.CialloWorld
                ? "已选日服 CialloWorld（日文文本）"
                : "日服 CialloWorld（日文文本）");
            SetText(sourceButtonRoboninon, selectedMirror == (int)JpDatabaseMirror.Roboninon
                ? "已选日服 roboninon（英文文本）"
                : "日服 roboninon（英文文本）");
        }

        private void RefreshDownloadLabel()
        {
            if (downloadButtonText == null)
                return;

            if (downloading)
            {
                var progress = downloadProgress;
                downloadButtonText.text = progress.HasTotal && progress.Total > 0
                    ? $"下载中 {FormatMegabytes(progress.Received)} / {FormatMegabytes(progress.Total)}"
                      + $" ({progress.Fraction * 100f:F0}%)"
                    : $"下载中 {FormatMegabytes(progress.Received)}";
                MakeSingleLine(downloadButtonText);
                return;
            }

            downloadButtonText.text = string.IsNullOrEmpty(statusMessage)
                ? $"下载{RegionName}最新数据库"
                : statusMessage;
            MakeSingleLine(downloadButtonText);
        }

        /// <summary>
        /// 让克隆出来的按钮文字保持单行（服务器工程里的 Text 桩没有这些成员，故用条件编译隔离）。
        /// </summary>
        private static void MakeSingleLine(Text text)
        {
            if (text == null)
                return;
#if UNITY_5_3_OR_NEWER
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 10;
            text.resizeTextMaxSize = text.fontSize > 0 ? text.fontSize : 24;
#endif
        }

        private void SetStatus(string message)
        {
            statusMessage = message ?? string.Empty;
            RefreshDownloadLabel();
        }

        private void SetInteractable(bool interactable)
        {
            RefreshSourceButtons();
            if (prodUpdateButton != null)
                prodUpdateButton.interactable = interactable;
            if (downloadButton != null)
                downloadButton.interactable = interactable;
        }

        /// <summary>
        /// 隐藏控件所在整行（行里既有标题也有控件）。
        /// </summary>
        private void HideRow(Component control)
        {
            if (control == null)
                return;

            var parent = control.transform.parent;
            // 控件本身挂在「行」上时父节点是内容容器，此时隐藏控件自己；
            // 控件是行里的子物体时隐藏整行（行里还有标题）。
            Transform target = parent != null && contentRoot != null && ReferenceEquals(parent, contentRoot)
                ? control.transform
                : parent;
            if (target != null)
                target.gameObject.SetActive(false);
        }

        private static bool TryParseVersion(InputField field, out long value, string label)
        {
            value = 0;
            if (field == null)
                return true;

            if (long.TryParse(field.text.Trim(), out value) && value > 0)
                return true;

            MainManager.Instance.WindowConfigMessage($"{label}必须是正整数！", null, null);
            return false;
        }

        private static void PlaceRect(RectTransform rect, Vector2 position, Vector2 size)
        {
            if (rect == null)
                return;

            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;
            rect.localScale = Vector3.one;
        }

        private static string FormatMegabytes(long bytes)
        {
            if (bytes <= 0)
                return "0 MB";
            return $"{bytes / 1024f / 1024f:F1} MB";
        }
    }
}
