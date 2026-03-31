namespace SocialMedia.Domain.Constants;
public static class ApiRoutes
{
    public const string Base = "api";
    public const string Version1 = Base + "/v1";

    public static class Users
    {
        public const string Root = Version1 + "/users";
        public const string Engagement = Root + "/engagement";
        public const string Profile = Root + "/profile/{id}";
    }

    public static class Auth
    {
        public const string Root = Version1 + "/auth";
    }

    public static class Feed
    {
        public const string Root = Version1 + "/feed";
    }

    public static class Posts
    {
        public const string Root = Version1 + "/posts";
        public const string Feed = Root + "/feed";
        public const string Create = Root + "/create";
        public const string Update = Root + "/update/{id}";
        public const string Delete = Root + "/delete/{id}";
    }

    public static class Comments
    {
        public const string Root = Version1 + "/comments";
        public const string Create = Root + "/create";
        public const string Delete = Root + "/delete/{id}";
    }

    public static class Likes
    {
        public const string Root = Version1 + "/likes";
        public const string Create = Root + "/create";
        public const string Delete = Root + "/delete/{id}";
    }

}
