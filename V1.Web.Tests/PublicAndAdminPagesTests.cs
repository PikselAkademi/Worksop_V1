using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace V1.Web.Tests;

public class PublicAndAdminPagesTests
{
    [Fact]
    public async Task PublicPages_ReturnSuccess()
    {
        await using var factory = new V1WebApplicationFactory();
        using var client = factory.CreateClient();

        var home = await client.GetAsync("/");
        var about = await client.GetAsync("/Home/About");
        var contact = await client.GetAsync("/Home/Contact");

        home.EnsureSuccessStatusCode();
        about.EnsureSuccessStatusCode();
        contact.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task AdminPagesIndex_ReturnsExpectedContent()
    {
        await using var factory = new V1WebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/Admin/Pages");
        response.EnsureSuccessStatusCode();

        var html = await response.Content.ReadAsStringAsync();
        Assert.Contains("Yönetim Paneli", html);
        Assert.Contains("home", html, StringComparison.OrdinalIgnoreCase);
    }

    private sealed class V1WebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureAppConfiguration((_, configBuilder) =>
            {
                configBuilder.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Database:Provider"] = "InMemory"
                });
            });
        }
    }
}
