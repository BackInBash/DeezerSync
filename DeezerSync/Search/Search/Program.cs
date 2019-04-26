using Newtonsoft.Json;
using System;

namespace Search
{
    class Program
    {
        static void Main(string[] args)
        {
            Search.SoundCloud.playlist sc = new Search.SoundCloud.playlist();
            var playlists = sc.GetStandardPlaylists().Result;

            // Playlist Loop
            foreach(var i in playlists)
            {
                // Track Loop
                foreach(var a in i.tracks)
                {
                    //Console.WriteLine("Title: "+a.title+ " Artist: "+a.username+ " Duration: "+ a.duration);
                    //Official o = new Official(a.username, a.title, a.duration);
                    //o.finder();
                    RemixTest r = new RemixTest(a.username, a.title, a.duration);
                    r.test();
                }
                //break;
            }
        }
    }
}
