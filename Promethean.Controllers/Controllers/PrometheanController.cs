using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
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

		private string _getClaimValue(string claimType) => User.Claims.FirstOrDefault(claim => claim.Type.Equals(claimType)).Value;
	}
}