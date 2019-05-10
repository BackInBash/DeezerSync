# DeezerSync
Application to Sync Playlists to Deezer

## Supported Platforms
+ SoundCloud
+ Spotify (later on)

## Supported Operating Systems
+ Windows
+ macOS
+ Linux (Later in a fully featured version)

## Used Libraries
+ [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
+ [SoundCloud.Api](https://github.com/prayzzz/SoundCloud.Api)

### Build with dotNET Core 3 Preview 5 & VS 2019

## Getting Started
To run this Program a `config.json` config file is needed in the working directory.
The `config.json` requires the following entries:

```console
{
  "SoundCloud_Username": "",    // SoundCloud User Name (These are the playlists to sync)
  "SoundCloud_ClientID": "",    // A valid SoundCloud Client ID (Just start a few tracks on the site and check the dev console for api calls. A clientid should be in the url)
  "Deezer_Secret": ""           // Login to Deezer and search in the dev console for an arl Cookie
}
```
