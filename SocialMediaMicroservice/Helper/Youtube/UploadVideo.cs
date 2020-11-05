using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
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
    public class UploadVideo
    {
        public async Task<bool> Run(YoutubeUploadModel obj)
        {
            try
            {
                UserCredential credential;
                using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
                {
                    // change later, this is a test user
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        // This OAuth 2.0 access scope allows an application to upload files to the
                        // authenticated user's YouTube channel, but doesn't allow other types of access.
                        new[] { YouTubeService.Scope.YoutubeUpload },
                        "user",
                        CancellationToken.None
                    );
                }

                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
                });

                var video = new Video();
                video.Snippet = new VideoSnippet();
                video.Snippet.Title = obj.title;
                video.Snippet.Description = obj.description;
                video.Snippet.Tags = new string[] { "tag1", "tag2", "Jungle" };
                video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
                //  video.Snippet.ChannelId = "";
                //  video.Snippet.ChannelTitle = "";
                //  video.Snippet.DefaultAudioLanguage = "";
                //  video.Snippet.DefaultLanguage = "";
                //  video.Snippet.Description = "";
                ////  video.Snippet.Etag = [""];
                //  video.Snippet.LiveBroadcastContent = "";
                //  video.Snippet.Localized = "";
                //  video.Snippet.PublishedAt = DateTime.Now;
                //  video.Snippet.PublishedAtRaw = "";
                //  video.Snippet.Thumbnails = "";


                video.Status = new VideoStatus();
                video.Status.PrivacyStatus = "public";// "unlisted"; // or "private" or "public"
                // Dinh Truong, 2nd November 2020 23:30, delete start
                //var filePath = @"E:\samplevideo\Sea waves & beach drone video _ Free HD Video - no copyright.mp4"; // Replace with path to actual movie file.
                // Dinh Truong 2nd November 2020 delete end

                string uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                string fPath = Path.Combine(uploads, obj.filepath.FileName);
                if (obj.filepath.Length > 0)
                {
                    using (Stream fStream = new FileStream(fPath, FileMode.Create))
                    {
                        await obj.filepath.CopyToAsync(fStream);
                    }
                }

                using (var fileStream = new FileStream(fPath, FileMode.Open))
                {
                    var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                    videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                    videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                    var x = await videosInsertRequest.UploadAsync();
                    if(x.Status == UploadStatus.Failed)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Console.WriteLine("{0} bytes sent.", progress.BytesSent);
                    break;

                case UploadStatus.Failed:
                    Console.WriteLine("An error prevented the upload from completing.\n{0}", progress.Exception);
                    break;
            }
        }

        void videosInsertRequest_ResponseReceived(Video video)
        {
            Console.WriteLine("Video id '{0}' was successfully uploaded.", video.Id);
        }

    }
}
