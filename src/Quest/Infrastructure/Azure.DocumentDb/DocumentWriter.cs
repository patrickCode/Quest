using System;
using Common.Model;
using Common.Interfaces;
using Common.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using System.Collections.Generic;
using Microsoft.Azure.Documents.Client;

namespace Azure.DocumentDb
{
    public class DocumentWriter<T> : IDocumentWriter<T> where T : DocumentEntity
    {
        private readonly DocumentDbConfiguration _docDbConfiguration;
        private readonly DocumentClient _documentClient;
        private readonly IDocumentReader<T> _documentReader;
        public DocumentWriter(DocumentDbConfiguration docDbConfiguration, IDocumentReader<T> documentReader)
        {
            _docDbConfiguration = docDbConfiguration;
            _documentClient = new DocumentClient(new Uri(_docDbConfiguration.Endpoint), _docDbConfiguration.PrimaryKey);
            _documentReader = documentReader;
            Init().Wait();
        }

        private async Task Init()
        {
            var database = (Database)(await _documentClient.CreateDatabaseIfNotExistsAsync(new Database()
            {
                Id = _docDbConfiguration.Database
            }));

            await _documentClient.CreateDocumentCollectionIfNotExistsAsync(
                database.SelfLink,
                new DocumentCollection()
                {
                    Id = _docDbConfiguration.QuestionCollection
                });
        }

        public void CreateOrUpdate(T document)
        {
            CreateOrUpdateAsync(document).Wait();
        }

        public async Task CreateOrUpdateAsync(T document)
        {
            await _documentClient.UpsertDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(
                    _docDbConfiguration.Database,
                    _docDbConfiguration.QuestionCollection),
                document);
        }
        
        public void Create(List<T> document)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(List<T> document)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            DeleteAsync(id).Wait();
        }

        public async Task DeleteAsync(string id)
        {
            await _documentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(
                _docDbConfiguration.Database, 
                _docDbConfiguration.QuestionCollection, 
                id));
        }
    }
}