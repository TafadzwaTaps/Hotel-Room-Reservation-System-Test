using Hotel_Room_Reservation_System_Test.Models;
using MongoDB.Driver;

public class UserRoleservice{

      public IMongoCollection<T> mongoCollection<T>(in string collection)
    {
            var connect = new MongoClient("");
            var db = connect.GetDatabase("");
            return db.GetCollection<T>(collection);

    }
    
}