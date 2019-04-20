using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeezerSync.Deezer.API.Model
{
    class CreatePlaylist
    {
        public string title;
        public string description;
        public int status;
        public bool songs;
        public List<string> tags;
    }
}
