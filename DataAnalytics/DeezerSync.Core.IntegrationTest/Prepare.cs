using DeezerSync.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DeezerSync.Core.IntegrationTest
{

    public class Prepare
    {
        private List<StandardPlaylist> SoundCloud;

        public Prepare()
        {
            SoundCloud = JsonConvert.DeserializeObject<List<StandardPlaylist>>(File.ReadAllText(@"../../../../../DataAnalytics/RawData/IntegrationTestSoundCloud.json"));
            SoundCloud.AddRange(JsonConvert.DeserializeObject<List<StandardPlaylist>>(File.ReadAllText(@"../../../../../DataAnalytics/RawData/IntegrationTestSpotify.json")));
        }

        [Fact]
        public void TypeCheck()
        {
            Assert.IsType<List<StandardPlaylist>>(SoundCloud);
        }

        [Fact]
        public async Task Integration()
        {
            ILogger<Log.NLogger> logger = new Logger<Log.NLogger>(new NullLoggerFactory());
            DeezerSync.Core.Prepare p = new Core.Prepare(new DeezerSync.Log.NLogger(logger));

            // Playlist List
            List<StandardPlaylist> Playlist = new List<StandardPlaylist>();
            foreach (var i in SoundCloud)
            {
                // Track List
                List<StandardTitle> Titel = new List<StandardTitle>();
                foreach (var a in i.tracks)
                {
                    Titel.Add(await p.PrepareDeezerQuery(a));
                }
                Playlist.Add(new StandardPlaylist { description = i.description, id = i.id, provider = i.provider, title = i.title, tracks = Titel });
            }
            await File.WriteAllTextAsync(@"../../../../../DataAnalytics/RawData/PrepareResult.json", JsonConvert.SerializeObject(Playlist, Formatting.Indented));
        }
    }
}
