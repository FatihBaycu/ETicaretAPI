using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IWriteRepository<T>:IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> datas);
        bool Remove(T entity);
        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(string id);
        bool UpdateAsync(T entity);
        Task<int> SaveAsync();
    }
}
