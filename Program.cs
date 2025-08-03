using MasterDegree.Interfaces;
using MasterDegree.Models;
using MasterDegree.Repository;
using MasterDegree.Services;
using MasterDegree.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();

    builder.Services.AddDbContext<MasterDegreeDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
    });

    builder.Services.AddScoped<IDataService, DataService>();
    builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddScoped<IDataRepository, DataRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();

    #region AuthorizationSettings

    var configuration = new ConfigurationBuilder()
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .Build();


    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                LifetimeValidator = TokenLifetimeValidator.Validate,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!)),
                ClockSkew = TimeSpan.Zero
            };
        });

    #endregion

    var app = builder.Build();

    app.UseCors(options =>
    {
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.AllowAnyOrigin();
    });

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    File.WriteAllText("startup-error.txt", ex.ToString());
    throw;
}