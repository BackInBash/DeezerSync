using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeezerSync.Deezer.API.Model
{
    public partial class CreatePlaylistResponse
    {
        public List<string> error;
        public long results;
    }
}
