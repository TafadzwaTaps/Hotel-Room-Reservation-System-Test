
using MongoDB.Driver;

public class Facilitiesservice{


    private readonly string database="userTest";

    private readonly string collectionname="userManager";

    public IMongoCollection<T> mongoCollection<T>(in string collection)
    {
            var connect = new MongoClient("");
            var db = connect.GetDatabase(database);
            return db.GetCollection<T>(collection);

    }




}