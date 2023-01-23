using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Implementation of the ITableStorageClient interface by
    /// using an Azure Table Storage.
    /// </summary>
    /// <typeparam name="TElement">The type of table entity.</typeparam>
    [ExcludeFromCodeCoverage]
    public class AzureTableStorageClient<TElement> : TableStorageClient<TElement>
        where TElement : ITableEntity, new()
    {
        /// <summary>
        /// Reference to the current cloud table.
        /// </summary>
        protected readonly CloudTable _cloudTable = null;

        /// <summary>
        /// Indicates whether the CloudTable exists.
        /// </summary>
        private bool _cloudTableExists = false;

        /// <inheritdoc />
        public AzureTableStorageClient(TableStorageConfig config)
            : base(config)
        {
            var account = CloudStorageAccount.Parse(config.ConnectionString);
            var cloudTableClient = account.CreateCloudTableClient();
            _cloudTable = cloudTableClient.GetTableReference(config.TableName);
        }

        /// <inheritdoc />
        protected override async Task CreateTableIfNotExistsAsync()
        {
            if (!_cloudTableExists)
            {
                await _cloudTable.CreateIfNotExistsAsync();
                _cloudTableExists = true;
            }
        }

        /// <inheritdoc />
        protected override async Task<bool> DeleteIfExistsAsync()
            => await _cloudTable.DeleteIfExistsAsync();

        /// <inheritdoc />
        protected override async Task<TableResult> ExecuteAsync(TableOperation operation)
            => await _cloudTable.ExecuteAsync(operation);

        /// <inheritdoc />
        protected override async Task<TableQuerySegment<TElement>> ExecuteAsync(TableQuery<TElement> query, TableContinuationToken token)
            => await _cloudTable.ExecuteQuerySegmentedAsync(query, token);
    }
}
