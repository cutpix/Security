using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.DataProtection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.WebEncoders;

namespace Extensions.AspNet.Authentication.Instagram
{
    internal class InstagramAuthenticationMiddleware : OAuthAuthenticationMiddleware<InstagramAuthenticationOptions>
    {
        public InstagramAuthenticationMiddleware(RequestDelegate next,
                                                 IDataProtectionProvider dataProtectionProvider,
                                                 ILoggerFactory loggerFactory,
                                                 IUrlEncoder encoder,
                                                 IOptions<SharedAuthenticationOptions> sharedOptions,
                                                 IOptions<InstagramAuthenticationOptions> options,
                                                 ConfigureOptions<InstagramAuthenticationOptions> configureOptions = null)
            : base(next, dataProtectionProvider, loggerFactory, encoder, sharedOptions, options, configureOptions)
        {
        }

        protected override AuthenticationHandler<InstagramAuthenticationOptions> CreateHandler()
        {
            return new InstagramAuthenticationHandler(Backchannel);
        }
    }
}