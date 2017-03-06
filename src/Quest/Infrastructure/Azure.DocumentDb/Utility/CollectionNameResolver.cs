using System;
using Common.Model;
using Common.Exceptions;
using Common.Configuration;
using Common.Model.Metadata;
using System.Collections.Generic;

namespace Azure.DocumentDb.Utility
{
    public class CollectionNameResolver : ICollectionNameResolver
    {
        private readonly DocumentDbConfiguration _configuration;
        private readonly Dictionary<Type, string> _collectionNameMapping;

        public CollectionNameResolver(DocumentDbConfiguration configuration)
        {
            _configuration = configuration;
            _collectionNameMapping = new Dictionary<Type, string>()
            {
                {typeof(QuestionDto), _configuration.QuestionCollection },
                {typeof(CategoryDto), _configuration.CategoriesCollection },
                
                {typeof(MetadataBaseDto), _configuration.MetadataCollection },
                {typeof(QuestionTypeDto), _configuration.MetadataCollection },
                {typeof(AnswerTypeDto), _configuration.MetadataCollection },
                {typeof(DifficultyLevelDto), _configuration.MetadataCollection }
            };
        }

        public string Resolve(Type documentType)
        {
            if (_collectionNameMapping.ContainsKey(documentType))
                return _collectionNameMapping[documentType];
            throw new DocumentDbException(Guid.NewGuid(), "No collection for this type of document", 17);
        }
    }
}