using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
namespace MongoDB.Test

{
    [TestClass]
    public class TestUpdate: TestFixture
    {
        [TestMethod]
        public async Task Update_Restaurant_Cuisine()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Eq("name", "Morris Park Bake Shop");
            var update = Builders<BsonDocument>.Update
                .Set("cuisine", "French Bakery")
                .CurrentDate("lastModified");
            var result = await collection.UpdateOneAsync(filter, update);           

            Assert.AreEqual(1, result.MatchedCount);

            if (result.IsModifiedCountAvailable)
            {
                Assert.AreEqual(1, result.ModifiedCount);
            }
        }

        [TestMethod]
        public async Task Update_Restaurant_Address_Fields()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Eq("restaurant_id", "30075445");
            var update = Builders<BsonDocument>.Update
                .Set("address.building", "70")
                .Set("address.street", "West 3rd Ave")
                .Set("address.zipcode", "13780")
                .CurrentDate("lastModified");
            var result = await collection.UpdateOneAsync(filter, update);

            Assert.AreEqual(1, result.MatchedCount);
            if (result.IsModifiedCountAvailable)
            {
                Assert.AreEqual(1, result.ModifiedCount);
            }
        }

        [TestMethod]
        public async Task Update_Multiple_Restaurants()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var filter = builder.Eq("address.zipcode", "10462") & builder.Eq("cuisine", "Other");         
            var update = Builders<BsonDocument>.Update
                .Set("cuisine", "Category TBD")                
                .CurrentDate("lastModified");
            var result = await collection.UpdateOneAsync(filter, update);

            Assert.AreEqual(1, result.MatchedCount);

            if (result.IsModifiedCountAvailable)
            {
                Assert.AreEqual(1, result.ModifiedCount);
            }
        }



        //"address": {"building": "1007", "coord": [-73.856077, 40.848447], "street": "Morris Park Ave", "zipcode": "10462"}, 
        //"borough": "Bronx", 
        //"cuisine": "Bakery", 
        //"grades": [{"date": {"$date": 1393804800000}, "grade": "A", "score": 2}, 
        //{"date": {"$date": 1378857600000}, "grade": "A", "score": 6}, 
        //{"date": {"$date": 1358985600000}, "grade": "A", "score": 10}, 
        //{"date": {"$date": 1322006400000}, "grade": "A", "score": 9}, 
        //{"date": {"$date": 1299715200000}, "grade": "B", "score": 14}], 
        //"name": "Morris Park Bake Shop", 
        //"restaurant_id": "30075445"}


    }
}
