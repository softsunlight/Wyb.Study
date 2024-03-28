

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

            //services.AddServicesAndDaos();

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(_configuration);
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void Test1()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<JsParamBuilder, WechatLoginJsParamBuilder>();
            services.AddSingleton<JsParamBuilder, WechatLogoutJsParamBuilder>();

            var provider = services.BuildServiceProvider();
        }

        class TaskTypeConstant
        {
            public const string WechatLogin = "wechat_login";

            public const string WechatLogout = "wechat_logout";
        }

        abstract class JsParamBuilder
        {
            public abstract object Build();
        }

        class WechatLoginJsParamBuilder : JsParamBuilder
        {
            public override object Build()
            {
                return "wechat_login_param";
            }
        }

        class WechatLogoutJsParamBuilder : JsParamBuilder
        {
            public override object Build()
            {
                return "wechat_logout_param";
            }
        }
    }
}