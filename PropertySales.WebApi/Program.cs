using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using PropertySales.Application;
using PropertySales.Application.Common.Mappings;
using PropertySales.Application.Interfaces;
using PropertySales.SecureAuth;
using PropertySales.Domain;
using PropertySales.Persistence;
using PropertySales.Persistence.Contexts;
using PropertySales.Persistence.Initializers;
using PropertySales.WebApi.Middlewares;

var logger = NLog.LogManager.Setup()
    .LoadConfigurationFromFile("NLog.config", false).GetCurrentClassLogger();
logger.Debug("Init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers(options =>
        {
            options.CacheProfiles.Add("Caching",
                new CacheProfile()
                {
                    Duration = 300,
                    Location = ResponseCacheLocation.Any
                });
            options.CacheProfiles.Add("NoCaching", 
                new CacheProfile()
                {
                    Location = ResponseCacheLocation.None,
                    NoStore = true
                });
        })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IPropertySalesDbContext).Assembly));
    });

    builder.Services.AddApplication();
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddSecurity(builder.Configuration);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });

    builder.Services.AddIdentity<User, IdentityRole<long>>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = false;
    }).AddEntityFrameworkStores<PropertySalesDbContext>();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    });
    
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        try
        {
            var context = serviceProvider.GetRequiredService<PropertySalesDbContext>();
            DbInitializer.Initialize(context);

            var logContext = serviceProvider.GetRequiredService<LogDbContext>();
            LogDbInitializer.Initialize(logContext);
            
            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<User>>();
            var rolesManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole<long>>>();
            await RoleInitializer.InitializerAsync(rolesManager, userManager);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Stopped on account of error - {ex}", ex);
            throw;
        }
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();
    
    app.UseHttpsRedirection();

    app.UseCors("AllowAll");
    
    app.UseResponseCaching();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch(Exception ex)
{
    logger.Error(ex, "The program was stopped because of an exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}