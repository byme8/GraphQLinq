using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQLinq.Tests.Tools;
using NUnit.Framework;

namespace GraphQLinq.Tests
{
    public class TestWithTestServer
    {
        public const string TEST_SERVER_URL = "http://localhost:10000/graphql";

        public TestWithTestServer()
        {
            Context = new TestServer.QueryContext(HttpClientHelper.Create(TEST_SERVER_URL));
        }

        public TestServer.QueryContext Context
        {
            get;
        }

        [OneTimeSetUp]
        public async Task RunSever()
        {
            var server = TestServer.Program.StartServer(Array.Empty<string>());
            var serverIsRunning = await TestServer.Program.VerifyServerIsRunning();

            if (serverIsRunning)
            {
                return;
            }

            Assert.Fail("Failed to run test graphql server.");
        }

        [OneTimeTearDown]
        public void StopServer()
        {
            TestServer.Program.StopServer();
        }

    }
}