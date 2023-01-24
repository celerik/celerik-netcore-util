using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Implementation of the ITableStorageClient interface by
    /// using Mock Data.
    /// </summary>
    /// <typeparam name="TElement">The type of table entity.</typeparam>
    public class MockTableStorageClient<TElement> : TableStorageClient<TElement>
        where TElement : ITableEntity, new()
    {
        /// <summary>
        /// Object containing the mocked entities.
        /// </summary>
        private readonly IList<TElement> _mock = new List<TElement>();

        /// <inheritdoc />
        public MockTableStorageClient(TableStorageConfig config)
            : base(config) { }

        /// <inheritdoc />
        protected override async Task<TableResult> ExecuteAsync(TableOperation operation)
        {
            var operationEntity = (TElement)operation.Entity;

            if (operationEntity == null)
            {
                var fields = operation.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                var partitionKey = fields.First(f => f.Name.Contains("PartitionKey"));
                var rowKey = fields.First(f => f.Name.Contains("RowKey"));
                operationEntity = (TElement)Activator.CreateInstance(typeof(TElement));
                operationEntity.PartitionKey = (string)partitionKey.GetValue(operation);
                operationEntity.RowKey = (string)rowKey.GetValue(operation);
            }

            operationEntity.ETag = Guid.NewGuid().ToString();
            operationEntity.Timestamp = new DateTimeOffset(DateTime.UtcNow);

            var persistedEntity = _mock.FirstOrDefault(e =>
                e.PartitionKey == operationEntity.PartitionKey &&
                e.RowKey == operationEntity.RowKey);

            var result = new TableResult
            {
                Result = operationEntity,
                HttpStatusCode = 200,
                Etag = operationEntity.ETag
            };

            var type = operation.OperationType;
            var persisted = persistedEntity != null;

            if (type == TableOperationType.Delete ||
                type == TableOperationType.Replace ||
                (type == TableOperationType.InsertOrReplace && persisted))
                _mock.Remove(persistedEntity);

            if (type == TableOperationType.Insert ||
                type == TableOperationType.Replace ||
                (type == TableOperationType.InsertOrReplace) ||
                (type == TableOperationType.InsertOrMerge && !persisted))
                _mock.Add(operationEntity);

            if (type == TableOperationType.Delete ||
                type == TableOperationType.Retrieve ||
                type == TableOperationType.Merge ||
                (type == TableOperationType.InsertOrMerge && persisted))
                result.Result = persistedEntity;

            if (type == TableOperationType.Merge ||
                (type == TableOperationType.InsertOrMerge && persisted))
                persistedEntity = Merge(persistedEntity, operationEntity);

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Merges the source object into the target object.
        /// </summary>
        /// <typeparam name="T">The type of objects to be merged.</typeparam>
        /// <param name="target">The target object.</param>
        /// <param name="source">The source object.</param>
        /// <returns>The merged object.</returns>
        public static T Merge<T>(T target, T source)
        {
            typeof(T)
                .GetProperties()
                .Select((PropertyInfo x) => new KeyValuePair<PropertyInfo, object>(x, x.GetValue(source, null)))
                .Where((KeyValuePair<PropertyInfo, object> x) => x.Value != null).ToList()
                .ForEach((KeyValuePair<PropertyInfo, object> x) => x.Key.SetValue(target, x.Value, null));

            return target;
        }

        /// <inheritdoc />
        protected override async Task<TableQuerySegment<TElement>> ExecuteAsync(TableQuery<TElement> query, TableContinuationToken token)
        {
            var constructor = typeof(TableQuerySegment<TElement>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(c => c.GetParameters().Count() == 1);

            var result = constructor.Invoke(new object[] {
                new List<TElement>()
            }) as TableQuerySegment<TElement>;

            return await Task.FromResult(result);
        }

        /// <inheritdoc />
        public override async Task<bool> DeleteIfExistsAsync()
            => await Task.FromResult(true);
    }
}
