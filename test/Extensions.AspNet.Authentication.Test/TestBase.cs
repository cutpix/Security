using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.TestHost;
using Microsoft.Framework.DependencyInjection;

namespace Extensions.AspNet.Authentication.Test
{
    public class TestBase
    {
        protected static TestServer CreateServer(Action<IApplicationBuilder> configure, Action<IServiceCollection> configureServices, Func<HttpContext, bool> handler = null)
        {
            return TestServer.Create(app =>
            {
                configure?.Invoke(app);

                app.Use(async (context, next) =>
                {
                    if (handler == null || !handler(context))
                    {
                        await next();
                    }
                });
            }, configureServices);
        }
    }
}