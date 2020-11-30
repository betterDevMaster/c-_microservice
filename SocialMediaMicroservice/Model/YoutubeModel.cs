using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Model
{
    public class YoutubeModel
    {
        public string title { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string regionCode { get; set; }
        public string picture { get; set; }
        public string thumbnail { get; set; }
        public string publishedAt { get; set; }
        public string channelId { get; set; }
        public string channelTitle { get; set; }
        public string duration { get; set; }
        public string viewCount { get; set; }
        public string likeCount { get; set; }
        public string dislikeCount { get; set; }
        public string favoriteCount { get; set; }
        public string commentCount { get; set; }
    }
    public class YoutubeSearchModel
    {
        public string title { get; set; }
        public string url { get; set; }
        public string regionCode { get; set; }
        public string description { get; set; }
        public string picture { get; set; }
        public string thumbnail { get; set; }
        public string publishedAt { get; set; }
        public string channelId { get; set; }
        public string channelTitle { get; set; }
        public string duration { get; set; }
        public string viewCount { get; set; }
        public string likeCount { get; set; }
        public string dislikeCount { get; set; }
        public string favoriteCount { get; set; }
        public string commentCount { get; set; }
    }
    public class YoutubeUploadModel
    {
        public string title { get; set; }
        public IFormFile filepath { get; set; }
        public string description { get; set; }
        public bool isMonetize { get; set; }
        public EVideoLevel level { get; set; }
        public EVideoPrivacy privacy { get; set; }
        public string language { get; set; }
        public IFormFile thumbnail { get; set; }
        public bool isUploadToYouTube { get; set; }
        public string youtubeChannelId { get; set; }
        public string categories { get; set; }
        public EVideoContentType contentType { get; set; }

    }
    public class EditVideoPageModel
    {
        public List<SelectItem> categories { get; set; }
        public List<SelectItem> languages { get; set; }
        public List<SelectItem> playLists { get; set; }
        public List<SelectItem> levels { get; set; }
        public List<SelectItem> privacies { get; set; }
        public List<SelectItem> contents { get; set; }

    }
    public class SelectItem
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    //DO NOT CONFUSED with Youtube Channel and Our VideoJungle Channel~
    public class GetLatest
    {
        public string YoutubeChannelName { get; set; }
        public string YoutubeChannelIcon { get; set; }
        public string videoTitle { get; set; }
        public string videoDescription { get; set; }
    }
}
