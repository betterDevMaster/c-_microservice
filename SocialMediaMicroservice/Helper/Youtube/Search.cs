using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using SocialMediaMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Helper.Youtube
{
    public class Search
    {
        /// <summary>
        /// YouTube Data API v3 sample: search by keyword.
        /// Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
        /// See https://developers.google.com/api-client-library/dotnet/get_started
        ///
        /// Set ApiKey to the API key value from the APIs & auth > Registered apps tab of
        ///   https://cloud.google.com/console
        /// Please ensure that you have enabled the YouTube Data API for your project.
        /// </summary>

        public async Task<List<YoutubeSearchModel>> Run(string searchtext, string regionCode)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                //ApiKey = "AIzaSyAYiCDz7VoCjNFoE7fsVPbPY9iWyLOMth8",      //this key is not working
                ApiKey = "AIzaSyA8hPK8F-8BbO-8H6tQZuiopY5nYES1UR0",        //New api key by Sonny
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchtext; // Replace with your search term.
            searchListRequest.MaxResults = 50;
            searchListRequest.RegionCode = regionCode;
            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            List<YoutubeSearchModel> searchmodel = new List<YoutubeSearchModel>();
            YoutubeSearchModel obj = null;
            foreach (var searchResult in searchListResponse.Items)
            {
                obj = new YoutubeSearchModel();
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        obj.title = searchResult.Snippet.Title;
                        obj.url = "https://youtu.be/" + searchResult.Id.VideoId;
                        searchmodel.Add(obj);
                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, "https://youtu.be/" + searchResult.Id.VideoId));
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }
            }

            Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
            Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
            Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));

            return searchmodel;
        }
    }
}
