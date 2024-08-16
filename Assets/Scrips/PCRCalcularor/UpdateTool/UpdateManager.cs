using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace PCRCaculator.Update
{
  public class UpdateManager : MonoBehaviour
  {
    public InputField verCharaJP, verBossJP, verBossCN;
    public Toggle qa, jp, newab;
    public Button prodUpdateButton, jpBossIsShow;
    private bool isProdUpdated = false;
    public GameObject jpBossPanel;
    private const string URL = "https://redive.estertion.win/last_version_cn.json";
    public void StartCheck()
    {
      this.gameObject.SetActive(true);

      verCharaJP.text = MainManager.Instance.Version.CharacterVersionJP.ToString();
      verBossJP.text = MainManager.Instance.Version.BossVersionJP.ToString();
      verBossCN.text = MainManager.Instance.Version.BossVersionCN.ToString();
      qa.isOn = MainManager.Instance.Version.useQA;
      jp.isOn = MainManager.Instance.Version.useJP;
      newab.isOn = MainManager.Instance.Version.newAB;

    }

    public void CancelButton()
    {
      gameObject.SetActive(false);
      prodUpdateButton.GetComponentInChildren<Text>().text = "获取";
      prodUpdateButton.interactable = true;
      isProdUpdated = false;

    }

    public void UpdateButton()
    {
      MainManager.Instance.Version.useQA = qa.isOn;
      MainManager.Instance.Version.useJP = jp.isOn;
      MainManager.Instance.Version.newAB = newab.isOn;
      MainManager.Instance.Version.CharacterVersionJP = long.Parse(verCharaJP.text);
      MainManager.Instance.Version.BossVersionJP = long.Parse(verBossJP.text);
      MainManager.Instance.Version.BossVersionCN = long.Parse(verBossCN.text);

      SaveManager.Save(MainManager.Instance.Version);
      /*
      string path = Application.streamingAssetsPath + "/../.ABExt2";
      if (Application.platform == RuntimePlatform.Android)
      {
          path = Application.persistentDataPath + "/AB";
      }

      Directory.Delete(path, true);
      Directory.CreateDirectory(path);
      */

      gameObject.SetActive(false);
      MainManager.Instance.WindowConfigMessage("更新完成，重启摸轴器生效", Application.Quit, Application.Quit);
    }

    async void ProdUpdateButton()
    {
      if (!isProdUpdated)
      {
        await ProdUpdate();
      }
      else
      {
        verBossCN.text = prodUpdateButton.GetComponentInChildren<Text>().text;
        qa.isOn = false;
        prodUpdateButton.GetComponentInChildren<Text>().text = "已覆盖版本号并取消QA";
        prodUpdateButton.interactable = false;
      }
    }
    async Task ProdUpdate()
    {
      prodUpdateButton.GetComponentInChildren<Text>().text = "正在获取...";
      prodUpdateButton.interactable = false;

        try
        {
          using (var handler = new UnityHttpClientHandler())
          {
            using (var httpClient = new HttpClient())
            {
              HttpResponseMessage response = await httpClient.GetAsync(URL);
              if (response.IsSuccessStatusCode)
              {
                string jsonString = await response.Content.ReadAsStringAsync();
                Debug.Log("JSON Response: " + jsonString);
                var json = JsonUtility.FromJson<VersionInfo>(jsonString);
                Debug.Log("TruthVersion: " + json.TruthVersion);

                prodUpdateButton.GetComponentInChildren<Text>().text = json.TruthVersion;
                prodUpdateButton.interactable = true;
                isProdUpdated = true;
              }
              else
              {
                // Show results as text
                Debug.Log("Request failed with status code: " + (int)response.StatusCode);
                prodUpdateButton.GetComponentInChildren<Text>().text = "获取失败（重试）";
                prodUpdateButton.interactable = true;
                // Parse JSON here if needed, or update UI elements.
              }
            }
          }
        }
        catch (Exception e)
        {
          Debug.LogError("Error: " + e.Message);
          prodUpdateButton.GetComponentInChildren<Text>().text = "获取失败（重试）";
          prodUpdateButton.interactable = true;
        }
    }
    public void jpBossIsShowButton()
    {
      jpBossPanel.SetActive(!jpBossPanel.activeSelf);
    }
  }

  public class UnityHttpClientHandler : HttpClientHandler
  {
    public UnityHttpClientHandler()
    {
      // 设置自定义的证书验证委托
      ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
    }
  }
  [Serializable]
  public class VersionInfo
  {
    public string TruthVersion;
  }
}