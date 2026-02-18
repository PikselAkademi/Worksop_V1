using Microsoft.EntityFrameworkCore;
using V1.Web.Models;

namespace V1.Web.Data;

public static class SeedData
{
    public static async Task EnsureSeedDataAsync(AppDbContext dbContext)
    {
        if (await dbContext.PageContents.AnyAsync())
        {
            return;
        }

        var now = DateTime.UtcNow;

        dbContext.PageContents.AddRange(
            new PageContent
            {
                Slug = "home",
                Title = "Ana Sayfa",
                Body = "CI/CD eðitim demosu için hazýrlanan örnek kurumsal web sitesine hoþ geldiniz.",
                UpdatedBy = "System",
                UpdatedAtUtc = now
            },
            new PageContent
            {
                Slug = "about",
                Title = "Hakkýmýzda",
                Body = "Bu sayfa içerikleri yönetim panelinden anlýk olarak düzenlenebilir.",
                UpdatedBy = "System",
                UpdatedAtUtc = now
            },
            new PageContent
            {
                Slug = "contact",
                Title = "Ýletiþim",
                Body = "Bize ornek@firma.com adresinden ulaþabilirsiniz.",
                UpdatedBy = "System",
                UpdatedAtUtc = now
            });

        await dbContext.SaveChangesAsync();
    }
}
