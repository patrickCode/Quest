using System;
using System.Linq;
using Common.Model;
using Common.Interfaces;
using Common.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Azure.DocumentDb.Tests.FunctionalTest
{
    [TestClass]
    public class DocumentDbTests
    {
        private readonly DocumentDbConfiguration _configuration;
        private readonly IDocumentReader<Question> _reader;
        private readonly IDocumentWriter<Question> _writer;
        private readonly string _sampleDocumentId;
        public DocumentDbTests()
        {
            //Init
            _configuration = new DocumentDbConfiguration()
            {
                Endpoint = "https://doc-quest.documents.azure.com:443/",
                PrimaryKey = "7Y9l4npDQyiTD2eQsRwZZgy8RwV4LXIMofCE0wDswegPf7IH93iVlTg815KRVlSL0TlGm5T3SzMBmP5Pkuan0w==",
                Database = "quest",
                Collection = "questions"
            };

            _reader = new DocumentReader<Question>(_configuration);
            _writer = new DocumentWriter<Question>(_configuration, _reader);

            _sampleDocumentId = "Sample_Question_Id";
            CreateSampleDocument();
        }

        [TestMethod]
        public async Task ReadDocumentById()
        {
            //Init
            var id = _sampleDocumentId;

            //Action
            var document = await _reader.GetAsync(id);

            //Assert
            Assert.IsNotNull(document);
            Assert.AreEqual(id, document.Id);
        }

        [TestMethod]
        public async Task QueryDocument()
        {
            //Init
            var id = _sampleDocumentId;

            //Action
            var documents = await _reader.QueryAsync(q => q.Categories.Any(c => c.Code == "NG"));

            //Assert
            Assert.IsNotNull(documents);
            Assert.IsTrue(documents.Any());
        }

        [TestMethod]
        public async Task QueryDocumentBySqlQuery()
        {
            //Init
            var id = _sampleDocumentId;

            //Action
            var documents = await _reader.QueryAsync("SELECT c.id, c['Value'] FROM c JOIN cat in c.Categories WHERE cat.Code = \"NG\"");

            //Assert
            Assert.IsNotNull(documents);
            Assert.IsTrue(documents.Any());
        }

        [TestMethod]
        public async Task AddAndRetrieveDocument()
        {
            //Init
            var id = Guid.NewGuid().ToString();
            var question = GetSampleQuestion(id);

            //Action
            await _writer.CreateAsync(question);
            var document = await _reader.GetAsync(id);

            //Assert
            Assert.IsNotNull(document);
            Assert.AreEqual(question, document);
        }

        [TestMethod]
        public async Task UpdateAnExistingDocument()
        {
            //Init
            var id = _sampleDocumentId;
            var existingDocument = _reader.Get(id);

            //Verify
            Assert.IsNotNull(existingDocument);
            var newCorrectAnswer = "Sample_Answer_" + Guid.NewGuid().ToString();
            existingDocument.CorrectAnswer = newCorrectAnswer;

            //Action
            await _writer.CreateAsync(existingDocument);
            var document = await _reader.GetAsync(id);

            //Assert
            Assert.IsNotNull(document);
            Assert.AreEqual(newCorrectAnswer, document.CorrectAnswer);
        }

        [TestMethod]
        public async Task DeleteDocument()
        {
            //Init
            var id = Guid.NewGuid().ToString();
            var question = GetSampleQuestion(id);

            //Action
            await _writer.CreateAsync(question);
            var document = await _reader.GetAsync(id);

            //Pre-Verification
            Assert.IsNotNull(document);
            Assert.AreEqual(question, document);

            //Action
            await _writer.DeleteAsync(id);
            document = await _reader.GetAsync(id);

            //Assert
            Assert.IsNull(document);
        }

        private void CreateSampleDocument()
        {
            var question = GetSampleQuestion(_sampleDocumentId);
            _writer.Create(question);
        }

        private Question GetSampleQuestion(string id)
        {
            return new Question()
            {
                Id = id,
                Value = "Sample Question?",
                AnswerTypeCode = "SUB",
                QuestionTypeCode = "TXT",
                CorrectAnswer = "Sample Answer",
                Options = null,
                Categories = new List<Category>()
                {
                    new Category()
                    {
                        Value = "Angular.JS 1.0",
                        Code = "NG",
                        SubCatgories = new List<SubCategory>()
                        {
                            new SubCategory()
                            {
                                Value = "Angular Services",
                                Code = "NG-SVC"
                            },
                            new SubCategory()
                            {
                                Value = "Angular Providers",
                                Code = "NG-PROV"
                            }
                        }
                    }
                },
                Tags = "Angular,Angular-1.x,Angular-Services,Angular-Providers",
                DifficultLevel = 200,
                CreatedBy = "pratikb@microsoft.com",
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy = "pratikb@microsoft.com",
                LastModifedOn = DateTime.UtcNow
            };
        }
    }
}