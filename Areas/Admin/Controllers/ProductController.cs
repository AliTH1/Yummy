using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.Areas.ViewModels;
using Yummy.DAL;
using Yummy.Models;

namespace Yummy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ActionResult> Index(int page=1, int take=8)
        {
            List<Product> products = await _appDbContext.Products
                .Skip((page-1)*take)
                .Take(take)
                .Include(s => s.Category)
                .ToListAsync();

            int pageCount = await GetPageCount(take);

            PaginationVM<Product> paginationVM = new()
            {
                Data = products,
                PageCount = pageCount,
                CurrentPage = page,
                HasNext = page < pageCount,
                HasPrevious = page > 1,
                Take = take
            };


            return View(paginationVM);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        public async Task<int> GetPageCount(int take)
        {
            int serviceCount = await _appDbContext.Products.CountAsync();

            return (int)Math.Ceiling((double) serviceCount / take);
        }
    }
}
