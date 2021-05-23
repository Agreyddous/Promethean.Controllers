using System.Net;
using Promethean.CommandHandlers.Handlers;
using Promethean.Controllers.Tests.Commands;
using Promethean.Controllers.Tests.Commands.Results;
using Promethean.Notifications;

namespace Promethean.Controllers.Tests.Handlers
{
	public class TestCommandHandler : Notifiable, ICommandHandler<TestCommand, TestCommandResult>
	{
		public TestCommandResult Handle(TestCommand command) => new TestCommandResult(HttpStatusCode.OK);
	}
}