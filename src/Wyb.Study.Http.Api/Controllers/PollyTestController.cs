using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.CircuitBreaker;
using Polly.RateLimiting;
using Polly.Timeout;
using System.Threading.RateLimiting;

namespace Wyb.Study.Http.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PollyTestController : ControllerBase
    {
        private readonly ILogger<PollyTestController> _logger;

        public PollyTestController(ILogger<PollyTestController> logger)
        {
            _logger = logger;
        }

        #region 超时
        [HttpGet]
        public async Task Timeout1()
        {
            var pipeline = new ResiliencePipelineBuilder()
            .AddTimeout(TimeSpan.FromSeconds(3))
            .Build();

            HttpResponseMessage httpResponse = await pipeline.ExecuteAsync(
            async ct =>
            {
                // Execute a delegate that takes a CancellationToken as an input parameter.
                return await new HttpClient().GetAsync("http://localhost:5166/PollyTest/Test", ct);
            },
            CancellationToken.None);
        }

        [HttpGet]
        public async Task Timeout2()
        {
            var pipeline = new ResiliencePipelineBuilder()
            .AddTimeout(new TimeoutStrategyOptions
            {
                Timeout = TimeSpan.FromSeconds(2),
                OnTimeout = args =>
                {
                    Console.WriteLine("Timeout limit has been exceeded");
                    return default;
                }
            })
            .Build();

            //try
            //{
            HttpResponseMessage httpResponse = await pipeline.ExecuteAsync(
            async ct =>
            {
                // Execute a delegate that takes a CancellationToken as an input parameter.
                return await new HttpClient().GetAsync("http://localhost:5166/PollyTest/Test", ct);
            },
            CancellationToken.None);
            //}
            //catch (TimeoutRejectedException)
            //{
            //    Console.WriteLine("Timeout limit has been exceeded");
            //}
        }

        #endregion

        #region 重试
        [HttpGet]
        public async Task<string> Retry1()
        {
            var pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new Polly.Retry.RetryStrategyOptions()//默认除了()，其他所有异常都会重试
            {
                OnRetry = static args =>
                {
                    Console.WriteLine("OnRetry, Attempt: {0}", args.AttemptNumber);

                    // Event handlers can be asynchronous; here, we return an empty ValueTask.
                    return default;
                }
            })//默认重试策略
            .Build();

            HttpResponseMessage httpResponse = await pipeline.ExecuteAsync(
            async ct =>
            {
                //throw new Exception("error");
                // Execute a delegate that takes a CancellationToken as an input parameter.
                return await new HttpClient().GetAsync("http://localhost:5166/PollyTest/Test", ct);
            },
            CancellationToken.None);
            return await httpResponse.Content.ReadAsStringAsync();
        }
        #endregion

        #region 限流
        [HttpGet]
        public async Task<string> RateLimiter1()
        {
            //var pipeline = new ResiliencePipelineBuilder()
            ////.AddRateLimiter(new RateLimiterStrategyOptions())//默认重试策略
            //.AddConcurrencyLimiter(1)
            ////.AddRateLimiter(new SlidingWindowRateLimiter(
            ////new SlidingWindowRateLimiterOptions
            ////{
            ////    PermitLimit = 100,
            ////    Window = TimeSpan.FromMinutes(1)
            ////}))
            //.Build();
            try
            {
                var result = await Test1.Pipeline.ExecuteAsync(token => TextSearchAsync(token), CancellationToken.None);
                return "ok";
            }
            catch (RateLimiterRejectedException ex)
            {
                // Handle RateLimiterRejectedException,
                // that can optionally contain information about when to retry.
                if (ex.RetryAfter is TimeSpan retryAfter)
                {
                    //Console.WriteLine($"Retry After: {retryAfter}");
                    return $"Retry After: {retryAfter}";
                }
                return ex.Message;
            }
        }

        private async ValueTask<string> TextSearchAsync(CancellationToken token)
        {
            await Task.Delay(5 * 1000);
            //var task = await new HttpClient().GetAsync("http://localhost:5166/PollyTest/Test", ct);
            return "";
        }

        static class Test1
        {
            public static ResiliencePipeline Pipeline;
            static Test1()
            {
                Pipeline = new ResiliencePipelineBuilder()
            //.AddRateLimiter(new RateLimiterStrategyOptions())//默认重试策略
            .AddConcurrencyLimiter(1)
            //.AddRateLimiter(new SlidingWindowRateLimiter(
            //new SlidingWindowRateLimiterOptions
            //{
            //    PermitLimit = 100,
            //    Window = TimeSpan.FromMinutes(1)
            //}))
            .Build();
            }
        }
        #endregion

        #region 熔断
        [HttpGet]
        public async Task<string> CircuitBreaker1()
        {
            var result = await CircuitBreakerData.Pipeline.ExecuteAsync(token => TextSearchAsync2(token), CancellationToken.None);
            return "ok";
        }

        private async ValueTask<string> TextSearchAsync2(CancellationToken token)
        {
            //await Task.Delay(1000);
            if (DateTime.Now.Ticks % 2 == 0)
            {
                throw new Exception();
            }
            return "";
        }

        static class CircuitBreakerData
        {
            public static ResiliencePipeline Pipeline;
            static CircuitBreakerData()
            {
                Pipeline = new ResiliencePipelineBuilder()
            //.AddRateLimiter(new RateLimiterStrategyOptions())//默认重试策略
            .AddCircuitBreaker(new CircuitBreakerStrategyOptions
            {
                FailureRatio = 0.5,
                SamplingDuration = TimeSpan.FromSeconds(5),
                MinimumThroughput = 8,
                BreakDuration = TimeSpan.FromSeconds(2),
                ShouldHandle = new PredicateBuilder().Handle<Exception>()
            })
            //.AddRateLimiter(new SlidingWindowRateLimiter(
            //new SlidingWindowRateLimiterOptions
            //{
            //    PermitLimit = 100,
            //    Window = TimeSpan.FromMinutes(1)
            //}))
            .Build();
            }
        }
        #endregion

        [HttpGet]
        public async Task Test()
        {
            await Task.Delay(5000);
        }

    }
}
