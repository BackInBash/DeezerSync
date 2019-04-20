using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DeezerSync.Deezer.API.Model
{
    class UserDataModel
    {
        public partial class Welcome
        {
            [JsonProperty("error")]
            public List<object> Error { get; set; }

            [JsonProperty("results")]
            public Results Results { get; set; }
        }

        public partial class Results
        {
            [JsonProperty("USER")]
            public User User { get; set; }

            [JsonProperty("SETTING_LANG")]
            public string SettingLang { get; set; }

            [JsonProperty("SETTING_LOCALE")]
            public string SettingLocale { get; set; }

            [JsonProperty("DIRECTION")]
            public string Direction { get; set; }

            [JsonProperty("SESSION_ID")]
            public string SessionId { get; set; }

            [JsonProperty("USER_TOKEN")]
            public string UserToken { get; set; }

            [JsonProperty("PLAYLIST_WELCOME_ID")]
            public long PlaylistWelcomeId { get; set; }

            [JsonProperty("OFFER_ID")]
            public long OfferId { get; set; }

            [JsonProperty("OFFER_ELIGIBILITIES")]
            public List<object> OfferEligibilities { get; set; }

            [JsonProperty("COUNTRY")]
            public string Country { get; set; }

            [JsonProperty("COUNTRY_CATEGORY")]
            public string CountryCategory { get; set; }

            [JsonProperty("MIN_LEGAL_AGE")]
            public long MinLegalAge { get; set; }

            [JsonProperty("FAMILY_KIDS_AGE")]
            public long FamilyKidsAge { get; set; }

            [JsonProperty("SERVER_TIMESTAMP")]
            public long ServerTimestamp { get; set; }

            [JsonProperty("PLAYER_TOKEN")]
            public string PlayerToken { get; set; }

            [JsonProperty("checkForm")]
            public string CheckForm { get; set; }

            [JsonProperty("FROM_ONBOARDING")]
            [JsonConverter(typeof(PurpleParseStringConverter))]
            public bool FromOnboarding { get; set; }

            [JsonProperty("CUSTO")]
            public string Custo { get; set; }

            [JsonProperty("SETTING_REFERER_UPLOAD")]
            public string SettingRefererUpload { get; set; }

            [JsonProperty("REG_FLOW")]
            public List<string> RegFlow { get; set; }

            [JsonProperty("LOGIN_FLOW")]
            public List<string> LoginFlow { get; set; }

            [JsonProperty("__DZR_GATEKEEPS__")]
            public Dictionary<string, bool> DzrGatekeeps { get; set; }

            [JsonProperty("thirdParty")]
            public ThirdParty ThirdParty { get; set; }

            [JsonProperty("URL_MEDIA")]
            public string UrlMedia { get; set; }

            [JsonProperty("SPONSORED_TRACK_CONFIG")]
            public SponsoredTrackConfig SponsoredTrackConfig { get; set; }
        }

        public partial class SponsoredTrackConfig
        {
            [JsonProperty("token")]
            public Guid Token { get; set; }

            [JsonProperty("progress_call_frequency")]
            public long ProgressCallFrequency { get; set; }

            [JsonProperty("api_key")]
            public string ApiKey { get; set; }

            [JsonProperty("api_url")]
            public Uri ApiUrl { get; set; }
        }

        public partial class ThirdParty
        {
            [JsonProperty("facebook")]
            public ThirdPartyFacebook Facebook { get; set; }

            [JsonProperty("googleplus")]
            public ThirdPartyGoogleplus Googleplus { get; set; }

            [JsonProperty("braze")]
            public Braze Braze { get; set; }
        }

        public partial class Braze
        {
            [JsonProperty("apiKey")]
            public Guid ApiKey { get; set; }

            [JsonProperty("isAvailable")]
            public bool IsAvailable { get; set; }
        }

        public partial class ThirdPartyFacebook
        {
            [JsonProperty("appData")]
            public FacebookAppData AppData { get; set; }

            [JsonProperty("uid")]
            public string Uid { get; set; }

            [JsonProperty("accessToken")]
            public string AccessToken { get; set; }

            [JsonProperty("externalScope")]
            public string ExternalScope { get; set; }

            [JsonProperty("autoLogin")]
            public bool AutoLogin { get; set; }

            [JsonProperty("lang")]
            public string Lang { get; set; }
        }

        public partial class FacebookAppData
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("namespace")]
            public string Namespace { get; set; }

            [JsonProperty("scope")]
            public string Scope { get; set; }

            [JsonProperty("version")]
            public string Version { get; set; }

            [JsonProperty("channel")]
            public Uri Channel { get; set; }
        }

        public partial class ThirdPartyGoogleplus
        {
            [JsonProperty("appData")]
            public GoogleplusAppData AppData { get; set; }

            [JsonProperty("uid")]
            public object Uid { get; set; }

            [JsonProperty("accessToken")]
            public object AccessToken { get; set; }
        }

        public partial class GoogleplusAppData
        {
            [JsonProperty("client_id")]
            public string ClientId { get; set; }

            [JsonProperty("client_key")]
            public string ClientKey { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("scope")]
            public Uri Scope { get; set; }

            [JsonProperty("redirect_uri")]
            public string RedirectUri { get; set; }

            [JsonProperty("access_type")]
            public string AccessType { get; set; }

            [JsonProperty("cookie_policy")]
            public string CookiePolicy { get; set; }

            [JsonProperty("request_visible_actions")]
            public Uri RequestVisibleActions { get; set; }

            [JsonProperty("version")]
            public string Version { get; set; }
        }

        public partial class User
        {
            [JsonProperty("USER_ID")]
            public long UserId { get; set; }

            [JsonProperty("USER_PICTURE")]
            public string UserPicture { get; set; }

            [JsonProperty("INSCRIPTION_DATE")]
            public DateTimeOffset InscriptionDate { get; set; }

            [JsonProperty("TRY_AND_BUY")]
            public TryAndBuy TryAndBuy { get; set; }

            [JsonProperty("PARTNERS")]
            [JsonConverter(typeof(FluffyParseStringConverter))]
            public long Partners { get; set; }

            [JsonProperty("TOOLBAR")]
            public List<object> Toolbar { get; set; }

            [JsonProperty("OPTIONS")]
            public Options Options { get; set; }

            [JsonProperty("SETTING")]
            public Setting Setting { get; set; }

            [JsonProperty("LASTFM")]
            public GoogleplusClass Lastfm { get; set; }

            [JsonProperty("TWITTER")]
            public GoogleplusClass Twitter { get; set; }

            [JsonProperty("FACEBOOK")]
            public Facebook Facebook { get; set; }

            [JsonProperty("GOOGLEPLUS")]
            public GoogleplusClass Googleplus { get; set; }

            [JsonProperty("FAVORITE_TAG")]
            public long FavoriteTag { get; set; }

            [JsonProperty("ABTEST")]
            public Abtest Abtest { get; set; }

            [JsonProperty("MULTI_ACCOUNT")]
            public MultiAccount MultiAccount { get; set; }

            [JsonProperty("ONBOARDING_MODAL")]
            public bool OnboardingModal { get; set; }

            [JsonProperty("ADS_OFFER")]
            [JsonConverter(typeof(FluffyParseStringConverter))]
            public long AdsOffer { get; set; }

            [JsonProperty("ENTRYPOINTS")]
            public Entrypoints Entrypoints { get; set; }

            [JsonProperty("ADS_TEST_FORMAT")]
            public string AdsTestFormat { get; set; }

            [JsonProperty("EXPLICIT_CONTENT_LEVEL")]
            public string ExplicitContentLevel { get; set; }

            [JsonProperty("EXPLICIT_CONTENT_LEVELS_AVAILABLE")]
            public List<string> ExplicitContentLevelsAvailable { get; set; }

            [JsonProperty("BLOG_NAME")]
            public string BlogName { get; set; }

            [JsonProperty("FIRSTNAME")]
            public string Firstname { get; set; }

            [JsonProperty("LASTNAME")]
            public string Lastname { get; set; }

            [JsonProperty("USER_GENDER")]
            public string UserGender { get; set; }

            [JsonProperty("USER_AGE")]
            [JsonConverter(typeof(FluffyParseStringConverter))]
            public long UserAge { get; set; }

            [JsonProperty("DEVICES_COUNT")]
            public long DevicesCount { get; set; }

            [JsonProperty("HAS_UPNEXT")]
            public bool HasUpnext { get; set; }

            [JsonProperty("LOVEDTRACKS_ID")]
            public string LovedtracksId { get; set; }
        }

        public partial class Abtest
        {
            [JsonProperty("free_xp_homepage_free_users")]
            public AdBanner FreeXpHomepageFreeUsers { get; set; }

            [JsonProperty("discovery_algorithms")]
            public AdBanner DiscoveryAlgorithms { get; set; }

            [JsonProperty("flow_algorithms")]
            public AdBanner FlowAlgorithms { get; set; }

            [JsonProperty("channel_algorithms")]
            public AdBanner ChannelAlgorithms { get; set; }

            [JsonProperty("partner_association_push")]
            public AdBanner PartnerAssociationPush { get; set; }

            [JsonProperty("ad_int_mobile")]
            public AdBanner AdIntMobile { get; set; }

            [JsonProperty("ad_big_native")]
            public AdBanner AdBigNative { get; set; }

            [JsonProperty("ad_banner")]
            public AdBanner AdBanner { get; set; }

            [JsonProperty("swat_conversion_banner_free")]
            public AdBanner SwatConversionBannerFree { get; set; }

            [JsonProperty("end_of_session")]
            public AdBanner EndOfSession { get; set; }
        }

        public partial class AdBanner
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("option")]
            public string Option { get; set; }

            [JsonProperty("behaviour")]
            public string Behaviour { get; set; }
        }

        public partial class Entrypoints
        {
            [JsonProperty("LYRICS_PANEL")]
            public ConversionBannerFree LyricsPanel { get; set; }

            [JsonProperty("AUDIO_SETTING_PREMIUM")]
            public AudioSetting AudioSettingPremium { get; set; }

            [JsonProperty("AUDIO_SETTING_HIFI")]
            public AudioSetting AudioSettingHifi { get; set; }

            [JsonProperty("CONVERSION_BANNER_FREE")]
            public ConversionBannerFree ConversionBannerFree { get; set; }
        }

        public partial class AudioSetting
        {
            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("action")]
            public Uri Action { get; set; }
        }

        public partial class ConversionBannerFree
        {
            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("action")]
            public Uri Action { get; set; }
        }

        public partial class Facebook
        {
            [JsonProperty("UID")]
            public string Uid { get; set; }
        }

        public partial class GoogleplusClass
        {
        }

        public partial class MultiAccount
        {
            [JsonProperty("ENABLED")]
            public bool Enabled { get; set; }

            [JsonProperty("ACTIVE")]
            public bool Active { get; set; }

            [JsonProperty("CHILD_COUNT")]
            public object ChildCount { get; set; }

            [JsonProperty("MAX_CHILDREN")]
            public object MaxChildren { get; set; }

            [JsonProperty("PARENT")]
            public object Parent { get; set; }

            [JsonProperty("IS_KID")]
            public bool IsKid { get; set; }
        }

        public partial class Options
        {
            [JsonProperty("mobile_preview")]
            public bool MobilePreview { get; set; }

            [JsonProperty("mobile_radio")]
            public bool MobileRadio { get; set; }

            [JsonProperty("mobile_streaming")]
            public bool MobileStreaming { get; set; }

            [JsonProperty("mobile_streaming_duration")]
            public long MobileStreamingDuration { get; set; }

            [JsonProperty("mobile_offline")]
            public bool MobileOffline { get; set; }

            [JsonProperty("mobile_sound_quality")]
            public SoundQuality MobileSoundQuality { get; set; }

            [JsonProperty("mobile_audio_qualities")]
            public AudioQualities MobileAudioQualities { get; set; }

            [JsonProperty("default_download_on_mobile_network")]
            public bool DefaultDownloadOnMobileNetwork { get; set; }

            [JsonProperty("mobile_hq")]
            public bool MobileHq { get; set; }

            [JsonProperty("mobile_lossless")]
            public bool MobileLossless { get; set; }

            [JsonProperty("tablet_sound_quality")]
            public SoundQuality TabletSoundQuality { get; set; }

            [JsonProperty("tablet_audio_qualities")]
            public AudioQualities TabletAudioQualities { get; set; }

            [JsonProperty("audio_quality_default_preset")]
            public string AudioQualityDefaultPreset { get; set; }

            [JsonProperty("web_preview")]
            public bool WebPreview { get; set; }

            [JsonProperty("web_radio")]
            public bool WebRadio { get; set; }

            [JsonProperty("web_streaming")]
            public bool WebStreaming { get; set; }

            [JsonProperty("web_streaming_duration")]
            public long WebStreamingDuration { get; set; }

            [JsonProperty("web_offline")]
            public bool WebOffline { get; set; }

            [JsonProperty("web_hq")]
            public bool WebHq { get; set; }

            [JsonProperty("web_lossless")]
            public bool WebLossless { get; set; }

            [JsonProperty("web_sound_quality")]
            public SoundQuality WebSoundQuality { get; set; }

            [JsonProperty("ads_display")]
            public bool AdsDisplay { get; set; }

            [JsonProperty("ads_audio")]
            public bool AdsAudio { get; set; }

            [JsonProperty("dj")]
            public bool Dj { get; set; }

            [JsonProperty("nb_devices")]
            public long NbDevices { get; set; }

            [JsonProperty("multi_account")]
            public bool MultiAccount { get; set; }

            [JsonProperty("multi_account_max_allowed")]
            public long MultiAccountMaxAllowed { get; set; }

            [JsonProperty("radio_skips")]
            public long RadioSkips { get; set; }

            [JsonProperty("too_many_devices")]
            public bool TooManyDevices { get; set; }

            [JsonProperty("business")]
            public bool Business { get; set; }

            [JsonProperty("business_mod")]
            public bool BusinessMod { get; set; }

            [JsonProperty("business_box_owner")]
            public bool BusinessBoxOwner { get; set; }

            [JsonProperty("business_box_manager")]
            public bool BusinessBoxManager { get; set; }

            [JsonProperty("business_box")]
            public bool BusinessBox { get; set; }

            [JsonProperty("business_no_right")]
            public bool BusinessNoRight { get; set; }

            [JsonProperty("allow_subscription")]
            public bool AllowSubscription { get; set; }

            [JsonProperty("allow_trial_mobile")]
            public bool AllowTrialMobile { get; set; }

            [JsonProperty("timestamp")]
            public long Timestamp { get; set; }

            [JsonProperty("can_subscribe")]
            public bool CanSubscribe { get; set; }

            [JsonProperty("show_subscription_section")]
            public bool ShowSubscriptionSection { get; set; }

            [JsonProperty("streaming_group")]
            public string StreamingGroup { get; set; }

            [JsonProperty("web_streaming_used")]
            public long WebStreamingUsed { get; set; }

            [JsonProperty("can_subscribe_family")]
            public bool CanSubscribeFamily { get; set; }

            [JsonProperty("upgrade")]
            public Upgrade Upgrade { get; set; }
        }

        public partial class AudioQualities
        {
            [JsonProperty("mobile_download")]
            public List<string> MobileDownload { get; set; }

            [JsonProperty("mobile_streaming")]
            public List<string> MobileStreaming { get; set; }

            [JsonProperty("wifi_download")]
            public List<string> WifiDownload { get; set; }

            [JsonProperty("wifi_streaming")]
            public List<string> WifiStreaming { get; set; }
        }

        public partial class SoundQuality
        {
            [JsonProperty("low")]
            public bool Low { get; set; }

            [JsonProperty("standard")]
            public bool Standard { get; set; }

            [JsonProperty("high")]
            public bool High { get; set; }

            [JsonProperty("lossless")]
            public bool Lossless { get; set; }
        }

        public partial class Upgrade
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("offer")]
            public Offer Offer { get; set; }

            [JsonProperty("cta")]
            public Cta Cta { get; set; }
        }

        public partial class Cta
        {
            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("label_extend")]
            public string LabelExtend { get; set; }

            [JsonProperty("log_name")]
            public string LogName { get; set; }
        }

        public partial class Offer
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("duration")]
            public long Duration { get; set; }

            [JsonProperty("price")]
            public Price Price { get; set; }
        }

        public partial class Price
        {
            [JsonProperty("amount")]
            public string Amount { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("display")]
            public string Display { get; set; }
        }

        public partial class Setting
        {
            [JsonProperty("newsletter")]
            public Newsletter Newsletter { get; set; }

            [JsonProperty("global")]
            public Global Global { get; set; }

            [JsonProperty("site")]
            public Site Site { get; set; }

            [JsonProperty("twitter")]
            public GoogleClass Twitter { get; set; }

            [JsonProperty("facebook")]
            public GoogleClass Facebook { get; set; }

            [JsonProperty("google")]
            public GoogleClass Google { get; set; }

            [JsonProperty("notification_mail")]
            public NotificationM NotificationMail { get; set; }

            [JsonProperty("notification_mobile")]
            public NotificationM NotificationMobile { get; set; }

            [JsonProperty("beta_user")]
            public BetaUser BetaUser { get; set; }

            [JsonProperty("tips")]
            public Tips Tips { get; set; }

            [JsonProperty("audio_quality_settings")]
            public AudioQualitySettings AudioQualitySettings { get; set; }

            [JsonProperty("ads")]
            public Ads Ads { get; set; }

            [JsonProperty("location")]
            public Location Location { get; set; }

            [JsonProperty("customer_message")]
            public CustomerMessage CustomerMessage { get; set; }

            [JsonProperty("adjust")]
            public Adjust Adjust { get; set; }

            [JsonProperty("optin_mail")]
            public Optin OptinMail { get; set; }

            [JsonProperty("optin_push")]
            public Optin OptinPush { get; set; }

            [JsonProperty("optin_inapp")]
            public Optin OptinInapp { get; set; }

            [JsonProperty("optin_sms")]
            public Optin OptinSms { get; set; }
        }

        public partial class Adjust
        {
            [JsonProperty("first_stream_id")]
            public long FirstStreamId { get; set; }

            [JsonProperty("device")]
            public Device Device { get; set; }
        }

        public partial class Device
        {
            [JsonProperty("ax102699a4bdbcf8f7")]
            public DeviceAx102699A4Bdbcf8F7 Ax102699A4Bdbcf8F7 { get; set; }
        }

        public partial class DeviceAx102699A4Bdbcf8F7
        {
            [JsonProperty("login")]
            public Login Login { get; set; }
        }

        public partial class Login
        {
            [JsonProperty("last_trigger")]
            public long LastTrigger { get; set; }
        }

        public partial class Ads
        {
            [JsonProperty("featurefm_token")]
            public FeaturefmToken FeaturefmToken { get; set; }

            [JsonProperty("test_format")]
            public bool TestFormat { get; set; }

            [JsonProperty("force_adsource")]
            public string ForceAdsource { get; set; }
        }

        public partial class FeaturefmToken
        {
            [JsonProperty("token")]
            public Guid Token { get; set; }

            [JsonProperty("date")]
            public DateTimeOffset Date { get; set; }

            [JsonProperty("api_url")]
            public Uri ApiUrl { get; set; }
        }

        public partial class AudioQualitySettings
        {
            [JsonProperty("preset")]
            public string Preset { get; set; }

            [JsonProperty("download_on_mobile_network")]
            public bool DownloadOnMobileNetwork { get; set; }

            [JsonProperty("connected_device_streaming_preset")]
            public bool ConnectedDeviceStreamingPreset { get; set; }
        }

        public partial class BetaUser
        {
            [JsonProperty("ios")]
            public bool Ios { get; set; }

            [JsonProperty("android")]
            public bool Android { get; set; }

            [JsonProperty("windowsphone")]
            public bool Windowsphone { get; set; }

            [JsonProperty("windows")]
            public bool Windows { get; set; }
        }

        public partial class CustomerMessage
        {
            [JsonProperty("conversion_pplus")]
            public ConversionPplus ConversionPplus { get; set; }

            [JsonProperty("push_mobile")]
            public Push PushMobile { get; set; }

            [JsonProperty("conversion_family_offer")]
            public ConversionFamilyOffer ConversionFamilyOffer { get; set; }

            [JsonProperty("push_trialend_freexp")]
            public Push PushTrialendFreexp { get; set; }
        }

        public partial class ConversionFamilyOffer
        {
            [JsonProperty("web")]
            public Dictionary<string, bool> Web { get; set; }

            [JsonProperty("ax102699a4bdbcf8f7")]
            public ConversionFamilyOfferAx102699A4Bdbcf8F7 Ax102699A4Bdbcf8F7 { get; set; }
        }

        public partial class ConversionFamilyOfferAx102699A4Bdbcf8F7
        {
            [JsonProperty("2018-08-25")]
            public bool The20180825 { get; set; }
        }

        public partial class ConversionPplus
        {
            [JsonProperty("desktop")]
            public Desktop Desktop { get; set; }

            [JsonProperty("android")]
            public Android Android { get; set; }
        }

        public partial class Android
        {
            [JsonProperty("reg_d1")]
            public bool RegD1 { get; set; }
        }

        public partial class Desktop
        {
            [JsonProperty("reg_d1")]
            public bool RegD1 { get; set; }

            [JsonProperty("seasonal_offer")]
            [JsonConverter(typeof(FluffyParseStringConverter))]
            public long SeasonalOffer { get; set; }
        }

        public partial class Push
        {
            [JsonProperty("properties")]
            public List<object> Properties { get; set; }

            [JsonProperty("display_count")]
            public long DisplayCount { get; set; }

            [JsonProperty("last_display")]
            public DateTimeOffset LastDisplay { get; set; }
        }

        public partial class GoogleClass
        {
            [JsonProperty("share_comment")]
            public bool ShareComment { get; set; }

            [JsonProperty("share_favourite")]
            public bool ShareFavourite { get; set; }

            [JsonProperty("share_loved")]
            public bool ShareLoved { get; set; }

            [JsonProperty("share_listen", NullValueHandling = NullValueHandling.Ignore)]
            public bool? ShareListen { get; set; }

            [JsonProperty("share_share")]
            public bool ShareShare { get; set; }
        }

        public partial class Global
        {
            [JsonProperty("language")]
            public string Language { get; set; }

            [JsonProperty("onboarding_progress")]
            public long OnboardingProgress { get; set; }

            [JsonProperty("cookie_consent_string")]
            public string CookieConsentString { get; set; }

            [JsonProperty("social")]
            public bool Social { get; set; }

            [JsonProperty("popup_unload")]
            public bool PopupUnload { get; set; }

            [JsonProperty("filter_explicit_lyrics")]
            public bool FilterExplicitLyrics { get; set; }

            [JsonProperty("is_kid")]
            public bool IsKid { get; set; }

            [JsonProperty("has_up_next")]
            public bool HasUpNext { get; set; }

            [JsonProperty("onboarding")]
            public bool Onboarding { get; set; }

            [JsonProperty("has_root_consent")]
            public long HasRootConsent { get; set; }
        }

        public partial class Location
        {
            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("lat")]
            public double Lat { get; set; }

            [JsonProperty("lon")]
            public double Lon { get; set; }

            [JsonProperty("source")]
            public string Source { get; set; }
        }

        public partial class Newsletter
        {
            [JsonProperty("editor")]
            public bool Editor { get; set; }

            [JsonProperty("talk")]
            public bool Talk { get; set; }

            [JsonProperty("event")]
            public bool Event { get; set; }

            [JsonProperty("game")]
            public bool Game { get; set; }

            [JsonProperty("special_offer")]
            public bool SpecialOffer { get; set; }

            [JsonProperty("panel")]
            public bool Panel { get; set; }
        }

        public partial class NotificationM
        {
            [JsonProperty("share")]
            public bool Share { get; set; }

            [JsonProperty("friend_follow")]
            public bool FriendFollow { get; set; }

            [JsonProperty("playlist_comment")]
            public bool PlaylistComment { get; set; }

            [JsonProperty("playlist_follow")]
            public bool PlaylistFollow { get; set; }

            [JsonProperty("artist_new_release")]
            public bool ArtistNewRelease { get; set; }

            [JsonProperty("artist_status")]
            public bool ArtistStatus { get; set; }

            [JsonProperty("new_message")]
            public bool NewMessage { get; set; }
        }

        public partial class Optin
        {
            [JsonProperty("update")]
            public long Update { get; set; }

            [JsonProperty("special_offer")]
            public long SpecialOffer { get; set; }

            [JsonProperty("social")]
            public long Social { get; set; }

            [JsonProperty("event")]
            public long Event { get; set; }

            [JsonProperty("third_party")]
            public long ThirdParty { get; set; }

            [JsonProperty("survey")]
            public long Survey { get; set; }
        }

        public partial class Site
        {
            [JsonProperty("version")]
            [JsonConverter(typeof(FluffyParseStringConverter))]
            public long Version { get; set; }

            [JsonProperty("push_windows_10")]
            public bool PushWindows10 { get; set; }

            [JsonProperty("player_hq")]
            public bool PlayerHq { get; set; }

            [JsonProperty("player_fade")]
            public long PlayerFade { get; set; }

            [JsonProperty("player_shuffle")]
            public bool PlayerShuffle { get; set; }

            [JsonProperty("push_audiobooks")]
            public bool PushAudiobooks { get; set; }

            [JsonProperty("push_drive")]
            public bool PushDrive { get; set; }

            [JsonProperty("player_audio_quality")]
            public string PlayerAudioQuality { get; set; }

            [JsonProperty("labs")]
            public Labs Labs { get; set; }

            [JsonProperty("livebar_state")]
            public string LivebarState { get; set; }

            [JsonProperty("livebar_tab")]
            public string LivebarTab { get; set; }

            [JsonProperty("push_mobile")]
            public long PushMobile { get; set; }

            [JsonProperty("howto_step")]
            public long HowtoStep { get; set; }

            [JsonProperty("edito_tag")]
            public long EditoTag { get; set; }

            [JsonProperty("display_confirm_discovery")]
            public long DisplayConfirmDiscovery { get; set; }

            [JsonProperty("cast_audio_quality")]
            public string CastAudioQuality { get; set; }
        }

        public partial class Labs
        {
            [JsonProperty("ElectronUI")]
            public bool ElectronUi { get; set; }
        }

        public partial class Tips
        {
            [JsonProperty("lyrics")]
            public long Lyrics { get; set; }

            [JsonProperty("playlist_assistant_suggest")]
            public bool PlaylistAssistantSuggest { get; set; }

            [JsonProperty("player")]
            public bool Player { get; set; }

            [JsonProperty("flow")]
            public bool Flow { get; set; }

            [JsonProperty("add_to_library")]
            public bool AddToLibrary { get; set; }

            [JsonProperty("playlist_assistant")]
            public bool PlaylistAssistant { get; set; }

            [JsonProperty("up_next")]
            public bool UpNext { get; set; }
        }

        public partial class TryAndBuy
        {
            [JsonProperty("AVAILABLE")]
            public bool Available { get; set; }

            [JsonProperty("ACTIVE")]
            [JsonConverter(typeof(FluffyParseStringConverter))]
            public long Active { get; set; }

            [JsonProperty("DATE_START")]
            public string DateStart { get; set; }

            [JsonProperty("DATE_END")]
            public string DateEnd { get; set; }

            [JsonProperty("PLATEFORM")]
            public string Plateform { get; set; }

            [JsonProperty("DAYS_LEFT_MOB")]
            public long DaysLeftMob { get; set; }
        }

        public partial class Welcome
        {
            public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, UserDataModel.Converter.Settings);
        }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }

        internal class PurpleParseStringConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                bool b;
                if (Boolean.TryParse(value, out b))
                {
                    return b;
                }
                throw new Exception("Cannot unmarshal type bool");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (bool)untypedValue;
                var boolString = value ? "true" : "false";
                serializer.Serialize(writer, boolString);
                return;
            }

            public static readonly PurpleParseStringConverter Singleton = new PurpleParseStringConverter();
        }

        internal class FluffyParseStringConverter : JsonConverter
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

            public static readonly FluffyParseStringConverter Singleton = new FluffyParseStringConverter();
        }
    }
}
