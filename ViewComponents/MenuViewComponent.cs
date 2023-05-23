using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.DAL;

namespace Yummy.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public MenuViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Categories
                .Include(c => c.Products)
                .ToListAsync());
        }
    }
}
