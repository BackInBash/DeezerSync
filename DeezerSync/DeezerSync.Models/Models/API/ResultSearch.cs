using System;
using System.Collections.Generic;

namespace DeezerSync.Core.Models.API
{
    class ResultSearch
    {
        public partial class Search
        {
            public List<Datum> Data { get; set; }
            public long? Total { get; set; }
            public Uri Next { get; set; }
        }

        public partial class Datum
        {
            public long? Id { get; set; }
            public bool? Readable { get; set; }
            public string Title { get; set; }
            public string TitleShort { get; set; }
            public TitleVersion? TitleVersion { get; set; }
            public Uri Link { get; set; }
            public long? Duration { get; set; }
            public long? Rank { get; set; }
            public bool? ExplicitLyrics { get; set; }
            public long? ExplicitContentLyrics { get; set; }
            public long? ExplicitContentCover { get; set; }
            public Uri Preview { get; set; }
            public Artist Artist { get; set; }
            public Album Album { get; set; }
            public DatumType? Type { get; set; }
        }

        public partial class Album
        {
            public long? Id { get; set; }
            public string Title { get; set; }
            public Uri Cover { get; set; }
            public Uri CoverSmall { get; set; }
            public Uri CoverMedium { get; set; }
            public Uri CoverBig { get; set; }
            public Uri CoverXl { get; set; }
            public Uri Tracklist { get; set; }
            public AlbumType? Type { get; set; }
        }

        public partial class Artist
        {
            public long? Id { get; set; }
            public string Name { get; set; }
            public Uri Link { get; set; }
            public Uri Picture { get; set; }
            public Uri PictureSmall { get; set; }
            public Uri PictureMedium { get; set; }
            public Uri PictureBig { get; set; }
            public Uri PictureXl { get; set; }
            public Uri Tracklist { get; set; }
            public ArtistType? Type { get; set; }
        }

        public enum AlbumType { Album };

        public enum ArtistType { Artist };

        public enum TitleVersion { Empty, MusicFromTheMotionPicture };

        public enum DatumType { Track };
    }
}