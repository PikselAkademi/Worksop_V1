using Microsoft.EntityFrameworkCore;
using V1.Web.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var provider = builder.Configuration["Database:Provider"] ?? "InMemory";
if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection tanımlı değil.");

    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("V1ContentDb"));
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
    {
        await dbContext.Database.MigrateAsync();
    }
    else
    {
        await dbContext.Database.EnsureCreatedAsync();
    }

    await SeedData.EnsureSeedDataAsync(dbContext);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Pages}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

public partial class Program { }

