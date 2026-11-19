namespace PCRCaculator.SQL
{
    /// <summary>
    /// master 数据库来源。实现者负责把数据库字节交给 SQLiteTool。
    /// </summary>
    public interface IDataBaseManager
    {
        /// <summary>把数据库文件内容取回来（可能触发下载）。</summary>
        byte[] ResolveDatabase();

        /// <summary>版本标识，同时决定落地文件名 {DataVer}.db。</summary>
        string DataVer { get; }

        /// <summary>字节是否需要走国服式的 rainbow 反哈希（日服官方库需要，镜像库不需要）。</summary>
        bool RequiresUnhash { get; }
    }
}
