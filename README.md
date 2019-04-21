# DeezerSync
Application to Sync Playlists to Deezer

## Supported Platforms
+ SoundCloud
+ Spotify


## Search Querys

1. Vanilla Search `Artist` & `Title`
1. If `Title` contains `-` split title Then compare `Artist` with `split[0]` If equal search with `Artist` & `split[1]` else replace `Artist` (can be a label) with split[0]
   ```
   https://api.deezer.com/search?q=artist:"MR.BLACK" track:"MR.BLACK - In My Mind (Remix)"
   https://api.deezer.com/search?q=artist:"SLUMBERJACK" track:"SLUMBERJACK X Troyboi - Solid"
   https://api.deezer.com/search?q=artist:"Monstercat" track:"Lil Hank - Hank's Back"
   https://api.deezer.com/search?q=artist:"KLOUD" track:"KLOUD - Save The World"
   https://api.deezer.com/search?q=artist:"Dharma Worldwide" track:"KRIMSONN - AUBURN LULLABY"
   https://api.deezer.com/search?q=artist:"Monstercat" track:"Notaker - Into The Light (feat. Karra)"
   ```
1. If results < 0 try filter with Duration
1. Remove unwantet chars `&`
1. Remove unwantet details `[OUT NOW]` & `(TNT Remix)` // Filter for `Remix` and save remix artist for next query 
   ```
   https://api.deezer.com/search?q=artist:"Spinnin' Records" track:"Timmy Trumpet - Oracle (TNT Remix) [OUT NOW]"
   ```
