using Microsoft.EntityFrameworkCore;
using ProniaOnionAPİ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Persistence.Common
{
    internal static class GlobalQuerryFilterHandle
    {
        public static void ApplyFilter<T>(this ModelBuilder builder) where T : BaseEntity, new()
        {
            builder.Entity<T>().HasQueryFilter(x=>x.IsDeleted==false);
        }
        public static void ApplyQuerryFilters(this ModelBuilder builder)
        {
            builder.ApplyFilter<Product>();
            builder.ApplyFilter<Color>();
            builder.ApplyFilter<Category>();
            builder.ApplyFilter<Tag>();
            builder.ApplyFilter<TagProduct>();
            builder.ApplyFilter<ProductColor>();

        }
    }
}
