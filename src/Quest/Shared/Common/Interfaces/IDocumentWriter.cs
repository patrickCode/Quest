using Common.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Common.Interfaces
{
    public interface IDocumentWriter<Doc> where Doc : DocumentEntity
    {
        void Create(Doc document);
        Task CreateAsync(Doc document);
        void Create(List<Doc> document);
        Task CreateAsync(List<Doc> document);
        void Delete(string id);
        Task DeleteAsync(string id);
    }
}