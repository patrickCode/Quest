using System;
using Common.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Common.Domain
{
    public interface IQueryService<T> where T: DocumentEntity
    {
        T Get(string id);
        Task<T> GetAsync(string id);
        List<T> Get();
        Task<List<T>> GetAsync();
        List<T> Get(Func<T, bool> query);
        Task<List<T>> GetAsync(Func<T, bool> query);
    }
}