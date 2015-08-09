namespace Extensions.AspNet.Authentication.Vkontakte
{
    public static class VkontakteAuthenticationDefaults
    {
        public const string AuthenticationScheme = "Vkontakte";

        public const string AuthorizationEndpoint = "https://oauth.vk.com/authorize";

        public const string TokenEndpoint = "https://oauth.vk.com/access_token";

        public const string ApiVersion = "5.35";
    }
}