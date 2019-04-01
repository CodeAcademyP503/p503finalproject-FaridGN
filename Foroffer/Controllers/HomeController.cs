using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Foroffer.Models;
using Microsoft.EntityFrameworkCore;
using Foroffer.Models.ViewModels;
using PagedList.Core;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace Foroffer.Controllers
{
    public class HomeController : Controller
    {
        private readonly OfferDbContext _offerDbContext;
        private readonly IStringLocalizer<HomeController> _localizer;
        static int category_id = 0;
        static int subId = 0;
        static int menuCatId = 0;

        public HomeController(OfferDbContext offerDbContext, IStringLocalizer<HomeController> localizer)
        {
            _offerDbContext = offerDbContext;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            SlideViewModel svmodel = new SlideViewModel();
            svmodel.Images = await _offerDbContext.Images.ToListAsync();
            svmodel.Posts = _offerDbContext.Posts.Where(x => x.Company.Membership == "Paid").OrderByDescending(x => x.CreatedDate).Take(3);
            ViewBag.specDiscounts = await _offerDbContext.Posts.Where(y => y.SpecDiscount == true).ToListAsync();
            svmodel.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewBag.Gifted = await _offerDbContext.Posts.Where(z => z.Gifted == true).ToListAsync();
            ViewBag.Others = _offerDbContext.Posts.OrderBy(k => k.CreatedDate).Take(4);
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View(svmodel);
        }

        public IActionResult Category(int Id)
        {
            category_id = Id;
            SubViewModel subModel = new SubViewModel(); 
            subModel.Subcategories = _offerDbContext.Subcategories.Where(x => x.CategoryId == Id).ToList();
            subModel.Posts = _offerDbContext.Posts.Where(y => y.Subcategory.CategoryId == Id).ToList();
            subModel.Companies = _offerDbContext.Companies.ToList();
            ViewBag.Posts = _offerDbContext.Posts.Where(u => u.Subcategory.CategoryId == category_id).ToList();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View(subModel);
        }

        public async Task<IActionResult> Subcategory(int Id)
        {
            subId = Id;
            SubViewModel subView = new SubViewModel();
            subView.Subcategory = await _offerDbContext.Subcategories.Where(y => y.Id == Id).SingleOrDefaultAsync();
            subView.Posts = await _offerDbContext.Posts.Where(z => z.SubcategoryId == Id).ToListAsync();
            subView.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewBag.subCategories = _offerDbContext.Subcategories.Where(c => c.CategoryId == category_id).ToList();
            ViewBag.SubPosts = await _offerDbContext.Posts.Where(n => n.SubcategoryId == Id).ToListAsync();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View(subView);
        }

        //from menu dropdown
        public async Task<IActionResult> MenuSub(int Id)
        {
            SubViewModel subView = new SubViewModel();
            subView.Subcategory = await _offerDbContext.Subcategories.Where(y => y.Id == Id).SingleOrDefaultAsync();
            menuCatId = subView.Subcategory.CategoryId;
            subView.Posts = await _offerDbContext.Posts.Where(z => z.SubcategoryId == Id).ToListAsync();
            subView.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewBag.subCategories = _offerDbContext.Subcategories.Where(c => c.CategoryId == menuCatId).ToList();
            ViewBag.SubPosts = await _offerDbContext.Posts.Where(n => n.SubcategoryId == Id).ToListAsync();
            return View("Subcategory", subView);
        }

        public async Task<IActionResult> getCompany(int Id)
        {
            SubViewModel subViewModel = new SubViewModel();
            subViewModel.Subcategories = await _offerDbContext.Subcategories.Where(x => x.CategoryId == category_id).ToListAsync();
            subViewModel.Companies = await _offerDbContext.Companies.Where(a => a.Id == Id).ToListAsync();
            subViewModel.Posts = await _offerDbContext.Posts.Where(b => b.CompanyId == Id && b.Subcategory.CategoryId == category_id).ToListAsync();
            ViewBag.subCategories = await _offerDbContext.Subcategories.Where(c => c.CategoryId == category_id).ToListAsync();
            ViewBag.Posts = await _offerDbContext.Posts.Where(u => u.Subcategory.CategoryId == category_id).ToListAsync();
            ViewBag.Companies = await _offerDbContext.Companies.ToListAsync();
            return View("Category", subViewModel);
        }

        public async Task<IActionResult> getSubCompany(int Id)
        {
            SubViewModel svmodel = new SubViewModel();
            svmodel.Subcategory = await _offerDbContext.Subcategories.Where(z => z.Id == subId).SingleOrDefaultAsync();
            svmodel.Posts = await _offerDbContext.Posts.Where(t => t.SubcategoryId == subId && t.CompanyId == Id).ToListAsync();
            svmodel.Companies = await _offerDbContext.Companies.Where(k => k.Id == Id).ToListAsync();
            ViewBag.subCategories = await _offerDbContext.Subcategories.Where(c => c.CategoryId == category_id).ToListAsync();
            ViewBag.SubPosts = await _offerDbContext.Posts.Where(n => n.SubcategoryId == subId).ToListAsync();
            ViewBag.Companies = await _offerDbContext.Companies.ToListAsync();
            return View("Subcategory", svmodel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(int page = 1, int pageSize = 3)
        {
            var keyword = Request.Query["keyword"].ToString();
            var results = _offerDbContext.Posts.Where(p => p.Title.Contains(keyword) || p.Description.Contains(keyword));
            var comps = await _offerDbContext.Companies.ToListAsync();
            PagedList<Post> pagemodel = new PagedList<Post>(results, page, pageSize);
            ViewBag.keyword = keyword;
            return View(pagemodel);
        }

        [HttpGet]
        public IActionResult About()
        {
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
