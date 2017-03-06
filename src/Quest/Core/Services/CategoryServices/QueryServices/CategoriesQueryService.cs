using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Model;

namespace Services.CategoryServices.QueryServices
{
    public class CategoriesQueryService : ICategoriesQueryService
    {
        public List<CategoryDto> Get()
        {
            throw new NotImplementedException();
        }

        public List<CategoryDto> Get(Func<CategoryDto, bool> query)
        {
            throw new NotImplementedException();
        }

        public CategoryDto Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryDto>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryDto>> GetAsync(Func<CategoryDto, bool> query)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> GetAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
