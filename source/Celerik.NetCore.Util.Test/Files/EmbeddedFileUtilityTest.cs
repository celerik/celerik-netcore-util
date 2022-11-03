using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Celerik.NetCore.Util.Test
{
    public class Movie
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string Name { get; set; }
    }

    [TestClass]
    public class EmbeddedFileUtilityTest
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFileFileNotFoundException()
        {
            EmbeddedFileUtility.ReadFile("Files.UnexistingFile.txt");
        }

        [TestMethod]
        public void ReadFileOkDefaultAssembly()
        {
            var movies = EmbeddedFileUtility.ReadFile("Files.Movies.json");

            Assert.AreEqual(true, movies.ContainsInvariant("Enter the Dragon"));
            Assert.AreEqual(true, movies.ContainsInvariant("Kill Bill"));
            Assert.AreEqual(true, movies.ContainsInvariant("Terminator"));
        }

        [TestMethod]
        public void ReadFileOkSpecifiedAssembly()
        {
            var movies = EmbeddedFileUtility.ReadFile("Files.Movies.json", typeof(EmbeddedFileUtilityTest).Assembly);

            Assert.AreEqual(true, movies.ContainsInvariant("Enter the Dragon"));
            Assert.AreEqual(true, movies.ContainsInvariant("Kill Bill"));
            Assert.AreEqual(true, movies.ContainsInvariant("Terminator"));
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadJsonFileNotFoundException()
        {
            EmbeddedFileUtility.ReadJson<object>("Files.UnexistingFile.json");
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadException))]
        public void ReadJsonFileLoadException()
        {
            EmbeddedFileUtility.ReadJson<EmbeddedFileUtilityTest>("Files.Movies.json");
        }

        [TestMethod]
        public void ReadJsonSerializeAllOk()
        {
            var movies = EmbeddedFileUtility.ReadJson<IEnumerable<Movie>>("Files.Movies.json");

            Assert.AreEqual(1, movies.ElementAt(0).Id);
            Assert.AreEqual(2, movies.ElementAt(1).Id);
            Assert.AreEqual(3, movies.ElementAt(2).Id);

            Assert.AreEqual("Enter the Dragon", movies.ElementAt(0).Name);
            Assert.AreEqual("Kill Bill", movies.ElementAt(1).Name);
            Assert.AreEqual("Terminator 2", movies.ElementAt(2).Name);
        }

        [TestMethod]
        public void ReadJsonApplyJsonIgnoreOk()
        {
            var movies = EmbeddedFileUtility.ReadJson<IEnumerable<Movie>>(
                "Files.Movies.json", serializeAll: false
            );

            Assert.AreEqual(1, movies.ElementAt(0).Id);
            Assert.AreEqual(2, movies.ElementAt(1).Id);
            Assert.AreEqual(3, movies.ElementAt(2).Id);

            Assert.AreEqual(null, movies.ElementAt(0).Name);
            Assert.AreEqual(null, movies.ElementAt(1).Name);
            Assert.AreEqual(null, movies.ElementAt(2).Name);
        }

        [TestMethod]
        public void ReadJsonSpecifiedAssembly()
        {
            var movies = EmbeddedFileUtility.ReadJson<IEnumerable<Movie>>(
                "Files.Movies.json", typeof(EmbeddedFileUtilityTest).Assembly);

            Assert.AreEqual(1, movies.ElementAt(0).Id);
            Assert.AreEqual(2, movies.ElementAt(1).Id);
            Assert.AreEqual(3, movies.ElementAt(2).Id);

            Assert.AreEqual("Enter the Dragon", movies.ElementAt(0).Name);
            Assert.AreEqual("Kill Bill", movies.ElementAt(1).Name);
            Assert.AreEqual("Terminator 2", movies.ElementAt(2).Name);
        }
    }
}
