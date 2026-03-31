/***

Md Tausif Ali

***/

namespace SocialMedia.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialMedia.Infrastructure.Persistence;
using SocialMedia.Domain.Constants;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Service.UserService;
using SocialMedia.Infrastructure.Repository.UserRepository;
using SocialMedia.Infrastructure.Service.TokenService;
using SocialMedia.Infrastructure.Repository.RefreshTokenRepository;
using SocialMedia.Infrastructure.Service.BannedWordsCacheService;
using SocialMedia.Infrastructure.Service.ContentModerationService;
using SocialMedia.Infrastructure.Service.EngagementService;
using SocialMedia.Infrastructure.Service.PostService;
using SocialMedia.Infrastructure.Repository.PostRepository;
using SocialMedia.Infrastructure.Repository.BannedWordsRepository;
using SocialMedia.Infrastructure.Service.CommentService;
using SocialMedia.Infrastructure.Service.LikeService;
using SocialMedia.Infrastructure.Service.FeedService;
using SocialMedia.Infrastructure.Repository.CommentRepository;
using SocialMedia.Infrastructure.Repository.LikeRepository;
using SocialMedia.Infrastructure.Repository.FeedRepository;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(config.GetConnectionString(AppConstants.DefaultConnection)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddHttpContextAccessor();
        services.AddMemoryCache();

        services.AddScoped<IUserService,UserService>();
        services.AddScoped<IPostService,PostService>();
        services.AddScoped<ILikeService,LikeService>();
        services.AddScoped<IFeedService,FeedService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICommentService,CommentService>();
        services.AddScoped<IEngagementService,EngagementService>();
        services.AddScoped<IBannedWordsCacheService,BannedWordsCacheService>();
        services.AddScoped<IContentModerationService,ContentModerationService>();

        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IPostRepository,PostRepository>();
        services.AddScoped<ICommentRepository,CommentRepository>();
        services.AddScoped<IFeedRepository,FeedRepository>();
        services.AddScoped<ILikeRepository,LikeRepository>();
        services.AddScoped<IBannedWordsRepository,BannedWordsRepository>();
        services.AddScoped<IRefreshTokenRepository,RefreshTokenRepository>();

        return services;
    }
}
