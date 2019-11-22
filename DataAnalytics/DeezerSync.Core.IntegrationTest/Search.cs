using DeezerSync.Log;
using DeezerSync.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using NLog;
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
        public int error = 0;

        [Fact]
        public async Task SearchResults()
        {
            ILogger<Log.NLogger> logger = new Logger<Log.NLogger>(new NullLoggerFactory());
            for (int i = 1; i <= items; i++)
            {
                try
                {
                    var song = JsonConvert.DeserializeObject<DebugResult>(File.ReadAllText(@"../../../../../DataAnalytics/RawData/SearchResults/" + i + ".json"));
                    DeezerSync.Core.Search s = new Core.Search(new DeezerSync.Log.NLogger(logger));
                    long id = await s.search(song.Results, song.Searching);
                    if (id != 0)
                    {
                        found++;
                    }
                }
                catch (Exception e)
                {
                    error++;
                }
            }
            await File.WriteAllTextAsync("SearchResult.txt", "Found " + found + " Songs. Error on " + error + " Songs.");
            Assert.True((error > 10));
        }
    }
}
