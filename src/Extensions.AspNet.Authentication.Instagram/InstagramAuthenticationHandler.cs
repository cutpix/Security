using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Http.Authentication;

namespace Extensions.AspNet.Authentication.Instagram
{
    internal class InstagramAuthenticationHandler : OAuthAuthenticationHandler<InstagramAuthenticationOptions>
    {
        public InstagramAuthenticationHandler(HttpClient backchannel) : base(backchannel)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var notification = new OAuthAuthenticatedContext(Context, Options, Backchannel, tokens)
            {
                Properties = properties,
                Principal = new ClaimsPrincipal(identity)
            };

            var user = tokens.Response["user"];

            var userId = user["id"]?.ToString();
            if (userId != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var username = user["username"]?.ToString();
            if (username != null)
            {
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, username, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var fullname = user["full_name"]?.ToString();
            if (fullname != null)
            {
                identity.AddClaim(new Claim("urn:instagram:full_name", fullname, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var profilePic = user["profile_picture"]?.ToString();
            if (profilePic != null)
            {
                identity.AddClaim(new Claim("urn:instagram:profile_picture", profilePic, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var website = user["website"]?.ToString();
            if (website != null)
            {
                identity.AddClaim(new Claim("urn:instagram:website", website, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            await Options.Notifications.Authenticated(notification);

            return new AuthenticationTicket(notification.Principal, notification.Properties, notification.Options.AuthenticationScheme);
        }

        protected override string FormatScope()
        {
            return string.Join("+", Options.Scope);
        }
    }
}