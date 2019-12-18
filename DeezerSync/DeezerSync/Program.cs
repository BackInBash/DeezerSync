using Newtonsoft.Json;
using System;
using System.IO;

namespace DeezerSync
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine(
                " _____                               _____                     \n" +
                "|  __ \\                             / ____|\n" +
"| |  | |  ___   ___  ____ ___  _ __| (___   _   _  _ __    ___ \n" +
"| |  | | / _ \\ / _ \\|_  // _ \\| '__|\\___ \\ | | | || '_ \\  / __|\n" +
"| |__| ||  __/|  __/ / /|  __/| |   ____) || |_| || | | || (__ \n" +
"|_____/  \\___| \\___|/___|\\___||_|  |_____/  \\__, ||_| |_| \\___|\n" +
"                                             __/ |             \n" +
"                                            |___/");


            Console.WriteLine("Starting DeezerSync on " + System.Runtime.InteropServices.RuntimeInformation.OSDescription);

            // Read Config from File
            Config config = new Config();

            // Get Playlist Data
            DeezerSync.MusicProvider.main musicprovider_playlist = null;
            DeezerSync.DeezerAPI.Private api = new DeezerAPI.Private(config.deezer_secret);

            if (string.IsNullOrEmpty(config.soundcloud_clientid))
            {
                musicprovider_playlist = new MusicProvider.main(config.soundcloud_profile, config.spotify_profile, config.spotify_secret);
            }
            else
            {
                musicprovider_playlist = new MusicProvider.main(config.soundcloud_profile, config.spotify_profile, config.spotify_secret, config.soundcloud_clientid);
            }
            // Save to DB
            if(!string.IsNullOrEmpty(config.db_port) && !string.IsNullOrEmpty(config.db_ip))
            {
                DB.Mongo db = new DB.Mongo(config.db_ip, config.db_port);

                db.connect();
                await db.dropDatabase("SoundCloud");
                db.createDatabase("SoundCloud");

                foreach (var i in musicprovider_playlist.Data)
                {
                    await db.addPlaylist(i);
                }
            }
            // Start Search
            DeezerSync.Core.Search core = new DeezerSync.Core.Search(musicprovider_playlist.Data, await api.GetAllPlaylistsasync());
            await core.Start();

        }
    }
}
