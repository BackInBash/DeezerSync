using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DeezerSync.Models.API
{
    public partial class SearchResult
    {
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public object[] Error { get; set; }

        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public Results Results { get; set; }
    }

    public partial class Results
    {
        [JsonProperty("QUERY", NullValueHandling = NullValueHandling.Ignore)]
        public string Query { get; set; }

        [JsonProperty("FUZZINNESS", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Fuzzinness { get; set; }

        [JsonProperty("AUTOCORRECT", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Autocorrect { get; set; }

        [JsonProperty("ORDER", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Order { get; set; }

        [JsonProperty("TOP_RESULT", NullValueHandling = NullValueHandling.Ignore)]
        public object[] TopResult { get; set; }

        [JsonProperty("ARTIST", NullValueHandling = NullValueHandling.Ignore)]
        public Album Artist { get; set; }

        [JsonProperty("ALBUM", NullValueHandling = NullValueHandling.Ignore)]
        public Album Album { get; set; }

        [JsonProperty("TRACK", NullValueHandling = NullValueHandling.Ignore)]
        public Track Track { get; set; }

        [JsonProperty("PLAYLIST", NullValueHandling = NullValueHandling.Ignore)]
        public Playlist Playlist { get; set; }

        [JsonProperty("RADIO", NullValueHandling = NullValueHandling.Ignore)]
        public Album Radio { get; set; }

        [JsonProperty("SHOW", NullValueHandling = NullValueHandling.Ignore)]
        public Album Show { get; set; }

        [JsonProperty("CHANNEL", NullValueHandling = NullValueHandling.Ignore)]
        public Audiobook Channel { get; set; }

        [JsonProperty("LIVESTREAM", NullValueHandling = NullValueHandling.Ignore)]
        public Album Livestream { get; set; }

        [JsonProperty("AUDIOBOOK", NullValueHandling = NullValueHandling.Ignore)]
        public Audiobook Audiobook { get; set; }

        [JsonProperty("EPISODE", NullValueHandling = NullValueHandling.Ignore)]
        public Album Episode { get; set; }
    }

    public partial class Album
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public AlbumDatum[] Data { get; set; }

        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty("filtered_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? FilteredCount { get; set; }

        [JsonProperty("filtered_items", NullValueHandling = NullValueHandling.Ignore)]
        public object[] FilteredItems { get; set; }

        [JsonProperty("next", NullValueHandling = NullValueHandling.Ignore)]
        public long? Next { get; set; }
    }

    public partial class AlbumDatum
    {
        [JsonProperty("ALB_ID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? AlbId { get; set; }

        [JsonProperty("ALB_TITLE", NullValueHandling = NullValueHandling.Ignore)]
        public string AlbTitle { get; set; }

        [JsonProperty("ARTISTS", NullValueHandling = NullValueHandling.Ignore)]
        public Artist[] Artists { get; set; }

        [JsonProperty("AVAILABLE", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Available { get; set; }

        [JsonProperty("VERSION", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        [JsonProperty("ART_ID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ArtId { get; set; }

        [JsonProperty("ART_NAME", NullValueHandling = NullValueHandling.Ignore)]
        public ArtName? ArtName { get; set; }

        [JsonProperty("EXPLICIT_ALBUM_CONTENT", NullValueHandling = NullValueHandling.Ignore)]
        public ExplicitContent ExplicitAlbumContent { get; set; }

        [JsonProperty("PHYSICAL_RELEASE_DATE", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? PhysicalReleaseDate { get; set; }

        [JsonProperty("TYPE", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Type { get; set; }

        [JsonProperty("ARTIST_IS_DUMMY", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ArtistIsDummy { get; set; }

        [JsonProperty("NUMBER_TRACK", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? NumberTrack { get; set; }

        [JsonProperty("__TYPE__", NullValueHandling = NullValueHandling.Ignore)]
        public string DatumType { get; set; }
    }

    public partial class Artist
    {
        [JsonProperty("ART_ID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ArtId { get; set; }

        [JsonProperty("ROLE_ID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? RoleId { get; set; }

        [JsonProperty("ARTISTS_ALBUMS_ORDER", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ArtistsAlbumsOrder { get; set; }

        [JsonProperty("ART_NAME", NullValueHandling = NullValueHandling.Ignore)]
        public ArtName? ArtName { get; set; }

        [JsonProperty("RANK", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Rank { get; set; }

        [JsonProperty("LOCALES", NullValueHandling = NullValueHandling.Ignore)]
        public LocalesUnion? Locales { get; set; }

        [JsonProperty("ARTIST_IS_DUMMY", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ArtistIsDummy { get; set; }

        [JsonProperty("__TYPE__", NullValueHandling = NullValueHandling.Ignore)]
        public TypeEnum? Type { get; set; }

        [JsonProperty("ARTISTS_SONGS_ORDER", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ArtistsSongsOrder { get; set; }

        [JsonProperty("SMARTRADIO", NullValueHandling = NullValueHandling.Ignore)]
        public long? Smartradio { get; set; }
    }

    public partial class LocalesClass
    {
        [JsonProperty("lang_ja-jpan", NullValueHandling = NullValueHandling.Ignore)]
        public LangJa LangJaJpan { get; set; }

        [JsonProperty("lang_ja-hrkt", NullValueHandling = NullValueHandling.Ignore)]
        public LangJa LangJaHrkt { get; set; }

        [JsonProperty("lang_ja-kana", NullValueHandling = NullValueHandling.Ignore)]
        public LangJa LangJaKana { get; set; }
    }

    public partial class LangJa
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public Name? Name { get; set; }
    }

    public partial class ExplicitContent
    {
        [JsonProperty("EXPLICIT_LYRICS_STATUS", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExplicitLyricsStatus { get; set; }

        [JsonProperty("EXPLICIT_COVER_STATUS", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExplicitCoverStatus { get; set; }
    }

    public partial class Audiobook
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public object[] Data { get; set; }

        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }
    }

    public partial class Playlist
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public PlaylistDatum[] Data { get; set; }

        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty("filtered_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? FilteredCount { get; set; }

        [JsonProperty("filtered_items", NullValueHandling = NullValueHandling.Ignore)]
        public object[] FilteredItems { get; set; }

        [JsonProperty("next", NullValueHandling = NullValueHandling.Ignore)]
        public long? Next { get; set; }
    }

    public partial class PlaylistDatum
    {
        [JsonProperty("PLAYLIST_ID", NullValueHandling = NullValueHandling.Ignore)]
        public string PlaylistId { get; set; }

        [JsonProperty("PARENT_PLAYLIST_ID", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentPlaylistId { get; set; }

        [JsonProperty("TYPE", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Type { get; set; }

        [JsonProperty("TITLE", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("PARENT_USER_ID", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentUserId { get; set; }

        [JsonProperty("PARENT_USERNAME", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentUsername { get; set; }

        [JsonProperty("NB_SONG", NullValueHandling = NullValueHandling.Ignore)]
        public long? NbSong { get; set; }

        [JsonProperty("HAS_ARTIST_LINKED", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasArtistLinked { get; set; }

        [JsonProperty("__TYPE__", NullValueHandling = NullValueHandling.Ignore)]
        public string DatumType { get; set; }
    }

    public partial class Track
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public TrackDatum[] Data { get; set; }

        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty("filtered_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? FilteredCount { get; set; }

        [JsonProperty("filtered_items", NullValueHandling = NullValueHandling.Ignore)]
        public long[] FilteredItems { get; set; }

        [JsonProperty("next", NullValueHandling = NullValueHandling.Ignore)]
        public long? Next { get; set; }
    }

    public partial class TrackDatum
    {
        [JsonProperty("SNG_ID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? SngId { get; set; }

        [JsonProperty("PRODUCT_TRACK_ID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ProductTrackId { get; set; }

        [JsonProperty("UPLOAD_ID", NullValueHandling = NullValueHandling.Ignore)]
        public long? UploadId { get; set; }

        [JsonProperty("SNG_TITLE", NullValueHandling = NullValueHandling.Ignore)]
        public string SngTitle { get; set; }

        [JsonProperty("ART_ID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ArtId { get; set; }

        [JsonProperty("PROVIDER_ID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ProviderId { get; set; }

        [JsonProperty("ART_NAME", NullValueHandling = NullValueHandling.Ignore)]
        public ArtName? ArtName { get; set; }

        [JsonProperty("ARTIST_IS_DUMMY", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ArtistIsDummy { get; set; }

        [JsonProperty("ARTISTS", NullValueHandling = NullValueHandling.Ignore)]
        public Artist[] Artists { get; set; }

        [JsonProperty("ALB_ID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? AlbId { get; set; }

        [JsonProperty("ALB_TITLE", NullValueHandling = NullValueHandling.Ignore)]
        public string AlbTitle { get; set; }

        [JsonProperty("TYPE", NullValueHandling = NullValueHandling.Ignore)]
        public long? Type { get; set; }

        [JsonProperty("MD5_ORIGIN", NullValueHandling = NullValueHandling.Ignore)]
        public string Md5Origin { get; set; }

        [JsonProperty("VIDEO", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Video { get; set; }

        [JsonProperty("DURATION", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Duration { get; set; }

        [JsonProperty("RANK_SNG", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? RankSng { get; set; }

        [JsonProperty("SMARTRADIO", NullValueHandling = NullValueHandling.Ignore)]
        public long? Smartradio { get; set; }

        [JsonProperty("FILESIZE_AAC_64", NullValueHandling = NullValueHandling.Ignore)]
        public long? FilesizeAac64 { get; set; }

        [JsonProperty("FILESIZE_MP3_64", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? FilesizeMp364 { get; set; }

        [JsonProperty("FILESIZE_MP3_128", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? FilesizeMp3128 { get; set; }

        [JsonProperty("FILESIZE_MP3_256", NullValueHandling = NullValueHandling.Ignore)]
        public long? FilesizeMp3256 { get; set; }

        [JsonProperty("FILESIZE_MP3_320", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? FilesizeMp3320 { get; set; }

        [JsonProperty("FILESIZE_FLAC", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? FilesizeFlac { get; set; }

        [JsonProperty("FILESIZE", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Filesize { get; set; }

        [JsonProperty("GAIN", NullValueHandling = NullValueHandling.Ignore)]
        public string Gain { get; set; }

        [JsonProperty("MEDIA_VERSION", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? MediaVersion { get; set; }

        [JsonProperty("DISK_NUMBER", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? DiskNumber { get; set; }

        [JsonProperty("TRACK_NUMBER", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? TrackNumber { get; set; }

        [JsonProperty("TRACK_TOKEN", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackToken { get; set; }

        [JsonProperty("TRACK_TOKEN_EXPIRE", NullValueHandling = NullValueHandling.Ignore)]
        public long? TrackTokenExpire { get; set; }

        [JsonProperty("VERSION", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        [JsonProperty("MEDIA", NullValueHandling = NullValueHandling.Ignore)]
        public Media[] Media { get; set; }

        [JsonProperty("EXPLICIT_LYRICS", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ExplicitLyrics { get; set; }

        [JsonProperty("RIGHTS", NullValueHandling = NullValueHandling.Ignore)]
        public Rights Rights { get; set; }

        [JsonProperty("ISRC", NullValueHandling = NullValueHandling.Ignore)]
        public string Isrc { get; set; }

        [JsonProperty("HIERARCHICAL_TITLE", NullValueHandling = NullValueHandling.Ignore)]
        public string HierarchicalTitle { get; set; }

        [JsonProperty("SNG_CONTRIBUTORS", NullValueHandling = NullValueHandling.Ignore)]
        public SngContributors SngContributors { get; set; }

        [JsonProperty("LYRICS_ID", NullValueHandling = NullValueHandling.Ignore)]
        public long? LyricsId { get; set; }

        [JsonProperty("EXPLICIT_TRACK_CONTENT", NullValueHandling = NullValueHandling.Ignore)]
        public ExplicitContent ExplicitTrackContent { get; set; }

        [JsonProperty("DATE_START", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateStart { get; set; }

        [JsonProperty("DATE_START_PREMIUM", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateStartPremium { get; set; }

        [JsonProperty("S_MOD", NullValueHandling = NullValueHandling.Ignore)]
        public long? SMod { get; set; }

        [JsonProperty("S_PREMIUM", NullValueHandling = NullValueHandling.Ignore)]
        public long? SPremium { get; set; }

        [JsonProperty("STATUS", NullValueHandling = NullValueHandling.Ignore)]
        public long? Status { get; set; }

        [JsonProperty("HAS_LYRICS", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasLyrics { get; set; }

        [JsonProperty("__TYPE__", NullValueHandling = NullValueHandling.Ignore)]
        public string DatumType { get; set; }
    }

    public partial class Media
    {
        [JsonProperty("TYPE", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("HREF", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Href { get; set; }
    }

    public partial class Rights
    {
        [JsonProperty("STREAM_ADS_AVAILABLE", NullValueHandling = NullValueHandling.Ignore)]
        public bool? StreamAdsAvailable { get; set; }

        [JsonProperty("STREAM_ADS", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StreamAds { get; set; }

        [JsonProperty("STREAM_SUB_AVAILABLE", NullValueHandling = NullValueHandling.Ignore)]
        public bool? StreamSubAvailable { get; set; }

        [JsonProperty("STREAM_SUB", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StreamSub { get; set; }
    }

    public partial class SngContributors
    {
        [JsonProperty("main_artist", NullValueHandling = NullValueHandling.Ignore)]
        public ArtName[] MainArtist { get; set; }

        [JsonProperty("featuring", NullValueHandling = NullValueHandling.Ignore)]
        public ArtName[] Featuring { get; set; }

        [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
        public Arranger[] Author { get; set; }

        [JsonProperty("mainartist", NullValueHandling = NullValueHandling.Ignore)]
        public ArtName[] Mainartist { get; set; }

        [JsonProperty("featuredartist", NullValueHandling = NullValueHandling.Ignore)]
        public ArtName[] Featuredartist { get; set; }

        [JsonProperty("masterer", NullValueHandling = NullValueHandling.Ignore)]
        public Arranger[] Masterer { get; set; }

        [JsonProperty("mixer", NullValueHandling = NullValueHandling.Ignore)]
        public Arranger[] Mixer { get; set; }

        [JsonProperty("producer", NullValueHandling = NullValueHandling.Ignore)]
        public Arranger[] Producer { get; set; }

        [JsonProperty("arranger", NullValueHandling = NullValueHandling.Ignore)]
        public Arranger[] Arranger { get; set; }

        [JsonProperty("writer", NullValueHandling = NullValueHandling.Ignore)]
        public Arranger[] Writer { get; set; }

        [JsonProperty("additional keyboards", NullValueHandling = NullValueHandling.Ignore)]
        public string[] AdditionalKeyboards { get; set; }
    }

    public enum ArtName { Kaskade, TessComrie };

    public enum Name { カスケイド, カスケード };

    public enum TypeEnum { Artist };

    public enum Arranger { FinnBjarnson, KennethNathanielPyfer, RyanRaddon };

    public partial struct LocalesUnion
    {
        public object[] AnythingArray;
        public LocalesClass LocalesClass;

        public static implicit operator LocalesUnion(object[] AnythingArray) => new LocalesUnion { AnythingArray = AnythingArray };
        public static implicit operator LocalesUnion(LocalesClass LocalesClass) => new LocalesUnion { LocalesClass = LocalesClass };
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ArtNameConverter.Singleton,
                LocalesUnionConverter.Singleton,
                NameConverter.Singleton,
                TypeEnumConverter.Singleton,
                ArrangerConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class ArtNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ArtName) || t == typeof(ArtName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Kaskade":
                    return ArtName.Kaskade;
                case "Tess Comrie":
                    return ArtName.TessComrie;
            }
            throw new Exception("Cannot unmarshal type ArtName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ArtName)untypedValue;
            switch (value)
            {
                case ArtName.Kaskade:
                    serializer.Serialize(writer, "Kaskade");
                    return;
                case ArtName.TessComrie:
                    serializer.Serialize(writer, "Tess Comrie");
                    return;
            }
            throw new Exception("Cannot marshal type ArtName");
        }

        public static readonly ArtNameConverter Singleton = new ArtNameConverter();
    }

    internal class LocalesUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(LocalesUnion) || t == typeof(LocalesUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<LocalesClass>(reader);
                    return new LocalesUnion { LocalesClass = objectValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<object[]>(reader);
                    return new LocalesUnion { AnythingArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type LocalesUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (LocalesUnion)untypedValue;
            if (value.AnythingArray != null)
            {
                serializer.Serialize(writer, value.AnythingArray);
                return;
            }
            if (value.LocalesClass != null)
            {
                serializer.Serialize(writer, value.LocalesClass);
                return;
            }
            throw new Exception("Cannot marshal type LocalesUnion");
        }

        public static readonly LocalesUnionConverter Singleton = new LocalesUnionConverter();
    }

    internal class NameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Name) || t == typeof(Name?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "カスケイド":
                    return Name.カスケイド;
                case "カスケード":
                    return Name.カスケード;
            }
            throw new Exception("Cannot unmarshal type Name");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Name)untypedValue;
            switch (value)
            {
                case Name.カスケイド:
                    serializer.Serialize(writer, "カスケイド");
                    return;
                case Name.カスケード:
                    serializer.Serialize(writer, "カスケード");
                    return;
            }
            throw new Exception("Cannot marshal type Name");
        }

        public static readonly NameConverter Singleton = new NameConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "artist")
            {
                return TypeEnum.Artist;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            if (value == TypeEnum.Artist)
            {
                serializer.Serialize(writer, "artist");
                return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }

    internal class ArrangerConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Arranger) || t == typeof(Arranger?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Finn Bjarnson":
                    return Arranger.FinnBjarnson;
                case "Kenneth Nathaniel Pyfer":
                    return Arranger.KennethNathanielPyfer;
                case "Ryan Raddon":
                    return Arranger.RyanRaddon;
            }
            throw new Exception("Cannot unmarshal type Arranger");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Arranger)untypedValue;
            switch (value)
            {
                case Arranger.FinnBjarnson:
                    serializer.Serialize(writer, "Finn Bjarnson");
                    return;
                case Arranger.KennethNathanielPyfer:
                    serializer.Serialize(writer, "Kenneth Nathaniel Pyfer");
                    return;
                case Arranger.RyanRaddon:
                    serializer.Serialize(writer, "Ryan Raddon");
                    return;
            }
            throw new Exception("Cannot marshal type Arranger");
        }

        public static readonly ArrangerConverter Singleton = new ArrangerConverter();
    }
}