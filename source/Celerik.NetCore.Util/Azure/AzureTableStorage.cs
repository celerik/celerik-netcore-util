using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides functions to read data from and write data to an Azure Table Storage.
    /// </summary>
    /// <typeparam name="TElement">The type of table entity.</typeparam>
    [ExcludeFromCodeCoverage]
    public class AzureTableStorage<TElement> where TElement : ITableEntity, new()
    {
        /// <summary>
        /// Configuration to access the Azure Table Storage.
        /// </summary>
        private readonly AzureTableStorageConfig _config;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="config">Configuration to access the Azure Table Storage.</param>
        public AzureTableStorage(AzureTableStorageConfig config)
            => _config = config;

        /// <summary>
        /// Gets a list of entities matching the given partitionKey.
        /// </summary>
        /// <param name="partitionKey">Partition Key.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        public async Task<List<TElement>> ListAsync(string partitionKey = null)
        {
            try
            {
                var entitites = new List<TElement>();
                var table = await GetTableAsync();
                var token = (TableContinuationToken)null;
                var query = new TableQuery<TElement>();

                if (partitionKey != null)
                    query = query.Where(TableQuery.GenerateFilterCondition(
                        "PartitionKey",
                        QueryComparisons.Equal,
                        partitionKey
                    ));

                do
                {
                    var segment = await table.ExecuteQuerySegmentedAsync(query, token);
                    token = segment.ContinuationToken;
                    entitites.AddRange(segment.Results);

                } while (token != null);

                return entitites;
            }
            catch
            {
                if (_config.HideExceptions)
                    return new List<TElement>();

                throw;
            }
        }

        /// <summary>
        /// Gets an entity by its partitionKey and rowKey.
        /// </summary>
        /// <param name="partitionKey">Partitition Key.</param>
        /// <param name="rowKey">Row Key.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        public async Task<TElement> GetAsync(string partitionKey, string rowKey)
        {
            try
            {
                var table = await GetTableAsync();
                var operation = TableOperation.Retrieve<TElement>(partitionKey, rowKey);
                var result = await table.ExecuteAsync(operation);
                var entity = (TElement)(dynamic)result.Result;

                return entity;
            }
            catch
            {
                if (_config.HideExceptions)
                    return default;

                throw;
            }
        }

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        public async Task<bool> InsertAsync(TElement entity)
        {
            try
            {
                var table = await GetTableAsync();
                var operation = TableOperation.Insert(entity);
                await table.ExecuteAsync(operation);

                return true;
            }
            catch
            {
                if (_config.HideExceptions)
                    return false;

                throw;
            }
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        public async Task<bool> UpdateAsync(TElement entity)
        {
            try
            {
                var table = await GetTableAsync();
                var operation = TableOperation.InsertOrReplace(entity);
                await table.ExecuteAsync(operation);

                return true;
            }
            catch
            {
                if (_config.HideExceptions)
                    return false;

                throw;
            }
        }

        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="partitionKey">Partitition Key.</param>
        /// <param name="rowKey">Row Key.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        public async Task<bool> DeleteAsync(string partitionKey, string rowKey)
        {
            try
            {
                var entity = await GetAsync(partitionKey, rowKey);
                var table = await GetTableAsync();
                var operation = TableOperation.Delete(entity);
                await table.ExecuteAsync(operation);

                return true;
            }
            catch
            {
                if (_config.HideExceptions)
                    return false;

                throw;
            }
        }

        /// <summary>
        /// Gets a reference to the current table. In case the table doesn´t
        /// exist, it is created.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        private async Task<CloudTable> GetTableAsync()
        {
            var account = CloudStorageAccount.Parse(_config.ConnectionString);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference(_config.TableName);
            await table.CreateIfNotExistsAsync();

            return table;
        }
    }
}
