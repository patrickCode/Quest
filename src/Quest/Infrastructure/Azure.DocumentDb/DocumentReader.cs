﻿using System;
using System.Net;
using System.Linq;
using Common.Model;
using Common.Interfaces;
using Common.Configuration;
using System.Threading.Tasks;
using Azure.DocumentDb.Utility;
using Microsoft.Azure.Documents;
using System.Collections.Generic;
using Microsoft.Azure.Documents.Client;

namespace Azure.DocumentDb
{
    public class DocumentReader<T> : IDocumentReader<T> where T : DocumentEntity
    {
        private readonly DocumentDbConfiguration _docDbConfiguration;
        private readonly DocumentClient _documentClient;
        private readonly ICollectionNameResolver _collectionNameResolver;
        private readonly string _collectionName;
        public DocumentReader(DocumentDbConfiguration docDbConfiguration, ICollectionNameResolver collectionNameResolver)
        {
            _docDbConfiguration = docDbConfiguration;
            _documentClient = new DocumentClient(new Uri(_docDbConfiguration.Endpoint), _docDbConfiguration.PrimaryKey);
            _collectionNameResolver = collectionNameResolver;
            _collectionName = _collectionNameResolver.Resolve(typeof(T));
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
                    Id = _collectionName
                });
        }
        public bool Exists(string id)
        {
            return ExistsAsync(id).Result;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return (await GetAsync(id)) != null;
        }

        public T Get(string id)
        {
            return GetAsync(id).Result;
        }

        public async Task<T> GetAsync(string id)
        {
            try
            {
                var document = await _documentClient.ReadDocumentAsync(
                                UriFactory.CreateDocumentUri(
                                    _docDbConfiguration.Database,
                                    _collectionName,
                                    id));
                var doc = (Document)document;
                return (T)(dynamic)doc;
            }
            catch (DocumentClientException error)
            {
                if (error.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }

        public List<T> Get()
        {
            var options = new FeedOptions()
            {
                MaxItemCount = -1
            };

            var documentQuery = _documentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_docDbConfiguration.Database, _collectionName),
                options)
                .ToList();

            return documentQuery;
        }

        public async Task<List<T>> GetAsync()
        {
            return await Task.Run(() =>
            {
                return Get();
            });
        }

        public List<T> Query(string query)
        {
            var options = new FeedOptions()
            {
                MaxItemCount = -1
            };

            var documentQuery = _documentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_docDbConfiguration.Database, _collectionName),
                query,
                options)
                .ToList();

            return documentQuery;
        }

        public async Task<List<T>> QueryAsync(string query)
        {
            return await Task.Run(() =>
            {
                return Query(query);
            });
        }

        public List<T> Query(Func<T, bool> query)
        {
            var options = new FeedOptions()
            {
                MaxItemCount = -1
            };

            var documentQuery = _documentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_docDbConfiguration.Database, _collectionName),
                options)
                .Where(query)
                .ToList();

            return documentQuery;
        }

        public async Task<List<T>> QueryAsync(Func<T, bool> query)
        {
            return await Task.Run(() =>
            {
                return Query(query);
            });
        }
    }
}