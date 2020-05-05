namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Defines the different table storage providers.
    /// </summary>
    public enum TableStorageProvider
    {
        /// <summary>
        /// Provider using Mock Data.
        /// </summary>
        Mock = 1,

        /// <summary>
        /// Provider using Azure Table Storage.
        /// </summary>
        Azure = 2
    }
}
