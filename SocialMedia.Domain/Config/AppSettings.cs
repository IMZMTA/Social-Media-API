namespace SocialMedia.Domain.Config;

public class AppSettings
{
    public bool SwaggerEnabled { get; set; }
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    public JwtSettings Jwt { get; set; } = new();
}

public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
