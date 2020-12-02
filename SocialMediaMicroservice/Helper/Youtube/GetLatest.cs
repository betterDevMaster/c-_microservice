using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using SocialMediaMicroservice.Model;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Helper.Youtube
{
    //Service Class Added by Suryabhan
    public class GetLatest
    { 
        ResponseModel response;
        public GetLatest()
        {
            response = new ResponseModel();
        }
        //show videos by region code.
        public async Task<ResponseModel> GetVideoList(string regionCode)
        {
            try
            {
                var ApiKey = "AIzaSyA8hPK8F-8BbO-8H6tQZuiopY5nYES1UR0";
                var model = new EditVideoPageModel();
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = ApiKey,
                    ApplicationName = this.GetType().ToString()
                });

                // get category of US region
                //var categories = youtubeService.VideoCategories.List("snippet");
                //var languages = youtubeService.I18nLanguages.List("snippet");
                //var playList = youtubeService.Playlists.List("snippet");
                //var contents = youtubeService..List("snippet");
                //gapi.client.youtube.videos.list({ part: 'snippet,id', chart: 'mostPopular', regionCode: 'RU', maxResults: 8 })
                //playList.Callbac = true;


                VideosResource.ListRequest search_response = youtubeService.Videos.List("snippet,contentDetails,statistics");
                //search_response.Type = "video";
                search_response.RegionCode = regionCode;
                search_response.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;
                search_response.MaxResults = 50;
                //search_response.ChannelId = channelId;
                VideoListResponse searchresponse = search_response.Execute();

                List<string> videos = new List<string>();
                List<string> channels = new List<string>();
                List<string> playlists = new List<string>();

                // Add each result to the appropriate list, and then display the lists of
                // matching videos, channels, and playlists.
                List<YoutubeModel> searchmodel = new List<YoutubeModel>();
                YoutubeModel obj = null;
                foreach (var searchResult in searchresponse.Items)
                {
                    obj = new YoutubeModel();
                    obj.title = searchResult.Snippet.Title;
                    obj.regionCode = regionCode;
                    obj.url = "https://youtu.be/" + searchResult.Id;
                    obj.description = searchResult.Snippet.Description;
                    obj.picture = GetMainImg(searchResult.Snippet.Thumbnails);
                    obj.thumbnail = GetThumbnailImg(searchResult.Snippet.Thumbnails);
                    obj.publishedAt = searchResult.Snippet.PublishedAt;
                    obj.channelId = searchResult.Snippet.ChannelId;
                    obj.channelTitle = searchResult.Snippet.ChannelTitle;
                    obj.duration = GetDuration(searchResult.ContentDetails.Duration);
                    obj.viewCount = searchResult.Statistics.ViewCount.ToString();
                    obj.likeCount = searchResult.Statistics.LikeCount.ToString();
                    obj.dislikeCount = searchResult.Statistics.DislikeCount.ToString();
                    obj.favoriteCount = searchResult.Statistics.FavoriteCount.ToString();
                    obj.commentCount = searchResult.Statistics.CommentCount.ToString();

                    searchmodel.Add(obj);
                    videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, "https://youtu.be/" + searchResult.Id));
                }
                response.Data = searchmodel;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            response.Status_Code = 200;
            response.Success = true;
            return response;
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
