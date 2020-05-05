using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;

namespace Celerik.NetCore.Util.Test
{
    public class Person : TableEntity
    {
        public Person()
        {
            PartitionKey = "4C7374ED-568C-4C51-993E-FFE6F0A128B1";
            RowKey = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public int Age { get; set; }
    }

    [TestClass]
    public class TableStorageTest
    {
        private readonly ITableStorageClient<Person> Client
            = CreateClient(new TableStorageConfig
            {
                ConnectionString = "DefaultEndpointsProtocol=https;AccountName=fakeaccount;AccountKey=123456==;EndpointSuffix=core.windows.net",
                TableName = nameof(Person)
            });

        private static ITableStorageClient<Person> CreateClient(TableStorageConfig config)
            => TableStorageFactory.Create<Person>(config, TableStorageProvider.Mock);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullConfig()
            => CreateClient(config: null);

        [TestMethod]
        [ExpectedException(typeof(PropNullOrEmptyException))]
        public void ConstructorNullConnectionString()
            => CreateClient(new TableStorageConfig
            {
                ConnectionString = null,
                TableName = Client.Config.TableName
            });

        [TestMethod]
        [ExpectedException(typeof(PropNullOrEmptyException))]
        public void ConstructorEmptyConnectionString()
            => CreateClient(new TableStorageConfig
            {
                ConnectionString = "",
                TableName = Client.Config.TableName
            });

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ConstructorInvalidConnectionString()
            => CreateClient(new TableStorageConfig
            {
                ConnectionString = "ConnectionString",
                TableName = Client.Config.TableName
            });

        [TestMethod]
        [ExpectedException(typeof(PropNullOrEmptyException))]
        public void ConstructorNullTableName()
            => CreateClient(new TableStorageConfig
            {
                ConnectionString = Client.Config.ConnectionString,
                TableName = null
            });

        [TestMethod]
        [ExpectedException(typeof(PropNullOrEmptyException))]
        public void ConstructorEmptyTableName()
            => CreateClient(new TableStorageConfig
            {
                ConnectionString = Client.Config.ConnectionString,
                TableName = ""
            });

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ConstructorInvalidTableName()
            => CreateClient(new TableStorageConfig
            {
                ConnectionString = Client.Config.ConnectionString,
                TableName = "*111**___??{}"
            });

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorReservedTableName()
            => CreateClient(new TableStorageConfig
            {
                ConnectionString = Client.Config.ConnectionString,
                TableName = "tables"
            });

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetNullPartitionKey()
            => await Client.GetAsync(partitionKey: null, rowKey: Guid.NewGuid().ToString());

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetNullRowKey()
            => await Client.GetAsync(partitionKey: Guid.NewGuid().ToString(), rowKey: null);

        [TestMethod]
        public async Task GetUnexistingEntity()
        {
            var person = await Client.GetAsync(
                partitionKey: Guid.NewGuid().ToString(),
                rowKey: Guid.NewGuid().ToString());

            Assert.AreEqual(null, person);
        }

        [TestMethod]
        public async Task GetExistingEntity()
        {
            var person1 = await Client.InsertAsync(new Person());
            var person2 = await Client.GetAsync(person1.PartitionKey, person1.RowKey);

            Assert.AreEqual(person1.PartitionKey, person2.PartitionKey);
            Assert.AreEqual(person1.RowKey, person2.RowKey);

            await Client.DeleteAsync(person1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AnyNullPartitionKey()
            => await Client.AnyAsync(partitionKey: null, rowKey: Guid.NewGuid().ToString());

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AnyNullRowKey()
            => await Client.AnyAsync(partitionKey: Guid.NewGuid().ToString(), rowKey: null);

        [TestMethod]
        public async Task AnyFalse()
        {
            var any = await Client.AnyAsync(
                partitionKey: Guid.NewGuid().ToString(),
                rowKey: Guid.NewGuid().ToString());

            Assert.AreEqual(false, any);
        }

        [TestMethod]
        public async Task AnyTrue()
        {
            var person = await Client.InsertAsync(new Person());
            var any = await Client.AnyAsync(person.PartitionKey, person.RowKey);

            Assert.AreEqual(true, any);
            await Client.DeleteAsync(person);
        }

        [TestMethod]
        public async Task ListNullPartitionKey()
        {
            var people = await Client.ListAsync();
            Assert.AreEqual(true, people.Count >= 0);
        }

        [TestMethod]
        public async Task ListUnexistingPartititionKey()
        {
            var people = await Client.ListAsync(partitionKey: Guid.NewGuid().ToString());
            Assert.AreEqual(0, people.Count);
        }

        public async Task ListExistingPartititionKey()
        {
            var recordCount = 5;
            var partitionKey = Guid.NewGuid().ToString();
            for (var i = 0; i < recordCount; i++)
                await Client.InsertAsync(new Person { PartitionKey = partitionKey });

            var people = await Client.ListAsync(partitionKey);
            Assert.AreEqual(recordCount, people.Count);

            foreach (var person in people)
                await Client.DeleteAsync(person);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task InsertNullEntity()
            => await Client.InsertAsync(entity: null);

        [TestMethod]
        [ExpectedException(typeof(PropNullException))]
        public async Task InsertNullPartitionKey()
            => await Client.InsertAsync(new Person { PartitionKey = null });

        [TestMethod]
        [ExpectedException(typeof(PropNullException))]
        public async Task InsertNullRowKey()
            => await Client.InsertAsync(new Person { RowKey = null });

        [TestMethod]
        public async Task InsertExistingEntity()
        {
            var person = await Client.InsertAsync(new Person());
            var exception = false;

            try
            {
                await Client.InsertAsync(new Person { RowKey = person.RowKey });
            }
            catch
            {
                exception = true;
            }

            Assert.AreEqual(true, exception);
            await Client.DeleteAsync(person);
        }

        [TestMethod]
        public async Task InsertOk()
        {
            var utcNow = DateTime.UtcNow;
            var person1 = new Person { Name = "Martín de Francisco" };
            var person2 = await Client.InsertAsync(person1);
            var person3 = await Client.GetAsync(person2.PartitionKey, person2.RowKey);

            Assert.AreEqual(person1.PartitionKey, person3.PartitionKey);
            Assert.AreEqual(person1.RowKey, person3.RowKey);
            Assert.AreEqual(true, person3.Timestamp.UtcDateTime > utcNow);
            Assert.AreNotEqual(null, person3.ETag);
            Assert.AreEqual(person1.Name, person3.Name);
            Assert.AreEqual(0, person3.Age);

            await Client.DeleteAsync(person3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UpdateNullEntity()
            => await Client.UpdateAsync(entity: null);

        [TestMethod]
        [ExpectedException(typeof(PropNullException))]
        public async Task UpdateNullPartitionKey()
            => await Client.UpdateAsync(new Person { PartitionKey = null });

        [TestMethod]
        [ExpectedException(typeof(PropNullException))]
        public async Task UpdateNullRowKey()
            => await Client.InsertAsync(new Person { RowKey = null });

        [TestMethod]
        public async Task UpdateNewEntityInsertOrMergeStrategy()
        {
            var utcNow = DateTime.UtcNow;
            var person1 = new Person { Name = "Josefa" };
            var person2 = await Client.UpdateAsync(person1, TableStorageStrategy.InsertOrMerge);
            var person3 = await Client.GetAsync(person2.PartitionKey, person2.RowKey);

            Assert.AreEqual(person1.PartitionKey, person3.PartitionKey);
            Assert.AreEqual(person1.RowKey, person3.RowKey);
            Assert.AreEqual(true, person3.Timestamp.UtcDateTime > utcNow);
            Assert.AreNotEqual(null, person3.ETag);
            Assert.AreEqual(person1.Name, person3.Name);
            Assert.AreEqual(0, person3.Age);

            await Client.DeleteAsync(person3);
        }

        [TestMethod]
        public async Task UpdateExistingEntityInsertOrMergeStrategy()
        {
            var utcNow = DateTime.UtcNow;
            var person1 = new Person { Name = "Josefa" };
            var person2 = await Client.InsertAsync(person1);
            var person3 = await Client.UpdateAsync(new Person
            {
                PartitionKey = person2.PartitionKey,
                RowKey = person2.RowKey,
                Age = 50
            }, TableStorageStrategy.InsertOrMerge);
            var person4 = await Client.GetAsync(person1.PartitionKey, person1.RowKey);

            Assert.AreEqual(person1.PartitionKey, person4.PartitionKey);
            Assert.AreEqual(person1.RowKey, person4.RowKey);
            Assert.AreEqual(true, person4.Timestamp.UtcDateTime > utcNow);
            Assert.AreNotEqual(null, person4.ETag);
            Assert.AreEqual(person1.Name, person4.Name);
            Assert.AreEqual(person3.Age, person4.Age);

            await Client.DeleteAsync(person3);
        }

        [TestMethod]
        [ExpectedException(typeof(TableStorageException))]
        public async Task UpdateNewEntityMergeStrategy()
            => await Client.UpdateAsync(new Person(), TableStorageStrategy.Merge);

        [TestMethod]
        public async Task UpdateExistingEntityMergeStrategy()
        {
            var utcNow = DateTime.UtcNow;
            var person1 = new Person { Name = "Josefa" };
            var person2 = await Client.InsertAsync(person1);
            var person3 = await Client.UpdateAsync(new Person
            {
                PartitionKey = person2.PartitionKey,
                RowKey = person2.RowKey,
                Age = 50
            }, TableStorageStrategy.Merge);
            var person4 = await Client.GetAsync(person1.PartitionKey, person1.RowKey);

            Assert.AreEqual(person1.PartitionKey, person4.PartitionKey);
            Assert.AreEqual(person1.RowKey, person4.RowKey);
            Assert.AreEqual(true, person4.Timestamp.UtcDateTime > utcNow);
            Assert.AreNotEqual(null, person4.ETag);
            Assert.AreEqual(person1.Name, person4.Name);
            Assert.AreEqual(person3.Age, person4.Age);

            await Client.DeleteAsync(person3);
        }

        [TestMethod]
        [ExpectedException(typeof(TableStorageException))]
        public async Task UpdateNewEntityReplaceStrategy()
            => await Client.UpdateAsync(new Person(), TableStorageStrategy.Replace);

        [TestMethod]
        public async Task UpdateExistingEntityReplaceStrategy()
        {
            var utcNow = DateTime.UtcNow;
            var person1 = new Person { Name = "Josefa" };
            var person2 = await Client.InsertAsync(person1);
            var person3 = await Client.UpdateAsync(new Person
            {
                PartitionKey = person2.PartitionKey,
                RowKey = person2.RowKey,
                Age = 50
            }, TableStorageStrategy.Replace);
            var person4 = await Client.GetAsync(person1.PartitionKey, person1.RowKey);

            Assert.AreEqual(person1.PartitionKey, person4.PartitionKey);
            Assert.AreEqual(person1.RowKey, person4.RowKey);
            Assert.AreEqual(true, person4.Timestamp.UtcDateTime > utcNow);
            Assert.AreNotEqual(null, person4.ETag);
            Assert.AreEqual(null, person4.Name);
            Assert.AreEqual(person3.Age, person4.Age);

            await Client.DeleteAsync(person3);
        }

        [TestMethod]
        public async Task UpdateNewEntityInsertOrReplaceStrategy()
        {
            var utcNow = DateTime.UtcNow;
            var person1 = new Person { Name = "Josefa" };
            var person2 = await Client.UpdateAsync(person1);
            var person3 = await Client.GetAsync(person2.PartitionKey, person2.RowKey);

            Assert.AreEqual(person1.PartitionKey, person3.PartitionKey);
            Assert.AreEqual(person1.RowKey, person3.RowKey);
            Assert.AreEqual(true, person3.Timestamp.UtcDateTime > utcNow);
            Assert.AreNotEqual(null, person3.ETag);
            Assert.AreEqual(person1.Name, person3.Name);
            Assert.AreEqual(0, person3.Age);

            await Client.DeleteAsync(person3);
        }

        [TestMethod]
        public async Task UpdateExistingEntityInsertOrReplaceStrategy()
        {
            var utcNow = DateTime.UtcNow;
            var person1 = new Person { Name = "Josefa" };
            var person2 = await Client.InsertAsync(person1);
            var person3 = await Client.UpdateAsync(new Person
            {
                PartitionKey = person2.PartitionKey,
                RowKey = person2.RowKey,
                Age = 50
            });
            var person4 = await Client.GetAsync(person1.PartitionKey, person1.RowKey);

            Assert.AreEqual(person1.PartitionKey, person4.PartitionKey);
            Assert.AreEqual(person1.RowKey, person4.RowKey);
            Assert.AreEqual(true, person4.Timestamp.UtcDateTime > utcNow);
            Assert.AreNotEqual(null, person4.ETag);
            Assert.AreEqual(null, person4.Name);
            Assert.AreEqual(person3.Age, person4.Age);

            await Client.DeleteAsync(person3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteByKeysNullPartitionKey()
            => await Client.DeleteAsync(partitionKey: null, rowKey: Guid.NewGuid().ToString());

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteByKeysNullRowKey()
            => await Client.DeleteAsync(partitionKey: Guid.NewGuid().ToString(), rowKey: null);

        [TestMethod]
        [ExpectedException(typeof(TableStorageException))]
        public async Task DeleteByKeysUnexistingEntity()
            => await Client.DeleteAsync(
                partitionKey: Guid.NewGuid().ToString(),
                rowKey: Guid.NewGuid().ToString());

        [TestMethod]
        public async Task DeleteByKeysOk()
        {
            var inserted = await Client.InsertAsync(new Person());
            var deleted = await Client.DeleteAsync(inserted.PartitionKey, inserted.RowKey);
            var searched = await Client.GetAsync(deleted.PartitionKey, deleted.RowKey);

            Assert.AreEqual(null, searched);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteByEntityNullEntity()
            => await Client.DeleteAsync(entity: null);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteByEntityNullPartitionKey()
            => await Client.DeleteAsync(new Person { PartitionKey = null });

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteByEntityNullRowKey()
            => await Client.DeleteAsync(new Person { RowKey = null });

        [TestMethod]
        [ExpectedException(typeof(TableStorageException))]
        public async Task DeleteByEntityUnexistingEntity()
            => await Client.DeleteAsync(new Person());

        [TestMethod]
        public async Task DeleteByEntityOk()
        {
            var inserted = await Client.InsertAsync(new Person());
            var deleted = await Client.DeleteAsync(inserted);
            var searched = await Client.GetAsync(deleted.PartitionKey, deleted.RowKey);

            Assert.AreEqual(null, searched);
        }
    }
}
