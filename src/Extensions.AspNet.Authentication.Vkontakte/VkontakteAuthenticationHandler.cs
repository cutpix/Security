using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Http.Extensions;
using Newtonsoft.Json.Linq;

namespace Extensions.AspNet.Authentication.Vkontakte
{
    internal class VkontakteAuthenticationHandler : OAuthAuthenticationHandler<VkontakteAuthenticationOptions>
    {
        public VkontakteAuthenticationHandler(HttpClient backchannel) : base(backchannel)
        {
        }

        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            var display = GetDisplayType(Options.DisplayType);
            var state = Options.StateDataFormat.Protect(properties);
            var scope = FormatScope();

            var queryBuilder = new QueryBuilder
            {
                {"client_id", Options.ClientId},
                {"redirect_uri", redirectUri},
                {"display", display},
                {"scope", scope},
                {"response_type", "code"},
                {"v", VkontakteAuthenticationDefaults.ApiVersion},
                {"state", state}
            };

            return Options.AuthorizationEndpoint + queryBuilder;
        }

        protected override async Task<OAuthTokenResponse> ExchangeCodeAsync(string code, string redirectUri)
        {
            var queryBuilder = new QueryBuilder
            {
                {"client_id", Options.ClientId},
                {"client_secret", Options.ClientSecret},
                {"redirect_uri", redirectUri},
                {"code", code}
            };

            var response = await Backchannel.GetAsync(Options.TokenEndpoint + queryBuilder, Context.RequestAborted);
            response.EnsureSuccessStatusCode();

            string json;
            using (var content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }

            var payload = JObject.Parse(json);
            return new OAuthTokenResponse(payload);
        }

        protected override string FormatScope()
        {
            return string.Join(",", Options.Scope);
        }

        private static string GetDisplayType(VkontakteDisplayType displayType)
        {
            switch (displayType)
            {
                case VkontakteDisplayType.Page:
                {
                    return "page";
                }
                case VkontakteDisplayType.Popup:
                {
                    return "popup";
                }
                case VkontakteDisplayType.Mobile:
                {
                    return "mobile";
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(displayType), displayType, null);
                }
            }
        }
    }
}