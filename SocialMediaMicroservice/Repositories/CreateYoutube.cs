using JungleMicroserviceEntities.Entities;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JungleDynamoDBRequest.Services.DynamoDBService;

namespace SocialMediaMicroservice.Repositories
{
    public class CreateYoutube
    {

        #region Fields
        private readonly IAmazonDynamoDB _dynamoClient;
        private string TABLE_NAME = TableNameService.CHANNEL;
        #endregion

        #region Constructor
        public CreateYoutube(IAmazonDynamoDB dynamoClient)
        {
            _dynamoClient = dynamoClient;
        }
        #endregion

        #region Method
       
        #endregion
    }
}
