using System.Collections.Generic;

namespace DeezerSync.Deezer.API.Model
{
    public partial class AddSongsToPlaylist
    {
        public string playlist_id { get; set; }
        public List<List<long>> songs { get; set; }
        public long offset { get; set; }

    }
}
