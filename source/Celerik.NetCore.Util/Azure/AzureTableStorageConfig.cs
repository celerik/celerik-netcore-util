namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Defines the configuration to access an Azure Table Storage.
    /// </summary>
    public class AzureTableStorageConfig
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Name of the table.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Indicates whether exceptions thrown by doing table operations
        /// (List, Get, Insert, Update, Delete) should be hidden.
        /// </summary>
        public bool HideExceptions { get; set; }
    }
}
