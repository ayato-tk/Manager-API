using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Domain.Entities;

namespace Manager.Infra.Interfaces
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> CreateAsync(T obj);

        Task<T> UpdateAsync(T obj);

        Task RemoveAsync(long id);

        Task<T> GetByIdAsync(long id);

        Task<List<T>> GetAllAsync();
    }
}