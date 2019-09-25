using Newtonsoft.Json;
using System;

namespace Search
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Title = "DeezerSync Search Test";

            // Read Config from File
            Config config = new Config();
            config.Read();

            Search.SoundCloud.playlist sc = new Search.SoundCloud.playlist();
            var playlists = sc.GetStandardPlaylists().Result;
            int counter = 0;

            // Playlist Loop
            foreach (var i in playlists)
            {
                // Track Loop
                foreach (var a in i.tracks)
                {
                    //Console.WriteLine("Title: "+a.title+ " Artist: "+a.username+ " Duration: "+ a.duration);
                    //Official o = new Official(a.username, a.title, a.duration);
                    //o.finder();
                    //RemixTest r = new RemixTest(a.username, a.title, a.duration);
                    //r.test();
                    Official o = new Official(a.username, a.title, a.duration);
                    if(o.finder() != 0)
                    {
                        counter++;
                    }
                }
                Console.WriteLine("Found Tracks: " + counter + " in " + i.title);
                break;
            }
            
        }
    }
}
