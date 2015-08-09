using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.OptionsModel;

namespace Extensions.AspNet.Authentication.Vkontakte
{
    public static class VkontakteAppBuilderExtensions
    {
        public static IApplicationBuilder UseVkontakteAuthentication(this IApplicationBuilder app, Action<VkontakteAuthenticationOptions> configureOptions = null, string optionsName = "")
        {
            var options = new ConfigureOptions<VkontakteAuthenticationOptions>(configureOptions ?? (_ => { }))
            {
                Name = optionsName
            };

            return app.UseMiddleware<VkontakteAuthenticationMiddleware>(options);
        }
    }
}