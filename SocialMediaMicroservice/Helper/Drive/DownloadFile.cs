using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using SocialMediaMicroservice.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Helper.Drive
{
    public class DownloadFile
    {
        public async Task<string> GetFileID(DriveSearchModel model)
        {
            try
            {
                var driveService = new DriveService(new BaseClientService.Initializer()
                {
                    ApiKey= "AIzaSyA8hPK8F-8BbO-8H6tQZuiopY5nYES1UR0",
                    ApplicationName = this.GetType().ToString()
                });
                // Get the client request object for the bucket and desired object.
                Google.Apis.Drive.v3.Data.File file = new Google.Apis.Drive.v3.Data.File();

                var searchRequest = driveService.Files.List();                
                searchRequest.Q = "name='" + model.filename + "'";
                searchRequest.Spaces = "drive";
                searchRequest.Fields = "id, name";
                FileList result = await searchRequest.ExecuteAsync();
                string fileid = string.Empty;
                if(result != null && result.Files.Count > 0)
                {
                    fileid = result.Files.First().Id;
                }
                return fileid;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }        
    }
}
