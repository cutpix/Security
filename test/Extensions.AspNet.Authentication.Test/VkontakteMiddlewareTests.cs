using System.Net;
using System.Threading.Tasks;
using Extensions.AspNet.Authentication.Vkontakte;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.WebEncoders;
using Shouldly;
using Xunit;

namespace Extensions.AspNet.Authentication.Test
{
    public class VkontakteMiddlewareTests : TestBase
    {
        [Fact]
        public async Task BuildChallengeTest()
        {
            var server = CreateServer(
                app =>
                {
                    app.UseCookieAuthentication();
                    app.UseVkontakteAuthentication();
                },
                services =>
                {
                    services.AddAuthentication();
                    services.Configure<VkontakteAuthenticationOptions>(options =>
                    {
                        options.ClientId = "TestClientId";
                        options.ClientSecret = "TestClientSecret";
                    });
                },
                context =>
                {
                    context.Authentication.ChallengeAsync("Vkontakte").GetAwaiter().GetResult();
                    return true;
                });

            var response = await server.CreateRequest("http://test.com/challenge").GetAsync();
            response.StatusCode.ShouldBe(HttpStatusCode.Redirect);

            var redirectUri = response.Headers.Location.AbsoluteUri;
            redirectUri.ShouldStartWith("https://oauth.vk.com/authorize");
            redirectUri.ShouldContain("response_type=code");
            redirectUri.ShouldContain("redirect_uri=" + UrlEncoder.Default.UrlEncode("http://test.com/signin-vkontakte"));
            redirectUri.ShouldContain("client_id=TestClientId");
            redirectUri.ShouldContain("display=popup");
            redirectUri.ShouldContain("scope=");
            redirectUri.ShouldContain("v=5.35");
            redirectUri.ShouldContain("state=");
        }
    }
}