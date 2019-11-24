using DeezerSync.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DeezerSync.Core.IntegrationTest
{
    public class DebugFalsePositives
    {
        public int found = 0;
        public int error = 0;

#if DEBUG
        [Fact]
        public async Task FalsePositives()
        {
            ILogger<Log.NLogger> logger = new Logger<Log.NLogger>(new NullLoggerFactory());

            string[] filePaths = Directory.GetFiles(@"../../../../../DataAnalytics/PreparedData/FalsePositives/", "*.json", SearchOption.AllDirectories);
            foreach (var i in filePaths)
            {
                try
                {
                    var song = JsonConvert.DeserializeObject<DebugResult>(File.ReadAllText(i));
                    DeezerSync.Core.Search s = new Core.Search(new DeezerSync.Log.NLogger(logger));
                    List<StandardTitle> list = new List<StandardTitle>();
                    list.Add(song.Reported);

                    long id = await s.search(list, song.Searching);
                    if (id != 0)
                    {
                        found++;
                    }
                }
                catch (Exception)
                {
                    error++;
                }
            }
        }
#endif
    }
}
