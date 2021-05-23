using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Promethean.CommandHandlers.Commands;
using Promethean.CommandHandlers.Commands.Results;
using Promethean.CommandHandlers.Handlers;
using Promethean.Logs.Services;

namespace Promethean.Controllers
{
	[ApiController]
	public abstract class PrometheanController : ControllerBase
	{
		protected readonly IHandler Handler;
		protected readonly ILogService LogService;

		public PrometheanController(IHandler handler, ILogService logService)
		{
			Handler = handler;
			LogService = logService;
		}

		protected string UserRole => _getClaimValue(ClaimTypes.Role);
		protected string UserName => _getClaimValue(ClaimTypes.Name);
		protected string UserEmail => _getClaimValue(ClaimTypes.Email);

		protected string RetrieveCustomClaim(string customClaimType) => _getClaimValue(customClaimType);

		protected async Task<TCommandResult> Handle<TCommand, TCommandResult>(TCommand command)
			where TCommand : ICommand
			where TCommandResult : ICommandResult, new()
		{
			string method = $"{HttpContext?.Request.Method} -> {HttpContext?.Request.Path}";

			LogService.Log<PrometheanController>("Input", method, new { Input = command, IP = HttpContext?.Connection.RemoteIpAddress.ToString() }, LogLevel.Debug);

			TCommandResult result = await Handler.Handle<TCommand, TCommandResult>(command);

			LogService.Log<PrometheanController>("Output", method, new { Code = result.Code, Output = result }, LogLevel.Debug);

			if (HttpContext != null)
				CreateResponse(result);

			return result;
		}

		protected TCommandResult CreateResponse<TCommandResult>(TCommandResult result) where TCommandResult : ICommandResult => CreateResponse(result.Code, result);

		protected TResult CreateResponse<TResult>(HttpStatusCode code, TResult result)
		{
			Response.StatusCode = (int)code;

			return result;
		}

		private string _getClaimValue(string claimType) => User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(claimType))?.Value;
	}
}