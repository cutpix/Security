using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Http;

namespace Extensions.AspNet.Authentication.Instagram
{
    public class InstagramAuthenticationOptions : OAuthAuthenticationOptions
    {
        public InstagramAuthenticationOptions()
        {
            AuthenticationScheme = InstagramAuthenticationDefaults.AuthenticationScheme;
            Caption = AuthenticationScheme;
            CallbackPath = new PathString("/signin-instagram");
            AuthorizationEndpoint = InstagramAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = InstagramAuthenticationDefaults.TokenEndpoint;
            Scope.Add("basic");
        }
    }
}