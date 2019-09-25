using System;
using System.Collections.Generic;
using System.Text;

namespace DeezerSync.Core.Models
{
    public partial class StandardPlaylist
    {
        public string provider { get; set; }                // Playlist provider (Spotify, Soundcloud, ...)
        public string description { get; set; }             // Playlist description
        public string title { get; set; }                   // Playlist title
        public List<StandardTitle> tracks { get; set; }     // A list of all tracks
        public string id { get; set; }                      // Playlist ID

    }
}
