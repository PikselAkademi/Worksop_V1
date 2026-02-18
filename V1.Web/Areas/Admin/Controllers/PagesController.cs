using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using V1.Web.Areas.Admin.Models;
using V1.Web.Data;

namespace V1.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class PagesController(AppDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var pages = await dbContext.PageContents
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync();

        return View(pages);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var page = await dbContext.PageContents.SingleOrDefaultAsync(x => x.Id == id);
        if (page is null)
        {
            return NotFound();
        }

        var model = new PageContentEditViewModel
        {
            Id = page.Id,
            Slug = page.Slug,
            Title = page.Title,
            Body = page.Body,
            UpdatedBy = page.UpdatedBy
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PageContentEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var page = await dbContext.PageContents.SingleOrDefaultAsync(x => x.Id == model.Id);
        if (page is null)
        {
            return NotFound();
        }

        page.Title = model.Title;
        page.Body = model.Body;
        page.UpdatedBy = string.IsNullOrWhiteSpace(model.UpdatedBy) ? "Admin" : model.UpdatedBy;
        page.UpdatedAtUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
        TempData["Message"] = "Sayfa güncellendi.";

        return RedirectToAction(nameof(Index));
    }
}
