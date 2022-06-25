using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TestServer.StrawberryShake.Client
{
    public static class TestServerClientCreator
    {
        public static TestServerQLClient Create(Action<HttpClient> configure)
        {
            var services = new ServiceCollection();
            services
                .AddTestServerQLClient()
                .ConfigureHttpClient(configure);

            var provider = services.BuildServiceProvider();
            var client = provider.GetService<TestServerQLClient>()!;

            return client;
        }

        public static TestServerQLClient Create(HttpClient httpClient)
        {
            var services = new ServiceCollection();
            services.AddTestServerQLClient();
            services.AddSingleton<IHttpClientFactory>(new FakeFactory(httpClient));

            var provider = services.BuildServiceProvider();
            var client = provider.GetService<TestServerQLClient>()!;

            return client;
        }

        private class FakeFactory : IHttpClientFactory
        {
            private readonly HttpClient client;

            public FakeFactory(HttpClient client)
            {
                this.client = client;
            }

            public HttpClient CreateClient(string name)
            {
                return client;
            }
        }
    }
}