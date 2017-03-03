using Common.Domain;
using Common.Model;
using Common.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.QuestionServices.Commands;

namespace Web.API
{
    [Route("api/questions")]
    public class QuestionsController: Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public QuestionsController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<string> CreateCommand([FromBody]QuestionDto question)
        {
            var command = new AddQuestion(question);
            var result = await _commandDispatcher.DispatchAsync(command) as IdentityResult;
            return result.Identity;
        }
    }
}