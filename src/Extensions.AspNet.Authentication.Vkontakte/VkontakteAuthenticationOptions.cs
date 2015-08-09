using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Http;

namespace Extensions.AspNet.Authentication.Vkontakte
{
    public class VkontakteAuthenticationOptions : OAuthAuthenticationOptions
    {
        public VkontakteAuthenticationOptions()
        {
            AuthenticationScheme = VkontakteAuthenticationDefaults.AuthenticationScheme;
            Caption = AuthenticationScheme;
            CallbackPath = new PathString("/signin-vkontakte");
            AuthorizationEndpoint = VkontakteAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = VkontakteAuthenticationDefaults.TokenEndpoint;
            SaveTokensAsClaims = false;
            DisplayType = VkontakteDisplayType.Popup;
        }

        public VkontakteDisplayType DisplayType { get; set; }
    }
}
