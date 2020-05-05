using Microsoft.WindowsAzure.Storage.Table;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Factory to create ITableStorageClient instances.
    /// </summary>
    public static class TableStorageFactory
    {
        /// <summary>
        /// Creates a new ITableStorageClient instance based on the
        /// passed-in provider.
        /// </summary>
        /// <typeparam name="TElement">The type of table entity.</typeparam>
        /// <param name="config">Configuration to access the Table Storage.
        /// </param>
        /// <param name="provider">Defines the provider used to build the
        /// instance. By default it is set to Azure Table Storage.</param>
        /// <returns>ITableStorageClient instance based on the passed-in
        /// provider.</returns>
        public static ITableStorageClient<TElement> Create<TElement>(
            TableStorageConfig config,
            TableStorageProvider provider = TableStorageProvider.Azure)
                where TElement : ITableEntity, new()
            => provider switch
            {
                TableStorageProvider.Mock => new MockTableStorageClient<TElement>(config),
                _ => new AzureTableStorageClient<TElement>(config)
            };
    }
}
