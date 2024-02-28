using CSRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlSugar;
using Wyb.Study.RedPacket.Dao;
using Wyb.Study.RedPacket.IDao;
using Wyb.Study.RedPacket.IServices;
using Wyb.Study.RedPacket.Services;

namespace Wyb.Study.RedPacket.Test
{
    public class RedPacketServiceTest
    {

        private IConfiguration _configuration;

        private IServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();

            ServiceCollection services = new ServiceCollection();

            services.AddSingleton(_configuration);

            services.AddTransient<IRedPacketDao, RedPacketDao>();
            services.AddTransient<IRedWarsLogDao, RedWarsLogDao>();
            services.AddTransient<IRedPacketService, RedPacketService>();

            services.AddTransient<ISqlSugarClient>(s =>
            {
                //ScopedÓÃSqlSugarClient 
                SqlSugarClient sqlSugar = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.MySql,
                    ConnectionString = _configuration.GetSection("ConnectionStrings:Db").Value,
                    IsAutoCloseConnection = true,
                },
               db =>
               {
                   db.Aop.OnLogExecuting = (sql, pars) =>
                   {

                   };
               });
                return sqlSugar;
            });

            services.AddSingleton<CSRedisClient>(new CSRedisClient(_configuration.GetSection("ConnectionStrings:LocalRedis").Value));

            services.AddLogging();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void RedisLua_Test()
        {
            var redisClient = _serviceProvider.GetRequiredService<CSRedisClient>();
            //var result = redisClient.Eval("return redis.call('get',ARGV[1])", "", "name");
            //var result = redisClient.Eval("return redis.call('set',ARGV[1],ARGV[2])", "", "name", "v2");
            //var result = redisClient.Eval("return 'Hello World'", "");
            //var result = redisClient.Eval("if redis.call('get',ARGV[2])==ARGV[1] then return redis.call('del',ARGV[2]) else return 0 end", "", "1", "name");
        }

        [Test]
        public void AddRedPacket_Test()
        {
            var redPacketService = _serviceProvider.GetRequiredService<IRedPacketService>();
            var result = redPacketService.AddAsync(100, 10).Result;
        }

        [Test]
        public void GrabRedPacket_Test()
        {
            var redPacketService = _serviceProvider.GetRequiredService<IRedPacketService>();
            var result = redPacketService.GrabRedPacketAsync(1, Guid.NewGuid().ToString()).Result;
        }

        [Test]
        public void GrabRedPacket_Batch_Test()
        {
            var redPacketService = _serviceProvider.GetRequiredService<IRedPacketService>();
            var redPacket = redPacketService.AddAsync(100, 10).Result;
            int taskLength = 20;
            int userCount = 5;
            Task[] taskArr = new Task[taskLength * userCount];
            for (int i = 0; i < taskLength; i++)
            {
                string userId = Guid.NewGuid().ToString();
                for (int j = 0; j < userCount; j++)
                {
                    var task = Task.Run(() =>
                    {
                        var result = redPacketService.GrabRedPacketAsync(redPacket.Id, userId).Result;
                    });
                    taskArr[i * userCount + j] = task;
                    //taskArr[i] = task;
                }
            }
            Task.WaitAll(taskArr);
        }

        [Test]
        public void GrabRedPacket_Batch_Test2()
        {
            int redPacketCount = 200;
            Task[] redPacketTasks = new Task[redPacketCount];
            for (int x = 0; x < redPacketCount; x++)
            {
                var redPacketService = _serviceProvider.GetRequiredService<IRedPacketService>();
                var tempTask = Task.Run(() =>
                {
                    var redPacket = redPacketService.AddAsync(100, 10).Result;
                    int taskLength = 20;
                    int userCount = 5;
                    Task[] taskArr = new Task[taskLength * userCount];
                    for (int i = 0; i < taskLength; i++)
                    {
                        string userId = Guid.NewGuid().ToString();
                        for (int j = 0; j < userCount; j++)
                        {
                            var task = Task.Run(() =>
                            {
                                var result = redPacketService.GrabRedPacketAsync(redPacket.Id, userId).Result;
                            });
                            taskArr[i * userCount + j] = task;
                            //taskArr[i] = task;
                        }
                    }
                    Task.WaitAll(taskArr);
                });
                redPacketTasks[x] = tempTask;
            }
            Task.WaitAll(redPacketTasks);
        }
    }
}