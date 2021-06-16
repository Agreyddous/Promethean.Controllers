using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Promethean.CommandHandlers.DependencyInjection;
using Promethean.CommandHandlers.Handlers.Contracts;
using Promethean.Controllers.Tests.Commands;
using Promethean.Controllers.Tests.Commands.Results;
using Promethean.Logs.DependencyInjection;
using Promethean.Logs.Services.Contracts;

namespace Promethean.Controllers.Tests.Controllers
{
	[TestClass]
	public class PrometheanControllerTests
	{
		private readonly ServiceProvider _serviceProvider;

		private IServiceScope _serviceScope;

		public PrometheanControllerTests()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddLogService();
			services.AddLogging(configure => configure.AddConsole());
			services.AddCommandHandlers();
			services.AddScoped<TestController>(services => new TestController(services.GetService<IHandler>(), services.GetService<ILogService>()));

			_serviceProvider = services.BuildServiceProvider();
		}

		[TestInitialize]
		public void Setup() => _serviceScope = _serviceProvider.CreateScope();

		[TestMethod("Handle a command using the Handle method from the controller")]
		public async Task HandleCommand()
		{
			TestController controller = _getService<TestController>();

			TestCommandResult result = await controller.HandleCommand(new TestCommand());

			Assert.IsNotNull(result);
			Assert.AreEqual(HttpStatusCode.OK, result.Code);
		}

		private TService _getService<TService>() => _serviceScope.ServiceProvider.GetService<TService>();
	}
}