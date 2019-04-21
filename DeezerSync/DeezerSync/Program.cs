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
            SoundCloud.playlist pl = new SoundCloud.playlist();

            Deezer.Search s = new Deezer.Search(pl.GetStandardPlaylists().Result, Deezer.Playlist.GetAllPlaylists());
            s.Title();
        }
    }
}
