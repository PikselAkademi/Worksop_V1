using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using V1.Web.Data;
using V1.Web.Models;

namespace V1.Web.Controllers;

public class HomeController(AppDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = await LoadPageAsync("home");
        return View(model);
    }

    public async Task<IActionResult> About()
    {
        var model = await LoadPageAsync("about");
        return View(model);
    }

    public async Task<IActionResult> Contact()
    {
        var model = await LoadPageAsync("contact");
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task<ContentPageViewModel> LoadPageAsync(string slug)
    {
        var page = await dbContext.PageContents
            .AsNoTracking()
            .SingleAsync(x => x.Slug == slug);

        return new ContentPageViewModel
        {
            Slug = page.Slug,
            Title = page.Title,
            Body = page.Body,
            UpdatedAtUtc = page.UpdatedAtUtc
        };
    }
}
