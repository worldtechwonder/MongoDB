using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.VisualStudio.TestTools.UnitTesting; 

namespace MongoDB.Test
{
    [TestClass]
    public class TestQuery: TestFixture
    {
        [TestMethod]
        public async Task WhenGetAll_Returns_AllRestaurants()
        {          
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = new BsonDocument();
            var count = 0;        

            using (var cursor = await collection.FindAsync<BsonDocument>(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {                       
                        count++;
                    }
                }
            }

            Assert.AreEqual(25359, count);          
        }

        [TestMethod]
        public async Task WhenGet_Cuisine_And_ZipCode_Returns_RestaurantList()
        {      
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var filter = builder.Eq("cuisine", "Bakery") & builder.Eq("address.zipcode", "10462");

            var result = await collection.Find(filter).ToListAsync();                                                
            Assert.AreEqual(7, result.Count());
        }


        [TestMethod]
        public async Task WhenGet_Cuisine_Or_ZipCode_Returns_RestaurantList()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var filter = builder.Eq("cuisine", "Bakery") | builder.Eq("address.zipcode", "10462");

            var result = await collection.Find(filter).ToListAsync();
            Assert.AreEqual(834, result.Count());
        }

        [TestMethod]
        public async Task WhenGet_Borough_Name_Returns_RestaurantList()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var filter = builder.Eq("borough", "Bronx");        
            var result = await collection.Find(filter).ToListAsync();
            Assert.AreEqual(2338, result.Count());
        }


        [TestMethod]
        public async Task WhenGet_Grade_Letter_Returns_RestaurantList()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var filter = builder.Eq("grades.grade", "A");
            var result = await collection.Find(filter).ToListAsync();
            Assert.AreEqual(23440, result.Count());
        }


        [TestMethod]
        public async Task WhenGet_Grade_Score_Greater_Than_Returns_RestaurantList()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var filter = builder.Gt("grades.score", 20);
            var result = await collection.Find(filter).ToListAsync();
            Assert.AreEqual(6332, result.Count());
        }


        [TestMethod]
        public async Task WhenGet_Grade_Score_Less_Than_Returns_RestaurantList()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var filter = builder.Lt("grades.score", 15);
            var result = await collection.Find(filter).ToListAsync();
            Assert.AreEqual(23828, result.Count());
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
