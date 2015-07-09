using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB.Test
{
    [TestClass]
    public class TestInsert: TestFixture
    {
        [TestMethod]
        public async Task InsertADocument()
        {
            var document = new BsonDocument
            {
                { "address" , new BsonDocument
                    {
                        { "street", "Market Street" },
                        { "zipcode", "94105" },
                        { "building", "100" },
                        { "coord", new BsonArray { 73.9557413, 40.7720266 } }
                    }
                },
                { "borough", "San Francisco" },
                { "cuisine", "Chinese" },
                { "grades", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "date", new DateTime(2015, 10, 1, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "A" },
                            { "score", 10 }
                        },
                        new BsonDocument
                        {
                            { "date", new DateTime(2015, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "B" },
                            { "score", 8 }
                        }
                    }
                },
                { "name", "R&G Lounge" },
                { "restaurant_id", "51704620" }
            };

            var collection = _database.GetCollection<BsonDocument>("restaurants");
            await collection.InsertOneAsync(document);          
                      
        }
    }
}
