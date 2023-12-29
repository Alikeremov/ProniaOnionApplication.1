﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Infrastructure.Implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Infrastructure.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IJwtTokenHandler,JwtTokenHandler>();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issuer"],
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"])),
                    LifetimeValidator = (_, expired, key, _) =>key!=null? expired>DateTime.UtcNow : false
                    #region Uzun yol
                    //LifetimeValidator = (_, expired, key, _) =>
                    //{
                    //    if(key != null)
                    //    {
                    //        if (expired > DateTime.UtcNow)
                    //        {
                    //            return true;
                    //        }
                    //    }
                    //} 
                    #endregion
                };
            });
            services.AddAuthorization();
            return services;
        }
    }
}
