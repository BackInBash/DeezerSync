using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

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
        public Track Track { get; set; }
    }

    public partial class Album
    {
        public List<AlbumDatum> Data { get; set; }
        public long? Count { get; set; }
        public long? Total { get; set; }
        public long? FilteredCount { get; set; }
        public List<long> FilteredItems { get; set; }
        public long? Next { get; set; }
    }

    public partial class AlbumDatum
    {
        public long? AlbId { get; set; }
        public string AlbTitle { get; set; }
        public string AlbPicture { get; set; }
        public bool? Available { get; set; }
        public string Version { get; set; }
        public long? ArtId { get; set; }
        public string? ArtName { get; set; }
        public ExplicitContent ExplicitAlbumContent { get; set; }
        public DateTimeOffset? PhysicalReleaseDate { get; set; }
        public long? Type { get; set; }
        public bool? ArtistIsDummy { get; set; }
        public long? NumberTrack { get; set; }
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
        public List<long> FilteredItems { get; set; }
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
        public long? AlbId { get; set; }
        public string AlbTitle { get; set; }
        public long? Type { get; set; }
        public string Md5Origin { get; set; }
        public bool? Video { get; set; }
        public long? Duration { get; set; }
        public string AlbPicture { get; set; }
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
        public string? Version { get; set; }
        public long? ExplicitLyrics { get; set; }
        public string Isrc { get; set; }
        public string HierarchicalTitle { get; set; }
        public SngContributorsUnion? SngContributors { get; set; }
        public long? LyricsId { get; set; }
        public ExplicitContent ExplicitTrackContent { get; set; }
        public DateTimeOffset? DateStart { get; set; }
        public DateTimeOffset? DateStartPremium { get; set; }
        public long? SMod { get; set; }
        public long? SPremium { get; set; }
        public long? Status { get; set; }
        public bool? HasLyrics { get; set; }
    }


    public partial class SngContributorsClass
    {
        public List<string> Artist { get; set; }
        public List<string> MainArtist { get; set; }
        public List<string> Featuring { get; set; }
        public List<string> Composer { get; set; }
        public List<string> Author { get; set; }
        public List<string> Mainartist { get; set; }
        public List<string> Featuredartist { get; set; }
        public List<string> Vocals { get; set; }
        public List<string> Producer { get; set; }
        public List<string> Writer { get; set; }
        public List<string> Musicpublisher { get; set; }
    }




    public partial struct SngContributorsUnion
    {
        public List<object> AnythingArray;
        public SngContributorsClass SngContributorsClass;

        public static implicit operator SngContributorsUnion(List<object> AnythingArray) => new SngContributorsUnion { AnythingArray = AnythingArray };
        public static implicit operator SngContributorsUnion(SngContributorsClass SngContributorsClass) => new SngContributorsUnion { SngContributorsClass = SngContributorsClass };
    }
}