using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace StartupTechStack
{
    public class BaseDbService
    {
        private string _mondoDBName = "StartupTechStack";
        protected string ConnectionString { get; set; }
        protected string MongoConnectionString { get; set; }

        private MongoClient _mongoClient;

     
        public MongoClient MongoClient
        {
            get
            {
                if (null == _mongoClient)
                {
                    _mongoClient = new MongoClient(MongoConnectionString);
                }
                return _mongoClient;
            }
            set { _mongoClient = value; }
        }

        private MongoDatabase _urbanSafariMongoDatabase = null;
        public MongoDatabase UrbanSafariMongoDatabase
        {
            get
            {
                if (null == _urbanSafariMongoDatabase)
                {
                    _urbanSafariMongoDatabase = MongoClient.GetServer().GetDatabase(_mondoDBName);
                }
                return _urbanSafariMongoDatabase;
            }
        }

        public MongoCollection<T> GetCollection<T>()
        {
            return new MongoCollection<T>(UrbanSafariMongoDatabase, new MongoCollectionSettings<T>(UrbanSafariMongoDatabase, typeof(T).Name));
        }

        public BaseDbService()
        {
            MongoConnectionString = ConfigurationManager.AppSettings["MongoServer"];
            _mondoDBName = ConfigurationManager.AppSettings["MongoDatabase"];
        }

        public T QueryById<T>(string key) where T : new()
        {
            return GetCollection<T>().FindOneById(key);
        }

        public T QueryById<T>(int key) where T : new()
        {
            return GetCollection<T>().FindOneById(key);
        }

        public T QueryById<T>(long key) where T : new()
        {
            return GetCollection<T>().FindOneById(key);
        }

        public void SaveDataItemList<T>(List<T> meetupDataItems) where T : new()
        {
            MongoCollection<T> mongoCollection = GetCollection<T>();
            meetupDataItems.ForEach(obj => mongoCollection.Save(obj));
        }

        public void SaveDataItem<T>(T meetupDataItem) where T : new()
        {
            GetCollection<T>().Save(meetupDataItem);
        }

        public void InsertMissingItems<T>(List<T> meetupDataItems) where T : new()
        {
            //            Query<T>.EQ("_id", T.Id);
            //             GetCollection<T>().FindAll()
        }
    }
}
