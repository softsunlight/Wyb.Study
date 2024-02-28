

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shop.Service;

namespace Wyb.Study.TestBase
{
    public class BaseUnitTest
    {
        protected IConfiguration _configuration;

        protected IServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();

            ServiceCollection services = new ServiceCollection();

            services.AddSingleton(_configuration);

            services.AddServicesAndDaos();

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(_configuration);
            });

            _serviceProvider = services.BuildServiceProvider();
        }
    }
}