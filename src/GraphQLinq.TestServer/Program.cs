using System.Net;
namespace TestServer;

public class Program
{
    public const string TEST_SERVER_URL = "http://localhost:10000/graphql";

    private static readonly CancellationTokenSource cancellationTokenSource = new();

    public static async Task Main(string[] args)
    {
        await StartServer(args);
    }

    public static void StopServer()
    {
        cancellationTokenSource.Cancel();
    }

    public static async Task StartServer(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel(o => o.ListenAnyIP(10_000));
        builder.WebHost.ConfigureLogging(o => o.ClearProviders());

        builder.Services.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddTypeExtension<UserGraphQLExtensions>();

        var app = builder.Build();

        app.MapGraphQL();

        await app.RunAsync(cancellationTokenSource.Token);
    }

    public static async Task<bool> VerifyServerIsRunning()
    {
        var httpClient = new HttpClient();
        for (int i = 0; i < 5; i++)
        {
            try
            {
                var response = await httpClient.GetAsync(TEST_SERVER_URL + "?sdl");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

                await Task.Delay(500);
            }
            catch
            {
            }
        }

        return false;
    }
}

public class Query
{

}

[ExtendObjectType(typeof(Query))]
public class UserGraphQLExtensions
{
    public User GetUser(int id)
    {
        return new User
        {
            FirstName = "Jon",
            LastName = "Smith"
        };
    }

    public User GetFailUser()
    {
        throw new GraphQLException(new Error("Property fails", "FAIL_PROPERTY"));
    }
}

public class User
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}