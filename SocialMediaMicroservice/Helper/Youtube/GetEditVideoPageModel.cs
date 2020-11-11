using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using SocialMediaMicroservice.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Helper.Youtube
{
    public class GetEditVideoPageModel
    {

        public async Task<EditVideoPageModel> Run()
        {
            try
            {
                var model = new EditVideoPageModel();
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    // ApiKey = "AIzaSyDxag54giZC4yPfNCa20z_RkVu01Zyehxs", // need to change later
                    ApiKey = "AIzaSyA1N-_xPPAz-O1l-RPe7KouhDUdcobCw_M",        //New api key by Suryabhan
                    ApplicationName = this.GetType().ToString()
                });

                // get category of US region
                var categories = youtubeService.VideoCategories.List("snippet");
                var languages = youtubeService.I18nLanguages.List("snippet");
                var playList = youtubeService.Playlists.List("snippet");
                //var contents = youtubeService..List("snippet");

                //playList.Callbac = true;

                categories.RegionCode = "US"; // need to change later
                var listLanguage = languages.Execute().Items.Select(i => new SelectItem()
                {
                    id = i.Id,
                    name = i.Snippet.Name,
                }).ToList();
                // cast category to a list return to client for render
                var listCategory = categories.Execute().Items.Where(i => i.Snippet.Assignable == true).Select(i => new SelectItem()
                {
                    id = i.Id,
                    name = i.Snippet.Title,
                }).ToList();
                // Code modifier by suryabhan
                //var listPlayList = playList.Execute().Items; 
                var playlist = new Playlist();
                playlist.Snippet = new PlaylistSnippet();
                playlist.Snippet.Title = "Test Playlist";
                playlist.Snippet.Description = "A playlist create with the youtub API v3";
                playlist.Status = new PlaylistStatus();
                playlist.Status.PrivacyStatus = "public";
                playlist = await youtubeService.Playlists.Insert(playlist, "snippet,status").ExecuteAsync();

                var newPlaylistItem = new PlaylistItem();
                newPlaylistItem.Snippet = new PlaylistItemSnippet();
                newPlaylistItem.Snippet.PlaylistId = playlist.Id;
                newPlaylistItem.Snippet.ResourceId = new ResourceId();
                newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
                newPlaylistItem.Snippet.ResourceId.VideoId = "GNRMeaz6QRI";
                newPlaylistItem = await youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();


                model.categories = listCategory;
                model.languages = listLanguage;
                model.contents = new List<SelectItem>()
                {
                    new SelectItem()
                    {
                        id = ((int)EVideoContentType.ForAll).ToString(),
                        name = EVideoContentType.ForAll.ToString(),
                    },
                    new SelectItem()
                    {
                        id = ((int)EVideoContentType.ForAdult).ToString(),
                        name = EVideoContentType.ForAdult.ToString(),
                    },
                    new SelectItem()
                    {
                        id = ((int)EVideoContentType.ForKid).ToString(),
                        name = EVideoContentType.ForKid.ToString(),
                    }
                };
                model.levels = new List<SelectItem>()
                {
                    new SelectItem()
                    {
                        id = ((int)EVideoLevel.Basic).ToString(),
                        name = EVideoLevel.Basic.ToString(),
                    },
                    new SelectItem()
                    {
                        id = ((int)EVideoLevel.Bronze).ToString(),
                        name = EVideoLevel.Bronze.ToString(),
                    },
                    new SelectItem()
                    {
                        id = ((int)EVideoLevel.Sliver).ToString(),
                        name = EVideoLevel.Sliver.ToString(),
                    },
                    new SelectItem()
                    {
                        id = ((int)EVideoLevel.Gold).ToString(),
                        name = EVideoLevel.Gold.ToString(),
                    },
                };
                model.privacies = new List<SelectItem>()
                {
                    new SelectItem()
                    {
                        id = ((int)EVideoPrivacy.Standard).ToString(),
                        name = EVideoPrivacy.Standard.ToString(),
                    },
                    new SelectItem()
                    {
                        id = ((int)EVideoPrivacy.CC).ToString(),
                        name = EVideoPrivacy.CC.ToString(),
                    },
                };
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
