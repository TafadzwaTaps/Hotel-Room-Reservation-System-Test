
using MongoDB.Driver;

public class Reservationservice{

      public IMongoCollection<T> mongoCollection<T>(in string collection)
    {
            var connect = new MongoClient("");
            var db = connect.GetDatabase("");
            return db.GetCollection<T>(collection);

    }
    
}