namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Defines the configuration needed to access a Table Storage.
    /// </summary>
    public class TableStorageConfig
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Name of the table.
        /// </summary>
        public string TableName { get; set; }
    }
}
