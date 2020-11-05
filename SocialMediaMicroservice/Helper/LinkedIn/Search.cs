using SocialMediaMicroservice.Model;
using Sparkle.LinkedInNET;
using Sparkle.LinkedInNET.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Helper.LinkedIn
{
	public class Search
	{
		/// <summary>
		/// Get LinkedIn API Authorization 
		/// </summary>
		/// <param name="redirectUrl"></param>
		/// <param name="LinkedInClientId"></param>
		/// <param name="LinkedInClientSecret"></param>
		/// <returns></returns>
		public Uri GetAuthorizationUrl(string redirectUrl, string LinkedInClientId, string LinkedInClientSecret)
		{
			var api = ConfigLinkedInAPI(LinkedInClientId, LinkedInClientSecret);
			var scope = Sparkle.LinkedInNET.OAuth2.AuthorizationScope.ReadBasicProfile |
			Sparkle.LinkedInNET.OAuth2.AuthorizationScope.ReadEmailAddress |
			Sparkle.LinkedInNET.OAuth2.AuthorizationScope.ReadContactInfo;
			var state = Guid.NewGuid().ToString();
			var url = api.OAuth2.GetAuthorizationUrl(scope, state, redirectUrl);
			return url;
		}

		/// <summary>
		/// Authenitcate API from 
		/// </summary>
		/// <param name="LinkedInClientId"></param>
		/// <param name="LinkedInClientSecret"></param>
		/// <returns></returns>
		public LinkedInApi ConfigLinkedInAPI(string linkedInClientId, string linkedInClientSecret)
		{
			var config = new LinkedInApiConfiguration(linkedInClientId, linkedInClientSecret);
			var api = new LinkedInApi(config);
			return api;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <param name="redirectUrl"></param>
		/// <param name="linkedInClientId"></param>
		/// <param name="LinkedInClientSecret"></param>
		/// <returns></returns>
		public async Task<Person> ReadMyProfile(string code, string redirectUrl, string linkedInClientId, string linkedInClientSecret)
		{
			try
			{
				var api = ConfigLinkedInAPI(linkedInClientId, linkedInClientSecret);
				var userToken = api.OAuth2.GetAccessToken(code, redirectUrl);
				var user = new Sparkle.LinkedInNET.UserAuthorization(userToken.AccessToken);
				var fieldSelector = FieldSelector.For<Person>().WithFirstName()
				.WithLastName()
				.WithEmailAddress()
				.WithFormattedName()
						.WithEmailAddress()
						.WithHeadline()

						.WithLocationName()
						.WithLocationCountryCode()

						.WithPictureUrl()
						.WithPublicProfileUrl()
						.WithSummary()
						.WithIndustry()

						.WithPositions()
						.WithPositionsSummary()
						.WithThreeCurrentPositions()
						.WithThreePastPositions()

						.WithProposalComments()
						.WithAssociations()
						.WithInterests()
						.WithLanguageId()
						.WithLanguageName()
						.WithLanguageProficiency()
						.WithCertifications()
						.WithEducations()
						.WithFullVolunteer()
						.WithPatents();
				var profile = await api.Profiles.GetMyProfileAsync(user, null, fieldSelector);
				return profile;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
