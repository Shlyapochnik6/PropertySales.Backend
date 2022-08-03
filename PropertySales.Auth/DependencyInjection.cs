using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PropertySales.SecureAuth.Interfaces;

namespace PropertySales.SecureAuth;

public static class DependencyInjection
{
    public static IServiceCollection AddSecurity(this IServiceCollection services,
        IConfiguration configuration)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = key,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"]
                };
            });

        services.AddScoped<IJwtGenerator, JwtGenerator>();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
