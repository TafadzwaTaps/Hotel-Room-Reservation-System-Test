

using MongoDB.Driver;
using Sample_Hotel_Room_Reservation_System.Models;

public class Cancellationservice{


   private readonly string database="";

    private readonly string collectionname="cancellationpolicy";

    public IMongoCollection<T> mongoCollection<T>(in string collection)
    {
            var connect = new MongoClient("");
            var db = connect.GetDatabase(database);
            return db.GetCollection<T>(collection);

    }


    public string Add(CancellationPolicy policy){

        var collect = mongoCollection<CancellationPolicy>(collectionname);

        collect.InsertOne(policy);

        return "success";
    }

    public string Read(int i){

        var collect = mongoCollection<CancellationPolicy>(collectionname);

        collect.Find(x=>x.id==i).FirstOrDefault();

        return "success";
    }

    public string policies(){

    var collect = mongoCollection<CancellationPolicy>(collectionname);    
    collect.Find(x=>true).ToList();
    
    return "";


    }

    public string edit(int a){

       var collect = mongoCollection<CancellationPolicy>(collectionname);    
       collect.Find(x=>x.id==a).FirstOrDefault();
       
       return "success";

    }

    public string edit(int a,CancellationPolicy policy){

        var collect = mongoCollection<CancellationPolicy>(collectionname);     
        var filter = Builders<CancellationPolicy>.Filter.Eq(x=>x.id,a);

        collect.ReplaceOne(filter,policy);

        return "success";
    }


    public string Delete(int id){

       var collect = mongoCollection<CancellationPolicy>(collectionname); 

       collect.DeleteOne(x=>x.id==id);
       
       return "success";

    }

    public string ConfirmDelete(int id){

       var collect = mongoCollection<CancellationPolicy>(collectionname); 

       collect.Find(x=>x.id==id).FirstOrDefault();
       
       return "success";

    }


        
    





}