using System.Net;
using Common.Model;
using Common.Domain;
using System.Net.Http;
using Common.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Common.Domain.CommandResults;
using Services.QuestionServices.Commands;
using Services.QuestionServices.QueryServices;

namespace Web.API
{
    [Route("api/questions")]
    public class QuestionsController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQuestionsQueryService _questionsQueryService;
        public QuestionsController(ICommandDispatcher commandDispatcher, IQuestionsQueryService questionsQueryService)
        {
            _commandDispatcher = commandDispatcher;
            _questionsQueryService = questionsQueryService;
        }

        [HttpGet]
        public async Task<IList<QuestionDto>> Get(bool publicVisibility = true)
        {
            if (publicVisibility)
                return await _questionsQueryService.GetPublicQuestionsAsync();
            return await _questionsQueryService.GetAsync();
        }

        [HttpGet]
        [Route("{identifier}")]
        public async Task<QuestionDto> Get(string identifier, string type = "identifier")
        {
            if (type.Equals("question"))
                return await _questionsQueryService.GetQuestionByValueAsync(identifier);
            return await _questionsQueryService.GetAsync(identifier);
        }

        [HttpGet]
        [Route("/api/categories/{category}/questions")]
        public async Task<IList<QuestionDto>> GetByCategory(string category)
        {
            return await _questionsQueryService.GetByCategoryAsync(category);
        }

        [HttpGet]
        [Route("/api/users/{upn}/questions")]
        public async Task<IList<QuestionDto>> GetByUser(string upn)
        {
            return await _questionsQueryService.GetByUserAsync(upn);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateQuestion([FromBody]QuestionDto question)
        {
            var command = new UpdateQuestion(question);
            var result = await _commandDispatcher.DispatchAsync(command) as IdentityResult;
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result.Identity)
            };
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateQuestion([FromBody]QuestionDto question)
        {
            var command = new AddQuestion(question);
            var result = await _commandDispatcher.DispatchAsync(command) as IdentityResult;
            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(result.Identity)
            };
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> DeleteQuestion(string id)
        {
            var command = new DeleteQuestion(id);
            var result = await _commandDispatcher.DispatchAsync(command) as VoidResult;
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}