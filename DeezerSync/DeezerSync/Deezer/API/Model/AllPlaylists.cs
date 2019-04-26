using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DeezerSync.Deezer.API.Model
{
   public partial class AllPlaylists
    {
        public partial class Request
        {
            [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
            public List<object> Error { get; set; }

            [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
            public Results Results { get; set; }
        }

        public partial class Results
        {
            [JsonProperty("TAB", NullValueHandling = NullValueHandling.Ignore)]
            public Tab Tab { get; set; }

            [JsonProperty("DATA", NullValueHandling = NullValueHandling.Ignore)]
            public Data Data { get; set; }
        }

        public partial class Data
        {
            [JsonProperty("USER", NullValueHandling = NullValueHandling.Ignore)]
            public User User { get; set; }

            [JsonProperty("FOLLOW", NullValueHandling = NullValueHandling.Ignore)]
            public bool? Follow { get; set; }

            [JsonProperty("FOLLOWING", NullValueHandling = NullValueHandling.Ignore)]
            public bool? Following { get; set; }

            [JsonProperty("HAS_BLOCKED", NullValueHandling = NullValueHandling.Ignore)]
            public bool? HasBlocked { get; set; }

            [JsonProperty("IS_BLOCKED", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsBlocked { get; set; }

            [JsonProperty("IS_PUBLIC", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsPublic { get; set; }

            [JsonProperty("CURATOR", NullValueHandling = NullValueHandling.Ignore)]
            public bool? Curator { get; set; }

            [JsonProperty("IS_PERSONNAL", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsPersonnal { get; set; }

            [JsonProperty("NB_FOLLOWER", NullValueHandling = NullValueHandling.Ignore)]
            public long? NbFollower { get; set; }

            [JsonProperty("NB_FOLLOWING", NullValueHandling = NullValueHandling.Ignore)]
            public long? NbFollowing { get; set; }

            [JsonProperty("UNSEEN_PLAYLISTS_COUNT", NullValueHandling = NullValueHandling.Ignore)]
            public long? UnseenPlaylistsCount { get; set; }
        }

        public partial class User
        {
            [JsonProperty("USER_ID", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? UserId { get; set; }

            [JsonProperty("BLOG_NAME", NullValueHandling = NullValueHandling.Ignore)]
            public string BlogName { get; set; }

            [JsonProperty("USER_PICTURE", NullValueHandling = NullValueHandling.Ignore)]
            public string UserPicture { get; set; }

            [JsonProperty("ACTIVATION", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? Activation { get; set; }

            [JsonProperty("ONLINE", NullValueHandling = NullValueHandling.Ignore)]
            public Online Online { get; set; }

            [JsonProperty("__TYPE__", NullValueHandling = NullValueHandling.Ignore)]
            public string Type { get; set; }

            [JsonProperty("TOP_TRACK", NullValueHandling = NullValueHandling.Ignore)]
            public TopTrack TopTrack { get; set; }

            [JsonProperty("LOVEDTRACKS_ID", NullValueHandling = NullValueHandling.Ignore)]
            public string LovedtracksId { get; set; }

            [JsonProperty("DISPLAY_NAME", NullValueHandling = NullValueHandling.Ignore)]
            public string DisplayName { get; set; }
        }

        public partial class Online
        {
            [JsonProperty("STATUS", NullValueHandling = NullValueHandling.Ignore)]
            public bool? Status { get; set; }

            [JsonProperty("SONG")]
            public object Song { get; set; }
        }

        public partial class TopTrack
        {
            [JsonProperty("SNG_ID", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? SngId { get; set; }

            [JsonProperty("SNG_TITLE", NullValueHandling = NullValueHandling.Ignore)]
            public string SngTitle { get; set; }

            [JsonProperty("ART_ID", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? ArtId { get; set; }

            [JsonProperty("ART_NAME", NullValueHandling = NullValueHandling.Ignore)]
            public string ArtName { get; set; }

            [JsonProperty("ARTISTS", NullValueHandling = NullValueHandling.Ignore)]
            public List<Artist> Artists { get; set; }

            [JsonProperty("ALB_ID", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? AlbId { get; set; }

            [JsonProperty("ALB_TITLE", NullValueHandling = NullValueHandling.Ignore)]
            public string AlbTitle { get; set; }

            [JsonProperty("ALB_PICTURE", NullValueHandling = NullValueHandling.Ignore)]
            public string AlbPicture { get; set; }

            [JsonProperty("ALB_PHYSICAL_RELEASE_DATE", NullValueHandling = NullValueHandling.Ignore)]
            public DateTimeOffset? AlbPhysicalReleaseDate { get; set; }

            [JsonProperty("LYRICS_ID", NullValueHandling = NullValueHandling.Ignore)]
            public long? LyricsId { get; set; }

            [JsonProperty("DURATION", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? Duration { get; set; }

            [JsonProperty("EXPLICIT_TRACK_CONTENT", NullValueHandling = NullValueHandling.Ignore)]
            public ExplicitTrackContent ExplicitTrackContent { get; set; }

            [JsonProperty("__TYPE__", NullValueHandling = NullValueHandling.Ignore)]
            public string Type { get; set; }
        }

        public partial class Artist
        {
            [JsonProperty("ART_ID", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? ArtId { get; set; }

            [JsonProperty("ROLE_ID", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? RoleId { get; set; }

            [JsonProperty("ARTISTS_SONGS_ORDER", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? ArtistsSongsOrder { get; set; }

            [JsonProperty("ART_NAME", NullValueHandling = NullValueHandling.Ignore)]
            public string ArtName { get; set; }

            [JsonProperty("ART_PICTURE", NullValueHandling = NullValueHandling.Ignore)]
            public string ArtPicture { get; set; }

            [JsonProperty("SMARTRADIO", NullValueHandling = NullValueHandling.Ignore)]
            public long? Smartradio { get; set; }

            [JsonProperty("RANK", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? Rank { get; set; }

            //[JsonProperty("LOCALES", NullValueHandling = NullValueHandling.Ignore)]
            //public Locales Locales { get; set; }

            [JsonProperty("__TYPE__", NullValueHandling = NullValueHandling.Ignore)]
            public string Type { get; set; }
        }

        public partial class Locales
        {
            [JsonProperty("lang_ja-jpan", NullValueHandling = NullValueHandling.Ignore)]
            public Lang LangJaJpan { get; set; }

            [JsonProperty("lang_ja-hrkt", NullValueHandling = NullValueHandling.Ignore)]
            public Lang LangJaHrkt { get; set; }

            [JsonProperty("lang_zh-hant", NullValueHandling = NullValueHandling.Ignore)]
            public Lang LangZhHant { get; set; }
        }

        public partial class Lang
        {
            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }
        }

        public partial class ExplicitTrackContent
        {
            [JsonProperty("EXPLICIT_LYRICS_STATUS", NullValueHandling = NullValueHandling.Ignore)]
            public long? ExplicitLyricsStatus { get; set; }

            [JsonProperty("EXPLICIT_COVER_STATUS", NullValueHandling = NullValueHandling.Ignore)]
            public long? ExplicitCoverStatus { get; set; }
        }

        public partial class Tab
        {
            [JsonProperty("playlists", NullValueHandling = NullValueHandling.Ignore)]
            public Playlists Playlists { get; set; }

            [JsonProperty("playlists_suggest", NullValueHandling = NullValueHandling.Ignore)]
            public PlaylistsSuggest PlaylistsSuggest { get; set; }

            [JsonProperty("top_playlists", NullValueHandling = NullValueHandling.Ignore)]
            public Playlists TopPlaylists { get; set; }
        }

        public partial class Playlists
        {
            [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
            public List<PlaylistsDatum> Data { get; set; }

            [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
            public long? Count { get; set; }

            [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
            public long? Total { get; set; }

            [JsonProperty("filtered_count", NullValueHandling = NullValueHandling.Ignore)]
            public long? FilteredCount { get; set; }
        }

        public partial class PlaylistsDatum
        {
            [JsonProperty("PARENT_USER_ID", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? ParentUserId { get; set; }

            [JsonProperty("PLAYLIST_ID", NullValueHandling = NullValueHandling.Ignore)]
            public string PlaylistId { get; set; }

            [JsonProperty("TITLE", NullValueHandling = NullValueHandling.Ignore)]
            public string Title { get; set; }

            [JsonProperty("STATUS", NullValueHandling = NullValueHandling.Ignore)]
            public long? Status { get; set; }

            [JsonProperty("TYPE", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? Type { get; set; }

            [JsonProperty("PLAYLIST_PICTURE", NullValueHandling = NullValueHandling.Ignore)]
            public string PlaylistPicture { get; set; }

            [JsonProperty("PICTURE_TYPE", NullValueHandling = NullValueHandling.Ignore)]
            public PictureType? PictureType { get; set; }

            [JsonProperty("DATE_ADD", NullValueHandling = NullValueHandling.Ignore)]
            public DateTimeOffset? DateAdd { get; set; }

            [JsonProperty("DATE_CREATE", NullValueHandling = NullValueHandling.Ignore)]
            public DateTimeOffset? DateCreate { get; set; }

            [JsonProperty("DATE_MOD", NullValueHandling = NullValueHandling.Ignore)]
            public DateTimeOffset? DateMod { get; set; }

            [JsonProperty("NB_SONG", NullValueHandling = NullValueHandling.Ignore)]
            public long? NbSong { get; set; }

            [JsonProperty("__TYPE__", NullValueHandling = NullValueHandling.Ignore)]
            public TypeEnum? DatumType { get; set; }
        }

        public partial class PlaylistsSuggest
        {
            [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
            public List<PlaylistsSuggestDatum> Data { get; set; }

            [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
            public long? Count { get; set; }

            [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
            public long? Total { get; set; }

            [JsonProperty("filtered_count", NullValueHandling = NullValueHandling.Ignore)]
            public long? FilteredCount { get; set; }
        }

        public partial class PlaylistsSuggestDatum
        {
            [JsonProperty("PARENT_USER_ID", NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(ParseStringConverter))]
            public long? ParentUserId { get; set; }

            [JsonProperty("PLAYLIST_ID", NullValueHandling = NullValueHandling.Ignore)]
            public string PlaylistId { get; set; }

            [JsonProperty("TITLE", NullValueHandling = NullValueHandling.Ignore)]
            public string Title { get; set; }

            [JsonProperty("PLAYLIST_PICTURE", NullValueHandling = NullValueHandling.Ignore)]
            public string PlaylistPicture { get; set; }

            [JsonProperty("PICTURE_TYPE", NullValueHandling = NullValueHandling.Ignore)]
            public TypeEnum? PictureType { get; set; }

            [JsonProperty("__TYPE__", NullValueHandling = NullValueHandling.Ignore)]
            public TypeEnum? Type { get; set; }
        }

        public enum TypeEnum { Playlist };

        public enum PictureType { Cover };

        public partial class Request
        {
            public static Request FromJson(string json) => JsonConvert.DeserializeObject<Request>(json, AllPlaylists.Converter.Settings);
        }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                PictureTypeConverter.Singleton,
                TypeEnumConverter.Singleton,
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

        internal class PictureTypeConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(PictureType) || t == typeof(PictureType?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                if (value == "cover")
                {
                    return PictureType.Cover;
                }
                throw new Exception("Cannot unmarshal type PictureType");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (PictureType)untypedValue;
                if (value == PictureType.Cover)
                {
                    serializer.Serialize(writer, "cover");
                    return;
                }
                throw new Exception("Cannot marshal type PictureType");
            }

            public static readonly PictureTypeConverter Singleton = new PictureTypeConverter();
        }

        internal class TypeEnumConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                if (value == "playlist")
                {
                    return TypeEnum.Playlist;
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
                if (value == TypeEnum.Playlist)
                {
                    serializer.Serialize(writer, "playlist");
                    return;
                }
                throw new Exception("Cannot marshal type TypeEnum");
            }

            public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
        }
    }
}
