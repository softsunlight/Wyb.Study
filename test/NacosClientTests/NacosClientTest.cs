using System.Net;
using System.Net.Http.Json;

namespace NacosClientTests
{
    public class NacosClientTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task RegisterServiceTest()
        {
            HttpClient httpClient = new HttpClient();

            HttpContent jsonContent = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"namespaceId","" },
                {"serviceName","service1" },
                { "ip","127.0.0.1"},
                {"port","9100" }
            });
            var response = await httpClient.PostAsync("http://localhost:8848/nacos/v1/ns/instance", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            Assert.That(response.IsSuccessStatusCode, Is.EqualTo(true));
        }

        [Test]
        public async Task ServiceHealthyTest()
        {
            HttpClient httpClient = new HttpClient();

            HttpContent jsonContent = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"namespaceId","" },
                {"serviceName","service1" },
                { "ip","127.0.0.1"},
                {"port","12345" },
                {"healthy","true" }
            });
            var response = await httpClient.PutAsync("http://localhost:8848/nacos/v1/ns/health/instance", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            Assert.That(response.IsSuccessStatusCode, Is.EqualTo(true));
        }
    }
}