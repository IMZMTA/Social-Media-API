

using SocialMedia.Api.Extensions;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Config;
using SocialMedia.Api.Middleware;
using SocialMedia.Infrastructure.Persistence;
using SocialMedia.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddProjectServices(config);
builder.Services.AddCustomCors(config);
builder.Services.AddJwtAuthentication(config); 

var appSettings = config.GetSection(AppConstants.AppSettings).Get<AppSettings>() ?? throw new InvalidOperationException(Messages.MissingAppSetting);

if (appSettings.SwaggerEnabled)
{
    builder.Services.AddSwagger();
}

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>(); 

if (appSettings.SwaggerEnabled)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(AppConstants.DefaultCorsPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
    await BannedWordsSeeder.SeedAsync(dbContext);
}

app.Run();