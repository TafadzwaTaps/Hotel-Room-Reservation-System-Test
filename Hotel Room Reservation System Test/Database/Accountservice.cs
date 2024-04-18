

using MongoDB.Driver;
using Sample_Hotel_Room_Reservation_System.Models;

public class Accountservices{
  
    private readonly string database="HotelReseervation";

    private readonly string collectionname="Account";

    public IMongoCollection<T> mongoCollection<T>(in string collection)
    {
            var connect = new MongoClient("");
            var db = connect.GetDatabase(database);
            return db.GetCollection<T>(collection);

    }

    public string login(string username,string password){

        var collect = mongoCollection<User>(collectionname);

        collect.Find(x=>x.UserName==username && x.PasswordHash==password).FirstOrDefault();

        return "success";

    }

    public string register(User user){

         var collect = mongoCollection<User>(collectionname);

         collect.InsertOne(user);

         return "success";


    }

    private bool IsUsernameTaken(string username)
    {
        var collect = mongoCollection<User>(collectionname);
        var user = collect.Find(x=>x.UserName==username).FirstOrDefault();
        if(user!=null){
            return true;
        }
        
        return false;
    }







}