using DeezerSync.Model;
using System;
using System.Collections.Generic;

namespace DeezerSync
{
    class Program
    {
        public static List<StandardPlaylist> Playlists = new List<StandardPlaylist>();

        static void Main(string[] args)
        {
            //SoundCloud.playlist pl = new SoundCloud.playlist();
            long[] arr = { 652401012, 137203734, 71407527 };
            Deezer.Playlist dpl = new Deezer.Playlist();

            //pl.SetStandardPlaylists().Wait();
            var res = dpl.GetAllTracksInPlaylist(4081775206);
        }
    }
}
