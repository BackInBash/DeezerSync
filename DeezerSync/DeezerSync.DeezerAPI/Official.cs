using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DeezerSync.DeezerAPI
{
    class Official
    {
        private const string Official_api = "https://api.deezer.com/search?q=";
        private readonly HttpClient client = new HttpClient();
    }
}
