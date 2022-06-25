using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using TestServer;
using TestServer.StrawberryShake.Client;
using User = TestServer.User;

var server = TestServer.Api.Program.StartServer(Array.Empty<string>());
await TestServer.Api.Program.VerifyServerIsRunning();

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
    private readonly EndlessHttpClient httpClient;

    public StrawberryShakeVSGraphQLinq()
    {
        httpClient = new EndlessHttpClient();
        httpClient.BaseAddress = new Uri(TestServer.Api.Program.TEST_SERVER_URL);
        context = new TestServer.QueryContext(httpClient);
        client = TestServerClientCreator.Create(httpClient);
    }
    
    [Benchmark]
    public async Task RawRequest()
    {
        var json = @"{""query"":""query ($id: Int!) { user (id: $id) { \n  firstName\n  lastName\n }}"",""variables"":{""id"":42}}";
        var response = await httpClient.PostAsync("", new StringContent(json, Encoding.UTF8, "application/json"));
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<GraphQLResponse<GetUserResponse>>(jsonResponse);
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

public class GraphQLResponse<T>
{
    public T Data { get; set; }
}

public class GetUserResponse
{
    public User User { get; set; }
}