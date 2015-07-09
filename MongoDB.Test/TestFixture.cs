using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MongoDB.Test
{   
    [TestClass]
    public class TestFixture
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        private static List<BsonDocument> _dataset;
         
        [TestInitialize]
        public void Setup()
        {
            string connectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost";
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("test");    

            LoadDataSetFromFile();
            LoadCollection();
        }
        
        // helper methods
        private void LoadCollection()
        {
            LoadCollectionAsync().GetAwaiter().GetResult();
        }

        private async Task LoadCollectionAsync()
        {
            await _database.DropCollectionAsync("restaurants");

            var collection = _database.GetCollection<BsonDocument>("restaurants");
            await collection.InsertManyAsync(_dataset);
        }

        private void LoadDataSetFromFile()
        {
            _dataset = new List<BsonDocument>();

            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("MongoDB.Test.dataset.json"))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var document = BsonDocument.Parse(line);
                    _dataset.Add(document);
                }
            }
        }

    }
}
