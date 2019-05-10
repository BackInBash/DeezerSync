namespace DeezerSync.Deezer.API.Model
{
    public partial class RequestPlaylistData
    {
        public string playlist_id { get; set; }
        public string lang { get; set; }
        public long nb { get; set; }
        public long start { get; set; }
        public long tab { get; set; }
        public bool tags { get; set; }
        public bool header { get; set; }
    }
}
