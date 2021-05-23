using System.Collections.Generic;
using System.Net;
using Promethean.CommandHandlers.Commands.Results;
using Promethean.Notifications;

namespace Promethean.Controllers.Tests.Commands.Results
{
	public class TestCommandResult : CommandResult
	{
		public TestCommandResult() { }
		public TestCommandResult(HttpStatusCode code, IReadOnlyCollection<Notification> notifications = null) : base(code, notifications) { }
	}
}