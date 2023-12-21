using ProniaOnionAPİ.Application.Abstractions.Repositories;
using ProniaOnionAPİ.Domain.Entities;
using ProniaOnionAPİ.Persistence.Contexts;
using ProniaOnionAPİ.Persistence.Implementations.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Persistence.Implementations.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
