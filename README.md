# PlexDL
### Plex Downloader/Streamer written in C#

* Utilises a slightly modified fork of MetroSet-UI by N-a-r-w-i-n - [GitHub Repo](https://github.com/Brhsoftco/MetroSet-UI)
* Utilises csharp-plex-api by ammmze for server detection - [GitHub Repo](https://github.com/ammmze/csharp-plex-api)
* Utilises Google's Material Design Icons - [GitHub Repo](https://material.io/icons)
* Utilises AltoHttp by aalitor for Web Downloads - [GitHub Repo](https://github.com/aalitor/AltoHttp)
* Utilises PVS.MediaPlayer by Peter Vegter - [CodeProject Article](https://www.codeproject.com/Articles/109714/PVS-MediaPlayer-Audio-and-Video-Player-Library)
* Utilises WinFormAnimation by falahati - [Github Repo](https://github.com/falahati/WinFormAnimation/)
* Utilises CircularProgressBar by falhati - [GitHub Repo](https://github.com/falahati/CircularProgressBar/)

### What does PlexDL do?
PlexDL uses a Plex Media Server's ability to serve XML API requests. PlexDL gathers information from the API and displays it in various gridviews to make it easier for you to enjoy your content. PlexDL can gather information about Plex Movies and TV Shows (archives and other content variations are not yet implemented), and provide you with the ability to stream the content or download it from the server. You can also view various metadata attributes about the selected content via the button in the "Data" section.

### Performance?
PlexDL is in **no way** stable enough to be called high-performance. It is, however, stable enough to be used in most situations, and will work for almost any PMS out there (provided you have an account key). However, there may be instances where the software is underperforming due to a variety of reasons. One such reason, is that the custom interfaces built to interpret the data from the PMS are far from perfect, and may stutter from time to time. PlexDL is also heavily reliant on internet speeds and server reliability, so that is also a factor.

### How to get started
#### __1. Building from Source__
PlexDL targets the .NET Framework 4.7.2, and was initially built with Visual Studio 2019. The PlexDL source comes preloaded with necessary icons and resources, AltoHttp, chsarp-plex-api, WinFormAnimation, CircularProgressBar and PVS.MediaPlayer. PlexDL includes the appropriate NuGet references to libbrhscgui (prebuilt for you) and RestSharp, and upon cloning this repo, you'll just need to restore the packages. After doing all of this, you can successfully build the software. If you have any issues building or using the software, please file a GitHub issue report.
#### __2. Downloading from Releases__
You can access the latest build [here](https://github.com/Brhsoftco/PlexDL-MetroSet_UI/releases/latest).
### __Using PlexDL__
#### __Basic Usage__
1. To get started, first obtain your Plex account token. A guide for this may be found [here](https://support.plex.tv/articles/204059436-finding-an-authentication-token-x-plex-token/).
2. Select the connect button from the main panel and enter your account token.
3. Allow the program to search for and connect to a server.
4. PlexDL may also display additional servers registered to your token in the Servers panel.
5. Once you are connected (and the library is filled), you may start to browse your library.
6. The "Library Content" area is your hub for titles. Here, you can select TV Shows and Movies from their respective tabs.
7. If you select a movie from the "Movies" panel, the options to stream or download the content will become immediately available.
8. If you select a TV Show from the "TV" panel, you may browse the TV seasons (Top-Right panel) and episodes (Bottom-Right panel) associated with that title.
9. You may only stream or download a TV/Movie title upon selecting an item from the appropriate panel.
10. You may browse metadata associated with your selected content by clicking the "Bookmark" icon.
11. PlexDL allows profile loading and saving, which allows you to save your account token for later use or change internal settings to your liking. To do this, first load your content view, and then select the "Save" floppy disk icon. You can then edit the *.prof* XML file in any ordinary text editor.
12. Likewise, to load the profile, simply select the "Load" folder icon and browse to your generated XML *.prof* file.

#### __Content Filtering__
* PlexDL natively filters possibly adult-orientated content.
* It is possible to disable this filter by exporting a profile, then changing "AdultContentProtection" from "true" to "false" inside the resulting _.prof_ file, then reloading that profile back into PlexDL.
* For users' convenience, PlexDL will filter content that matches a genre-based criteria by pixelating posters in the metadata section, and warning users before streaming the content.
* PlexDL can also filter adult content based on a keyword list. E.g. a text file that contains terms related to adult content.
* PlexDL includes a blank file named "keywordBlacklist.txt" in the "Resources" section of the source code, however, you must populate this list yourself and build the source from there.
* PlexDL does not provide populated keyword lists by default; please do not ask for any.

#### __Shortcut Keys__
* Control+O - Allows Profile Loading
* Control+S - Allows Profile Saving
* Control+C - Allows Launching the Connection Dialog
* Control+D - Allows Disconnecting
* Control+M - Allows Viewing Metadata
* Control+F - Allows Launching the Search Dialog
* Control+E - Allows Exporting PlexMovie XML Data (for the Currently Selected Title)