using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;


namespace MongoDB.Test
{
    [TestClass]
    public class TestRemove: TestFixture
    {
        [TestMethod]
        public async Task Remove_Matching_Cuisine()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Eq("cuisine", "Chinese");
            var result = await collection.DeleteManyAsync(filter);
           
            Assert.AreEqual(2418, result.DeletedCount);
        }

        [TestMethod]
        public async Task Remove_All_Restaurants()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = new BsonDocument();
            var result = await collection.DeleteManyAsync(filter);

            Assert.AreEqual(25359, result.DeletedCount);
        }

        [TestMethod]
        public async Task Drop_Restaurants_Collection()
        {          
            await _database.DropCollectionAsync("restaurants");
            
            using (var cursor = await _database.ListCollectionsAsync())
            {
                var collections = await cursor.ToListAsync();
                var result = collections.Find(document => document["name"] == "restaurants");
                Assert.AreEqual(null, result);
            }
           
        }

    }
}
