using Hotel_Room_Reservation_System_Test.Models;
using MongoDB.Driver;

public class Roomservice{

       private readonly string database="HotelReseervation";

      private readonly string collectionname="Room";
      public IMongoCollection<T> mongoCollection<T>(in string collection)
    {
            var connect = new MongoClient("mongodb+srv://Quicksly:f4PvWY5G3RtuZcQo@cluster0.d2muozt.mongodb.net/?retryWrites=true&w=majority");
            var db = connect.GetDatabase(database);
            return db.GetCollection<T>(collection);
    }

     public string Add(Room policy){

        var collect = mongoCollection<Room>(collectionname);

        collect.InsertOne(policy);

        return "success";
    }

    public Room Read(string i){

        var collect = mongoCollection<Room>(collectionname);

       return collect.Find(x=>x.Id==i).FirstOrDefault();

        
    }

    public List<Room> Rooms(){

    var collect = mongoCollection<Room>(collectionname);    
    return collect.Find(x=>true).ToList();


    }

    public Room edit(string a){

       var collect = mongoCollection<Room>(collectionname);    
       return collect.Find(x=>x.Id==a).FirstOrDefault();    

    }

    public string edit(string a,Room policy){

        var collect = mongoCollection<Room>(collectionname);     
        var filter = Builders<Room>.Filter.Eq(x=>x.Id,a);

        collect.ReplaceOne(filter,policy);

        return "success";
    }


    public string Delete(string id){

       var collect = mongoCollection<Room>(collectionname); 

       collect.DeleteOne(x=>x.Id==id);
       
       return "success";

    }

    public string ConfirmDelete(string id){

       var collect = mongoCollection<Room>(collectionname); 

       collect.Find(x=>x.Id==id).FirstOrDefault();
       
       return "success";

    }

    
}