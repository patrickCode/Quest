using System;
using System.Linq;
using Common.Model;
using Common.Interfaces;
using Common.Exceptions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Services.CategoryServices.QueryServices
{
    public class CategoriesQueryService : ICategoriesQueryService
    {
        private readonly IDocumentReader<CategoryDto> _reader;
        public CategoriesQueryService(IDocumentReader<CategoryDto> reader)
        {
            _reader = reader;
        }

        public async Task<List<CategoryDto>> GetAsync()
        {
            return await _reader.GetAsync();
        }

        public List<CategoryDto> Get()
        {
            return _reader.Get();
        }

        public List<CategoryDto> Get(Func<CategoryDto, bool> query)
        {
            return _reader.Query(query);
        }

        public async Task<List<CategoryDto>> GetAsync(Func<CategoryDto, bool> query)
        {
            return await _reader.QueryAsync(query);
        }

        public CategoryDto Get(string id)
        {
            return _reader.Get(id);
        }

        public async Task<CategoryDto> GetAsync(string id)
        {
            return await _reader.GetAsync(id);
        }

        public async Task<CategoryDto> GetByCodeAsync(string code)
        {
            return (await _reader.QueryAsync(category => category.Code == code)).FirstOrDefault();
        }

        public CategoryDto GetByCode(string code)
        {
            return _reader.Query(category => category.Code == code).FirstOrDefault();
        }

        public async Task<List<SubCategoryDto>> GetSubCategoriesByCategoryCodeAsync(string categoryCode)
        {
            var requiredCategory = (await _reader.QueryAsync(category => category.Code == categoryCode)).FirstOrDefault();
            if (requiredCategory == null)
                throw new DomainValidationException(Guid.NewGuid(), "No Category exists by the given code", 18);
            return requiredCategory.SubCatgories;
        }

        public List<SubCategoryDto> GetSubCategoriesByCategoryCode(string categoryCode)
        {
            var requiredCategory = _reader.Query(category => category.Code == categoryCode).FirstOrDefault();
            if (requiredCategory == null)
                throw new DomainValidationException(Guid.NewGuid(), "No Category exists by the given code", 18);
            return requiredCategory.SubCatgories;
        }
    }
}