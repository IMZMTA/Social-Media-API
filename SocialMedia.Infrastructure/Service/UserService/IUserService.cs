using Microsoft.AspNetCore.Http;
using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Service.UserService;

public interface IUserService
{
    Task<LogInResponseModel> Login(LogInModel loginModel, CancellationToken cancellationToken);
    Task Logout(HttpContext httpContext, CancellationToken cancellationToken);
    Task<UserModel> RegisterUser(SignUpModel user, CancellationToken cancellationToken);
    Task<RefreshTokenResponseModel> RefreshToken( HttpContext context, CancellationToken cancellationToken);
    Task<int> RemoveByIdAsync( int userId, CancellationToken cancellationToken);
    Task<UserModel?> UpdateByIdAsync( UserModel user, CancellationToken cancellationToken);
    Task<UserModel?> GetById( int userId, CancellationToken cancellationToken);
    Task<List<UserList>> GetAll( CancellationToken cancellationToken);
}
