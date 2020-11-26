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
        public async Task<ResponseModel> GetVideoList(string regioncode)
        {
            try
            {
                var model = new EditVideoPageModel();
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                   // ApiKey = "AIzaSyA1N-_xPPAz-O1l-RPe7KouhDUdcobCw_M",
                    ApiKey = "AIzaSyA8hPK8F-8BbO-8H6tQZuiopY5nYES1UR0",
                    ApplicationName = this.GetType().ToString()
                });

                // get category of US region
                var categories = youtubeService.VideoCategories.List("snippet");
                var languages = youtubeService.I18nLanguages.List("snippet");
                var playList = youtubeService.Playlists.List("snippet");
                //var contents = youtubeService..List("snippet");
                //gapi.client.youtube.videos.list({ part: 'snippet,id', chart: 'mostPopular', regionCode: 'RU', maxResults: 8 })
                //playList.Callbac = true;


                SearchResource.ListRequest search_response = youtubeService.Search.List("snippet");
                //search_response.Type = "video";
                search_response.RegionCode = regioncode;
                //search_response.Q = regioncode;
                search_response.MaxResults = 50;
                //search_response.ChannelId = channelId;
                SearchListResponse searchresponse =await search_response.ExecuteAsync();

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
                    switch (searchResult.Id.Kind)
                    {
                        case "youtube#video":
                            obj.title = searchResult.Snippet.Title;
                            obj.description = searchResult.Snippet.Description;
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
    }
}
