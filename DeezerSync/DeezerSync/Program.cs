using System;
using System.Collections.Generic;

namespace DeezerSync
{
    public class StandardPlaylist
    {
        public string provider { get; set; }                // Playlist provider (Spotify, Soundcloud, ...)
        public string description { get; set; }             // Playlist description
        public string title { get; set; }                   // Playlist title
        public List<StandardTitle> titel { get; set; }      // A list of all tracks    

    }

    public class StandardTitle
    {
        public string description { get; set; }             // Description of this track
        public int duration { get; set; }                   // The track duration
        public string genre { get; set; }                   // The track genre
        public string title { get; set; }                   // The track title
        public string username { get; set; }                // The username from the uploader
        public string labelname { get; set; }               // The lable of the uploader
    }

    class Program
    {
        public static List<StandardPlaylist> Playlists = new List<StandardPlaylist>();

        static void Main(string[] args)
        {
            //SoundCloud.playlist pl = new SoundCloud.playlist();
            Deezer.API.Playlist dpl = new Deezer.API.Playlist();

            //pl.SetStandardPlaylists().Wait();
            Console.WriteLine(dpl.CreatePlaylist("TEST"));
            Console.ReadKey();
        }
    }
}
