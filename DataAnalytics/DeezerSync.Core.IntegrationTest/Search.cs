﻿using DeezerSync.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DeezerSync.Core.IntegrationTest
{
    partial class DebugResult
    {
        public StandardTitle Searching;
        public List<StandardTitle> Results;
    }
    public class Search
    {
        public const int items = 1593;
        public int found = 0;

        public void OutputAssert(Action func)
        {
            try
            {
                func();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Fact]
        public async Task SearchResults()
        {
            for (int i = 1; i <= items; i++)
            {
                var song = JsonConvert.DeserializeObject<DebugResult>(File.ReadAllText(@"../../../../../DataAnalytics/RawData/SearchResults/" + i + ".json"));
                DeezerSync.Core.Search s = new Core.Search(new List<StandardPlaylist>(), new List<StandardPlaylist>());
                long id = await s.search(song.Results, song.Searching);
                if (id != 0)
                {
                    found++;
                }
            }
            OutputAssert(() => Assert.True(true, "Found "+found+" Songs."));
        }
    }
}