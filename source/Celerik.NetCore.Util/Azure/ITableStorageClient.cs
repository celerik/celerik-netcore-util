using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Interface definition to read data from and write data to a
    /// Table Storage.
    /// </summary>
    /// <typeparam name="TElement">The type of table entity.</typeparam>
    public interface ITableStorageClient<TElement> where TElement : ITableEntity, new()
    {
        /// <summary>
        /// Configuration to access the Table Storage.
        /// </summary>
        TableStorageConfig Config { get; }

        /// <summary>
        /// Gets an entity by its partitionKey and rowKey. If there
        /// is no entity with the passed-in criteria, null is returned.
        /// </summary>
        /// <param name="partitionKey">Partitition Key.</param>
        /// <param name="rowKey">Row Key.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">If partitionKey or
        /// rowKey are null.</exception>
        /// <exception cref="TableStorageException">If there was an
        /// error performing the operation.</exception>
        Task<TElement> GetAsync(string partitionKey, string rowKey);

        /// <summary>
        /// Checks if an entity with the passed-in partitionKey and
        /// rowKey exists.
        /// </summary>
        /// <param name="partitionKey">Partitition Key.</param>
        /// <param name="rowKey">Row Key.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">If partitionKey or
        /// rowKey are null.</exception>
        /// <exception cref="TableStorageException">If there was an
        /// error performing the operation.</exception>
        Task<bool> AnyAsync(string partitionKey, string rowKey);

        /// <summary>
        /// Gets a list of entities matching the given partitionKey. If
        /// Partition key is null, all entities will be returned (take
        /// into account that it could be a very expensive operation).
        /// </summary>
        /// <param name="partitionKey">Partition Key.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="TableStorageException">If there was an
        /// error performing the operation.</exception>
        Task<List<TElement>> ListAsync(string partitionKey = null);

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">If entity is null.
        /// </exception>
        /// <exception cref="PropNullException">If PartitionKey or
        /// RowKey are null.</exception>
        /// <exception cref="TableStorageException">If there was an
        /// error performing the operation.</exception>
        Task<TElement> InsertAsync(TElement entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="strategy">Defines the strategy used to update the
        /// entity, by default it is set to: InsertOrReplace.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">If entity is null.
        /// </exception>
        /// <exception cref="PropNullException">If PartitionKey or
        /// RowKey are null in the entity object.</exception>
        /// <exception cref="TableStorageException">If there was an
        /// error performing the operation.</exception>
        Task<TElement> UpdateAsync(TElement entity, TableStorageStrategy strategy = TableStorageStrategy.InsertOrReplace);

        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="partitionKey">Partitition Key.</param>
        /// <param name="rowKey">Row Key.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">If partitionKey or
        /// rowKey are null.</exception>
        /// <exception cref="TableStorageException">If there was an
        /// error performing the operation.</exception>
        Task<TElement> DeleteAsync(string partitionKey, string rowKey);

        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">If entity is null,
        /// or entity.PartitionKey is null or entity.RowKey is null.
        /// </exception>
        /// <exception cref="TableStorageException">If there was an
        /// error performing the operation.</exception>
        Task<TElement> DeleteAsync(TElement entity);

        /// <summary>
        /// Delete the table specified by the tableName from ITableStorageClient instance based on the
        /// passed-in provider.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.
        /// </returns>
        Task<bool> DeleteIfExistsAsync();
    }
}
