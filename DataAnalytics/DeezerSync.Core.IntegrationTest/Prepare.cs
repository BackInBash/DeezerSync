using DeezerSync.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System;
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
        public async Task PrepareStrings()
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

        [Fact]
        public async Task TrackChanges()
        {

            // Read Values
            var Playlist = JsonConvert.DeserializeObject<List<StandardPlaylist>>(File.ReadAllText(@"../../../../../DataAnalytics/RawData/PrepareResult.json"));

            int count = 0;
            int track = 0;
            int artist = 0;
            int remixArtist = 0;
            int isRemix = 0;
            int unchanged = 0;

            for (int i = 0; i < Playlist.Count; i++)
            {
                for (int a = 0; a < Playlist[i].tracks.Count; a++)
                {
                    bool changed = false;
                    if (!Playlist[i].tracks[a].title.Equals(SoundCloud[i].tracks[a].title))
                    {
                        track++;
                        changed = true;
                    }
                    if ((Playlist[i].tracks[a].artist ?? string.Empty) != (SoundCloud[i].tracks[a].artist ?? string.Empty))
                    {
                        artist++;
                        changed = true;
                    }
                    if ((Playlist[i].tracks[a].remixArtist ?? string.Empty) != (SoundCloud[i].tracks[a].remixArtist ?? string.Empty))
                    {
                        remixArtist++;
                        changed = true;
                    }
                    if (!Playlist[i].tracks[a].isRemix.Equals(SoundCloud[i].tracks[a].isRemix))
                    {
                        isRemix++;
                        changed = true;
                    }
                    if (!changed)
                    {
                        unchanged++;
                    }
                    count++;
                }
            }
            await File.AppendAllTextAsync(@"../../../../../DataAnalytics/PrepareSearch.csv", DateTime.Now.ToString() + ", " + count + ", " + track + ", " + artist + ", " + remixArtist + ", " + isRemix + ", " + unchanged + "\n");
        }
    }
}
