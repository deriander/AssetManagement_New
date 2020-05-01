using AssetManagement.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repository.Interface
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> Post(T Entity);
        Task<List<T>> Get();
        Task<T> Get(int Id);      
        Task<T> Put(T Entity);
        Task<T> Delete(int Id);
    }
}
