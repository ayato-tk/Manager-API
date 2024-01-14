

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;

namespace Manager.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly ManagerContext _context;

        protected BaseRepository(ManagerContext context)
        {
            _context = context;
        }

        public virtual async Task<T> CreateAsync(T obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task<T> UpdateAsync(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task RemoveAsync(long id)
        {
            var obj = await GetByIdAsync(id);

                if (obj!= null)
                _context.Remove(obj);
                await _context.SaveChangesAsync();

        }


        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>()
                                    .AsNoTracking()
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                                    .AsNoTracking()
                                    .ToListAsync();
        }
    }
}