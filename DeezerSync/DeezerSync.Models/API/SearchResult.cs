using System;
using System.Collections.Generic;

namespace DeezerSync.Models.API
{
    public partial class SearchResult
    {
        public List<object> Error { get; set; }
        public Results Results { get; set; }
    }

    public partial class Results
    {
        public string Query { get; set; }
        public bool? Fuzzinness { get; set; }
        public bool? Autocorrect { get; set; }
        public List<string> Order { get; set; }
        public List<object> TopResult { get; set; }
        public Track Track { get; set; }
    }


    public partial class Artist
    {
        public long? ArtId { get; set; }
        public long? RoleId { get; set; }
        public long? ArtistsAlbumsOrder { get; set; }
        public string ArtName { get; set; }
        public string ArtPicture { get; set; }
        public long? Rank { get; set; }
        public List<object> Locales { get; set; }
        public bool? ArtistIsDummy { get; set; }
        public string Type { get; set; }
        public long? ArtistsSongsOrder { get; set; }
        public long? Smartradio { get; set; }
    }

    public partial class ExplicitContent
    {
        public long? ExplicitLyricsStatus { get; set; }
        public long? ExplicitCoverStatus { get; set; }
    }


    public partial class Track
    {
        public List<TrackDatum> Data { get; set; }
        public long? Count { get; set; }
        public long? Total { get; set; }
        public long? FilteredCount { get; set; }
        public List<object> FilteredItems { get; set; }
        public long? Next { get; set; }
    }

    public partial class TrackDatum
    {
        public long? SngId { get; set; }
        public long? ProductTrackId { get; set; }
        public long? UploadId { get; set; }
        public string SngTitle { get; set; }
        public long? ArtId { get; set; }
        public long? ProviderId { get; set; }
        public string ArtName { get; set; }
        public bool? ArtistIsDummy { get; set; }
        public List<Artist> Artists { get; set; }
        public long? AlbId { get; set; }
        public string AlbTitle { get; set; }
        public long? Type { get; set; }
        public string Md5Origin { get; set; }
        public bool? Video { get; set; }
        public long? Duration { get; set; }
        public Picture? AlbPicture { get; set; }
        public string ArtPicture { get; set; }
        public long? RankSng { get; set; }
        public long? Smartradio { get; set; }
        public long? FilesizeAac64 { get; set; }
        public long? FilesizeMp364 { get; set; }
        public long? FilesizeMp3128 { get; set; }
        public long? FilesizeMp3256 { get; set; }
        public long? FilesizeMp3320 { get; set; }
        public long? FilesizeFlac { get; set; }
        public long? Filesize { get; set; }
        public string Gain { get; set; }
        public long? MediaVersion { get; set; }
        public long? DiskNumber { get; set; }
        public long? TrackNumber { get; set; }
        public string TrackToken { get; set; }
        public long? TrackTokenExpire { get; set; }
        public string Version { get; set; }
        public List<Media> Media { get; set; }
        public long? ExplicitLyrics { get; set; }
        public Rights Rights { get; set; }
        public string Isrc { get; set; }
        public string HierarchicalTitle { get; set; }
        public SngContributors SngContributors { get; set; }
        public long? LyricsId { get; set; }
        public ExplicitContent ExplicitTrackContent { get; set; }
        public DateTimeOffset? DateStart { get; set; }
        public DateTimeOffset? DateStartPremium { get; set; }
        public long? SMod { get; set; }
        public long? SPremium { get; set; }
        public long? Status { get; set; }
        public bool? HasLyrics { get; set; }
        public string DatumType { get; set; }
    }

    public partial class Media
    {
        public string Type { get; set; }
        public Uri Href { get; set; }
    }

    public partial class Rights
    {
        public bool? StreamAdsAvailable { get; set; }
        public DateTimeOffset? StreamAds { get; set; }
        public bool? StreamSubAvailable { get; set; }
        public DateTimeOffset? StreamSub { get; set; }
    }

    public partial class SngContributors
    {
        public List<string> Artist { get; set; }
        public List<string> MainArtist { get; set; }
        public List<string> Composer { get; set; }
    }

    public enum Picture { The86E3748C3057Ec454Fffbc2190A2Bffc };
}