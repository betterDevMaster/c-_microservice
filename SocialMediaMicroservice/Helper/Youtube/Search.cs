using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Newtonsoft.Json.Linq;
using SocialMediaMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var ApiKey = "AIzaSyAmKcGIrqC2oeeqIuqeKhbDfQVEG5F76sE";
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKey,        //New api key by Nguyen
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");

            searchListRequest.Q = searchtext; // Replace with your search term.
            searchListRequest.MaxResults = 50;
            searchListRequest.RegionCode = regionCode;
            // Call the search.list method to retrieve results matching the specified query term.

            Google.Apis.YouTube.v3.Data.SearchListResponse searchListResponse = null;
            try
            {
                searchListResponse = searchListRequest.Execute();
            }
            catch (Exception ex)
            {
                return new List<YoutubeSearchModel>();
            }

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
                        var detailListRequest = youtubeService.Videos.List("contentDetails, statistics");
                        detailListRequest.Id = searchResult.Id.VideoId;
                        var response = detailListRequest.Execute();

                        obj.title = searchResult.Snippet.Title;
                        obj.regionCode = regionCode;
                        obj.url = "https://youtu.be/" + searchResult.Id.VideoId;
                        obj.description = searchResult.Snippet.Description;
                        obj.picture = GetMainImg(searchResult.Snippet.Thumbnails);
                        obj.thumbnail = GetThumbnailImg(searchResult.Snippet.Thumbnails);
                        obj.publishedAt = searchResult.Snippet.PublishedAt;
                        obj.channelId = searchResult.Snippet.ChannelId;
                        obj.channelTitle = searchResult.Snippet.ChannelTitle;
                        obj.duration = GetDuration(response.Items[0].ContentDetails.Duration);
                        obj.viewCount = response.Items[0].Statistics.ViewCount.ToString();
                        obj.likeCount = response.Items[0].Statistics.LikeCount.ToString();
                        obj.dislikeCount = response.Items[0].Statistics.DislikeCount.ToString();
                        obj.favoriteCount = response.Items[0].Statistics.FavoriteCount.ToString();
                        obj.commentCount = response.Items[0].Statistics.CommentCount.ToString();

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

        string GetMainImg(ThumbnailDetails thumbnailDetails)
        {
            if (thumbnailDetails == null)
                return string.Empty;
            return (thumbnailDetails.Maxres ?? thumbnailDetails.Standard ?? thumbnailDetails.High)?.Url;
        }

        string GetThumbnailImg(ThumbnailDetails thumbnailDetails)
        {
            if (thumbnailDetails == null)
                return string.Empty;
            return (thumbnailDetails.Medium ?? thumbnailDetails.Default__ ?? thumbnailDetails.High)?.Url;
        }
        string GetDuration(String duration)
        {
            var Duration = Convert.ToInt32(System.Xml.XmlConvert.ToTimeSpan(duration).TotalSeconds);
            return Duration.ToString();
        }
    }
}
