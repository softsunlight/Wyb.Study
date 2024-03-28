using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.IServices;
using Wyb.Study.Application.Contracts.Responses;

namespace Wyb.Study.Application.Services
{
    public class KafkaDemoService : IKafkaDemoService
    {
        private readonly ILogger<KafkaDemoService> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public KafkaDemoService(ILogger<KafkaDemoService> logger)
        {
            _logger = logger;
        }

        public async Task<DataResponse<dynamic>> SendMessageAsync()
        {
            DataResponse<dynamic> baseResponse = new DataResponse<dynamic>();
            try
            {
                var config = new ProducerConfig { BootstrapServers = "192.168.220.141:9092,192.168.220.135:9092,192.168.220.142:9092" };

                // If serializers are not specified, default serializers from
                // `Confluent.Kafka.Serializers` will be automatically used where
                // available. Note: by default strings are encoded as UTF8.
                using (var p = new ProducerBuilder<Null, string>(config).Build())
                {
                    try
                    {
                        var dr = await p.ProduceAsync("first", new Message<Null, string> { Value = "hello world" });
                        baseResponse.Data = dr;
                    }
                    catch (ProduceException<Null, string> e)
                    {
                        baseResponse.Message = $"Delivery failed: {e.Error.Reason}";
                    }
                }
                baseResponse.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "发送失败");
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }

        public async Task<DataResponse<dynamic>> SendMessageTestDifferentKeyAsync()
        {
            DataResponse<dynamic> baseResponse = new DataResponse<dynamic>();
            try
            {
                var config = new ProducerConfig { BootstrapServers = "192.168.220.141:9092,192.168.220.135:9092,192.168.220.142:9092" };

                // If serializers are not specified, default serializers from
                // `Confluent.Kafka.Serializers` will be automatically used where
                // available. Note: by default strings are encoded as UTF8.
                using (var p = new ProducerBuilder<string, string>(config).Build())
                {
                    List<DeliveryResult<string, string>> list = new List<DeliveryResult<string, string>>();
                    try
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var dr = await p.ProduceAsync("first", new Message<string, string> { Key = "a", Value = $"atguigu{i}" });
                            list.Add(dr);
                        }

                        for (int i = 0; i < 5; i++)
                        {
                            var dr = await p.ProduceAsync("first", new Message<string, string> { Key = "b", Value = $"atguigu{i}" });
                            list.Add(dr);
                        }

                        for (int i = 0; i < 5; i++)
                        {
                            var dr = await p.ProduceAsync("first", new Message<string, string> { Key = "c", Value = $"atguigu{i}" });
                            list.Add(dr);
                        }

                        baseResponse.Data = list.Select(p => new
                        {
                            p.Key,
                            p.Partition
                        });
                    }
                    catch (ProduceException<Null, string> e)
                    {
                        baseResponse.Message = $"Delivery failed: {e.Error.Reason}";
                    }
                }
                baseResponse.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "发送失败");
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }

        /// <summary>
        /// 将消息发送到指定的分区
        /// </summary>
        /// <returns></returns>
        public async Task<DataResponse<dynamic>> SendMessageToAssignPartitionAsync()
        {
            DataResponse<dynamic> baseResponse = new DataResponse<dynamic>();
            try
            {
                var config = new ProducerConfig { BootstrapServers = "192.168.220.141:9092,192.168.220.135:9092,192.168.220.142:9092" };

                // If serializers are not specified, default serializers from
                // `Confluent.Kafka.Serializers` will be automatically used where
                // available. Note: by default strings are encoded as UTF8.
                using (var p = new ProducerBuilder<string, string>(config).Build())
                {
                    List<DeliveryResult<string, string>> list = new List<DeliveryResult<string, string>>();
                    try
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var dr = await p.ProduceAsync(new TopicPartition("first", new Partition(1)), new Message<string, string> { Key = "a", Value = $"atguigu{i}" });
                            list.Add(dr);
                        }

                        baseResponse.Data = list.Select(p => new
                        {
                            p.Key,
                            p.Partition
                        });
                    }
                    catch (ProduceException<Null, string> e)
                    {
                        baseResponse.Message = $"Delivery failed: {e.Error.Reason}";
                    }
                }
                baseResponse.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "发送失败");
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }

        /// <summary>
        /// kafka事务
        /// </summary>
        /// <returns></returns>
        public async Task<DataResponse<dynamic>> TransactionAsync()
        {
            DataResponse<dynamic> baseResponse = new DataResponse<dynamic>();
            try
            {
                var config = new ProducerConfig { BootstrapServers = "192.168.220.141:9092,192.168.220.135:9092,192.168.220.142:9092" };
                //设置事务id
                config.TransactionalId = "transcation_id_0";

                // If serializers are not specified, default serializers from
                // `Confluent.Kafka.Serializers` will be automatically used where
                // available. Note: by default strings are encoded as UTF8.
                using (var p = new ProducerBuilder<string, string>(config).Build())
                {
                    List<DeliveryResult<string, string>> list = new List<DeliveryResult<string, string>>();
                    try
                    {
                        //初始化事务
                        p.InitTransactions(TimeSpan.FromSeconds(10));
                        //开启事务
                        p.BeginTransaction();
                        for (int i = 0; i < 5; i++)
                        {
                            var dr = await p.ProduceAsync(new TopicPartition("first", new Partition(1)), new Message<string, string> { Key = "a", Value = $"atguigu{i}" });
                            list.Add(dr);
                        }
                        //提交事务
                        p.CommitTransaction();
                        baseResponse.Data = list.Select(p => new
                        {
                            p.Key,
                            p.Partition
                        });
                    }
                    catch (ProduceException<Null, string> e)
                    {
                        baseResponse.Message = $"Delivery failed: {e.Error.Reason}";
                        //终止事务
                        p.AbortTransaction();
                    }
                }
                baseResponse.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "发送失败");
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }
    }
}
