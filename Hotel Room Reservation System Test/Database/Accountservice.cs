using Hotel_Room_Reservation_System_Test.Models;
using MongoDB.Driver;

public class Accountservices{
  
    private readonly string database="HotelReseervation";

    private readonly string collectionname="Account";

    public IMongoCollection<T> mongoCollection<T>(in string collection)
    {
            var connect = new MongoClient("mongodb+srv://Quicksly:f4PvWY5G3RtuZcQo@cluster0.d2muozt.mongodb.net/?retryWrites=true&w=majority");
            var db = connect.GetDatabase(database);
            return db.GetCollection<T>(collection);

    }

    public User login(string username,string password){

        var collect = mongoCollection<User>(collectionname);

        return collect.Find(x=>x.UserName==username && x.PasswordHash==password).FirstOrDefault();

    }

    public string register(User user){

         var collect = mongoCollection<User>(collectionname);

         collect.InsertOne(user);

         return "success";


    }

   public bool IsUsernameTaken(string username)
    {
        var collect = mongoCollection<User>(collectionname);
        var user = collect.Find(x=>x.UserName==username).FirstOrDefault();
        if(user!=null){
            return true;
        }
        
        return false;
    }







}