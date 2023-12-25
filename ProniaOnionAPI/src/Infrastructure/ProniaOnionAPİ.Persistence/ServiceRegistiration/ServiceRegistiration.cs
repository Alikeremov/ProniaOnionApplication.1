using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProniaOnionAPİ.Application.Abstractions.Repositories;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Persistence.Contexts;
using ProniaOnionAPİ.Persistence.Implementations.Repositories;
using ProniaOnionAPİ.Persistence.Implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Persistence.ServiceRegistiration
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddPersistensServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
