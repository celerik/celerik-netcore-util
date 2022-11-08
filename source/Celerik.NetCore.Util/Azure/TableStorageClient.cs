using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Base class for all services implementing the ITableStorageClient
    /// interface.
    /// </summary>
    /// <typeparam name="TElement">The type of table entity.</typeparam>
    public abstract class TableStorageClient<TElement> : ITableStorageClient<TElement>
        where TElement : ITableEntity, new()
    {
        /// <summary>
        /// List of reserved table names. Attempting to create a table with a
        /// reserved table name returns error code 404 (Bad Request).
        /// </summary>
        private readonly string[] ReservedTableNames = new string[] { "tables" };

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="config">Configuration to access the Table Storage.
        /// </param>
        /// <exception cref="ArgumentNullException">If config is null.
        /// </exception>
        /// <exception cref="PropNullOrEmptyException">If ConnectionString or
        /// TableName are null or empty.</exception>
        /// <exception cref="FormatException">If ConnectionString or TableName
        /// have an invalid format.</exception>
        /// <exception cref="ArgumentException">If TableName is a reserved
        /// table name.</exception>
        public TableStorageClient(TableStorageConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            if (string.IsNullOrEmpty(config.ConnectionString))
                throw new PropNullOrEmptyException(nameof(config.ConnectionString));
            if (string.IsNullOrEmpty(config.TableName))
                throw new PropNullOrEmptyException(nameof(config.TableName));

            var isValidConnectionString = CloudStorageAccount.TryParse(
                config.ConnectionString, out _);

            if (!isValidConnectionString)
                throw new FormatException(
                    UtilResources.Get("InvalidConnectionString"));
            if (!config.TableName.IsValidTableName())
                throw new FormatException(
                    UtilResources.Get("InvalidTableName", config.TableName));
            if (ReservedTableNames.Contains(config.TableName))
                throw new ArgumentException(
                    UtilResources.Get("TableStorageReservedName", config.TableName));

            Config = config;
        }

        /// <inheritdoc />
        public TableStorageConfig Config { get; private set; }

        /// <inheritdoc />
        public async Task<TElement> GetAsync(string partitionKey, string rowKey)
        {
            if (partitionKey == null)
                throw new ArgumentNullException(nameof(partitionKey));
            if (rowKey == null)
                throw new ArgumentNullException(nameof(rowKey));

            try
            {
                await CreateTableIfNotExistsAsync();

                var operation = TableOperation.Retrieve<TElement>(partitionKey, rowKey);
                var result = await ExecuteAsync(operation);
                var entity = (TElement)(dynamic)result.Result;

                return entity;
            }
            catch (Exception ex)
            {
                throw new TableStorageException(nameof(GetAsync), ex);
            }
        }

        /// <inheritdoc />
        public async Task<bool> AnyAsync(string partitionKey, string rowKey)
        {
            var entity = await GetAsync(partitionKey, rowKey);
            var any = entity != null;

            return any;
        }

        /// <inheritdoc />
        public async Task<List<TElement>> ListAsync(string partitionKey = null)
        {
            try
            {
                await CreateTableIfNotExistsAsync();

                var entitites = new List<TElement>();
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
                    var segment = await ExecuteAsync(query, token);
                    token = segment.ContinuationToken;
                    entitites.AddRange(segment.Results);
                }
                while (token != null);

                return entitites;
            }
            catch (Exception ex)
            {
                throw new TableStorageException(nameof(ListAsync), ex);
            }
        }

        /// <inheritdoc />
        public async Task<TElement> InsertAsync(TElement entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.PartitionKey == null)
                throw new PropNullException(nameof(entity.PartitionKey));
            if (entity.RowKey == null)
                throw new PropNullException(nameof(entity.RowKey));

            try
            {
                await CreateTableIfNotExistsAsync();

                var entityExists = await AnyAsync(entity.PartitionKey, entity.RowKey);
                if (entityExists)
                    throw new InvalidOperationException(
                        UtilResources.Get("TableStorageEntityExists", entity.PartitionKey, entity.RowKey));

                var operation = TableOperation.Insert(entity);
                var result = await ExecuteAsync(operation);

                ExceptionThrower.AssertHttp200Code(result.HttpStatusCode);

                entity = (TElement)(dynamic)result.Result;
                return entity;
            }
            catch (Exception ex)
            {
                throw new TableStorageException(nameof(InsertAsync), ex);
            }
        }

        /// <inheritdoc />
        public async Task<TElement> UpdateAsync(
            TElement entity,
            TableStorageStrategy strategy = TableStorageStrategy.InsertOrReplace)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.PartitionKey == null)
                throw new PropNullException(nameof(entity.PartitionKey));
            if (entity.RowKey == null)
                throw new PropNullException(nameof(entity.RowKey));

            try
            {
                await CreateTableIfNotExistsAsync();

                if (strategy == TableStorageStrategy.Merge ||
                    strategy == TableStorageStrategy.Replace)
                {
                    var any = await AnyAsync(entity.PartitionKey, entity.RowKey);
                    if (!any)
                        throw new InvalidOperationException(
                            UtilResources.Get("TableStorageEntityNotExists",
                            entity.PartitionKey, entity.RowKey));

                    entity.ETag ??= "*";
                }

                var operation = strategy switch
                {
                    TableStorageStrategy.InsertOrMerge
                        => TableOperation.InsertOrMerge(entity),
                    TableStorageStrategy.Merge
                        => TableOperation.Merge(entity),
                    TableStorageStrategy.Replace
                        => TableOperation.Replace(entity),
                    _
                        => TableOperation.InsertOrReplace(entity),
                };

                var result = await ExecuteAsync(operation);
                ExceptionThrower.AssertHttp200Code(result.HttpStatusCode);

                entity = (TElement)(dynamic)result.Result;
                return entity;
            }
            catch (Exception ex)
            {
                throw new TableStorageException(nameof(UpdateAsync), ex);
            }
        }

        /// <inheritdoc />
        public async Task<TElement> DeleteAsync(string partitionKey, string rowKey)
        {
            if (partitionKey == null)
                throw new ArgumentNullException(nameof(partitionKey));
            if (rowKey == null)
                throw new ArgumentNullException(nameof(rowKey));

            try
            {
                await CreateTableIfNotExistsAsync();

                var entity = await GetAsync(partitionKey, rowKey);
                if (entity == null)
                    throw new InvalidOperationException(
                        UtilResources.Get("TableStorageEntityNotExists",
                        partitionKey, rowKey));

                var operation = TableOperation.Delete(entity);
                var result = await ExecuteAsync(operation);

                ExceptionThrower.AssertHttp200Code(result.HttpStatusCode);

                entity = (TElement)(dynamic)result.Result;
                return entity;
            }
            catch (Exception ex)
            {
                throw new TableStorageException(nameof(DeleteAsync), ex);
            }
        }

        /// <inheritdoc />
        public async Task<TElement> DeleteAsync(TElement entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.PartitionKey == null)
                throw new PropNullException(nameof(entity.PartitionKey));
            if (entity.RowKey == null)
                throw new PropNullException(nameof(entity.RowKey));

            var result = await DeleteAsync(entity.PartitionKey, entity.RowKey);
            return result;
        }

        /// <summary>
        /// Creates the table if not exists.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        protected virtual async Task CreateTableIfNotExistsAsync()
            => await Task.FromResult(0);

        /// <summary>
        /// Executes the passed-in table operation.
        /// </summary>
        /// <param name="operation">The operation to execute.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        protected abstract Task<TableResult> ExecuteAsync(TableOperation operation);

        /// <summary>
        /// Executes the passed-in table query as a segment.
        /// </summary>
        /// <param name="query">The tabe query.</param>
        /// <param name="token">The continuation token.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        protected abstract Task<TableQuerySegment<TElement>> ExecuteAsync(TableQuery<TElement> query, TableContinuationToken token);
    }
}
