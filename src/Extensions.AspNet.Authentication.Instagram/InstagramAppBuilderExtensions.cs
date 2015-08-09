using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.OptionsModel;

namespace Extensions.AspNet.Authentication.Instagram
{
    public static class InstagramAppBuilderExtensions
    {
        public static IApplicationBuilder UseInstagramAuthentication(this IApplicationBuilder app, Action<InstagramAuthenticationOptions> configureOptions = null, string optionsName = "")
        {
            var options = new ConfigureOptions<InstagramAuthenticationOptions>(configureOptions ?? (_ => { }))
            {
                Name = optionsName
            };

            return app.UseMiddleware<InstagramAuthenticationMiddleware>(options);
        }
    }
}