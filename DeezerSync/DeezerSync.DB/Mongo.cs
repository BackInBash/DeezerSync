using System;
using System.Threading.Tasks;
using DeezerSync.Models;
using MongoDB.Driver;

namespace DeezerSync.DB
{
    public class Mongo
    {
        private MongoClient client;
        private IMongoDatabase db;
        private const string prefix = "DeezerSync_";
        private string ip;
        private string port;

        public Mongo(string ip, string port)
        {
            this.ip = ip;
            this.port = port;
        }

        /// <summary>
        /// Connect to Database
        /// </summary>
        public void connect()
        {
            client = new MongoClient("mongodb://"+ip+":"+port);
        }

        /// <summary>
        /// Drop Database
        /// </summary>
        /// <param name="db">DB Name</param>
        /// <returns></returns>
        public async Task dropDatabase(string db)
        {
            await client.DropDatabaseAsync(prefix + db);
        }

        /// <summary>
        /// Create or set Active Database
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public void createDatabase(string db)
        {
            this.db = client.GetDatabase(prefix + db);
        }

        /// <summary>
        /// Add Track to Database
        /// </summary>
        /// <param name="data">Track Object</param>
        /// <returns></returns>
        public async Task addTrack(StandardTitle data)
        {
            var input = db.GetCollection<StandardTitle>(data.title.Normalize().Replace(" ", "_", StringComparison.InvariantCultureIgnoreCase));
            await input.InsertOneAsync(data);
        }

        /// <summary>
        /// Add Playlist to Database
        /// </summary>
        /// <param name="data">Playlist Object</param>
        /// <returns></returns>
        public async Task addPlaylist(StandardPlaylist data)
        {
            var input = db.GetCollection<StandardPlaylist>(data.title.Normalize().Replace(" ", "_", StringComparison.InvariantCultureIgnoreCase));
            await input.InsertOneAsync(data);
        }

    }
}
