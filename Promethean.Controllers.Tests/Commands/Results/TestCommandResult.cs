using System.Collections.Generic;
using System.Net;
using Promethean.CommandHandlers.Commands.Results;
using Promethean.Notifications.Messages.Contracts;

namespace Promethean.Controllers.Tests.Commands.Results
{
	public class TestCommandResult : CommandResult
	{
		public TestCommandResult() { }
		public TestCommandResult(HttpStatusCode code, IReadOnlyDictionary<string, IReadOnlyCollection<INotificationMessage>> notifications = null) : base(code, notifications) { }
	}
}