using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Promethean.CommandHandlers.Handlers.Contracts;
using Promethean.Controllers.Tests.Commands;
using Promethean.Controllers.Tests.Commands.Results;
using Promethean.Logs.Services.Contracts;

namespace Promethean.Controllers.Tests.Controllers
{
	public class TestController : PrometheanController
	{
		public TestController(IHandler handler, ILogService logService) : base(handler, logService) { }

		[HttpGet("Handle")]
		public async Task<TestCommandResult> HandleCommand(TestCommand command) => await Handle<TestCommand, TestCommandResult>(command);
	}
}