using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using TestServer;
using TestServer.StrawberryShake.Client;

var server = TestServer.Program.StartServer(Array.Empty<string>());
await TestServer.Program.VerifyServerIsRunning();

await new StrawberryShakeVSGraphQLinq().StrawberryShake();

BenchmarkRunner.Run<StrawberryShakeVSGraphQLinq>();

// Workaround for StrawberryShake Client
public class EndlessHttpClient : HttpClient
{
    protected override void Dispose(bool disposing)
    {

    }
}

public class StrawberryShakeVSGraphQLinq
{
    private readonly QueryContext context;
    private readonly TestServerQLClient client;

    public StrawberryShakeVSGraphQLinq()
    {
        var httpClient = new EndlessHttpClient();
        httpClient.BaseAddress = new Uri(TestServer.Program.TEST_SERVER_URL);
        context = new TestServer.QueryContext(httpClient);
        client = TestServerClientCreator.Create(httpClient);
    }

    [Benchmark]
    public async Task StrawberryShake()
    {
        var a = await client.GetUser.ExecuteAsync(42);
    }

    [Benchmark]
    public async Task GraphQLinq()
    {
        await context.User(42).ToItem();
    }
}