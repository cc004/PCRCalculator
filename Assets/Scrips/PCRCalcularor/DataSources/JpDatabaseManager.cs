using System;
using PCRCaculator.SQL;

namespace PCRCaculator.DataSources
{
    /// <summary>
    /// 日服 master 数据库来源：从镜像下载「已反哈希」的 sqlite。
    /// 落地文件名 jpdb{ver}.db，不再需要 Rainbow 反哈希。
    /// </summary>
    public sealed class JpDatabaseManager : IDataBaseManager
    {
        private long version;

        public JpDatabaseManager(long version, JpDatabaseMirror mirror)
        {
            this.version = version;
            Mirror = mirror;
        }

        public JpDatabaseMirror Mirror { get; private set; }

        public long Version => version;

        public bool RequiresUnhash => false;

        public string DataVer => (Mirror == JpDatabaseMirror.Taiwan ? "twdb" : "jpdb")
            + (version > 0 ? version.ToString() : "latest")
            + (Mirror == JpDatabaseMirror.Roboninon ? "_roboninon" : "");

        public byte[] ResolveDatabase()
        {
            return ResolveDatabase(null);
        }

        public byte[] ResolveDatabase(AssetDownloadUtil.ByteProgress progress)
        {
            return AssetDownloadUtil.RunSync(async () =>
            {
                if (version <= 0)
                {
                    version = await JpDatabaseDownloader.FetchLatestVersionAsync(Mirror).ConfigureAwait(false);
                }

                return await JpDatabaseDownloader.DownloadAsync(Mirror, version, progress).ConfigureAwait(false);
            });
        }
    }
}
