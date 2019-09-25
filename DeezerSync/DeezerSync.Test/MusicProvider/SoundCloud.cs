using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DeezerSync.Test.MusicProvider
{
    public class SoundCloud
    {
        [Theory]
        [InlineData("omg-network-radio")]
        public async Task CheckPlaylistType(string username)
        {
            DeezerSync.MusicProvider.SoundCloud sc = new DeezerSync.MusicProvider.SoundCloud(username);
            var res = await sc.GetStandardPlaylists();

            Assert.IsType<List<Models.StandardPlaylist>>(res);
        }
    }
}
