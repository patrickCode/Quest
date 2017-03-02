using System;
using Common.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Common.Interfaces
{
    public interface IDocumentReader<Doc> where Doc: DocumentEntity
    {
        bool Exists(string id);
        Task<bool> ExistsAsync(string id);
        Doc Get(string id);
        Task<Doc> GetAsync(string id);
        List<Doc> Query(string sqlQuery);
        Task<List<Doc>> QueryAsync(string sqlQuery);
        List<Doc> Query(Func<Doc, bool> query);
        Task<List<Doc>> QueryAsync(Func<Doc, bool> query);
    }
}