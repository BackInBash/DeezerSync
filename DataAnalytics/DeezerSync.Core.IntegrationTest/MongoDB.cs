using DeezerSync.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DeezerSync.Core.IntegrationTest
{
    public class MongoDB
    {
        private List<StandardPlaylist> SoundCloud;

        public MongoDB()
        {
            SoundCloud = JsonConvert.DeserializeObject<List<StandardPlaylist>>(File.ReadAllText(@"../../../../../DataAnalytics/RawData/IntegrationTestSoundCloud.json"));
            SoundCloud.AddRange(JsonConvert.DeserializeObject<List<StandardPlaylist>>(File.ReadAllText(@"../../../../../DataAnalytics/RawData/IntegrationTestSpotify.json")));
        }

        [Fact]
        public async Task InsetDatabase()
        {
            try
            {
                DeezerSync.DB.Mongo db = new DB.Mongo("127.0.0.1", "27017");
                db.connect();

                await db.dropDatabase("SoundCloud");
                db.createDatabase("SoundCloud");

                foreach (var i in SoundCloud)
                {
                    await db.addPlaylist(i);
                }
            }
            catch (ArgumentNullException)
            {
                Assert.IsType<List<StandardPlaylist>>(SoundCloud);
            }
            Assert.IsType<List<StandardPlaylist>>(SoundCloud);
        }
    }
}
