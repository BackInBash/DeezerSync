using System.Collections.Generic;

namespace DeezerSync.Models.API
{
    public partial class CreatePlaylist
    {
        public string title;
        public string description;
        public int status;
        public bool songs;
        public List<string> tags;
    }
}
