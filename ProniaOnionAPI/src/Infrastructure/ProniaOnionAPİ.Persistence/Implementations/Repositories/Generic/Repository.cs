using Microsoft.EntityFrameworkCore;
using ProniaOnionAPİ.Application.Abstractions.Repositories;
using ProniaOnionAPİ.Domain.Entities;
using ProniaOnionAPİ.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Persistence.Implementations.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }
        public IQueryable<T> GetAll(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null,
            bool isDescending = false,
            int skip = 0, int take = 0,
            bool isTracking = false,
            bool ignoreQuery = false,
            params string[] includes)
        {

            IQueryable<T> query = _table;
            if (expression != null) query = query.Where(expression);


            if (orderExpression != null)
            {
                if (isDescending) query = query.OrderByDescending(orderExpression);
                else query = query.OrderBy(orderExpression);
            }
            if (skip != 0) query = query.Skip(skip);
            if (take != 0) query = query.Take(take);
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            if(ignoreQuery) query=query.IgnoreQueryFilters();
            return isTracking ? query : query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await _table.FirstOrDefaultAsync(c => c.Id == id);
            return entity;
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }
        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool Cheeck(Expression<Func<T, bool>> expression)
        {
            if (_table.Any(expression)) return true;
            return false;
        }

        
    }
}
