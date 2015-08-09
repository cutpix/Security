namespace Extensions.AspNet.Authentication.Instagram
{
    public static class InstagramAuthenticationDefaults
    {
        public const string AuthenticationScheme = "Instagram";

        public const string AuthorizationEndpoint = "https://api.instagram.com/oauth/authorize/";

        public const string TokenEndpoint = "https://api.instagram.com/oauth/access_token";
    }
}