using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.DataProtection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.WebEncoders;

namespace Extensions.AspNet.Authentication.Vkontakte
{
    public class VkontakteAuthenticationMiddleware : OAuthAuthenticationMiddleware<VkontakteAuthenticationOptions>
    {
        public VkontakteAuthenticationMiddleware(RequestDelegate next,
                                                 IDataProtectionProvider dataProtectionProvider,
                                                 ILoggerFactory loggerFactory,
                                                 IUrlEncoder encoder,
                                                 IOptions<SharedAuthenticationOptions> externalOptions,
                                                 IOptions<VkontakteAuthenticationOptions> options,
                                                 ConfigureOptions<VkontakteAuthenticationOptions> configureOptions = null)
            : base(next, dataProtectionProvider, loggerFactory, encoder, externalOptions, options, configureOptions)
        {
        }

        protected override AuthenticationHandler<VkontakteAuthenticationOptions> CreateHandler()
        {
            return new VkontakteAuthenticationHandler(Backchannel);
        }
    }
}