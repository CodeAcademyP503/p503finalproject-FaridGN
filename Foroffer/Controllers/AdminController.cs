using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Foroffer.Models;
using Foroffer.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Foroffer.Controllers
{
    public class AdminController : Controller
    {
        private readonly OfferDbContext _offerDbContext;
        private readonly IHostingEnvironment _env;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminController(OfferDbContext offerDbContext, IHostingEnvironment env, SignInManager<AppUser> signInManager)
        {
            _offerDbContext = offerDbContext;
            _env = env;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Admin()
        {
            AdminPostModel postModel = new AdminPostModel();
            postModel.Posts = await _offerDbContext.Posts.ToListAsync();
            foreach(Post item in postModel.Posts)
            {
                if(item.ExpirationDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                {
                    _offerDbContext.Posts.Remove(item);
                    await _offerDbContext.SaveChangesAsync();
                }
            }
            postModel.Companies = await _offerDbContext.Companies.ToListAsync();
            int specCount = SpecCount();
            if(specCount > 5)
            {
                ViewBag.Warning = "SpecDiscount Overflow";
            }

            if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
            {
                return View(postModel);
            }
            else
            {
                return RedirectToAction(nameof(AccountController.LoginAdmin), "Account");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            CategoryModel catmodel = new CategoryModel();
            catmodel.Categories = await _offerDbContext.Categories.ToListAsync();
             return View(catmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _offerDbContext.Categories.Add(category);
                await _offerDbContext.SaveChangesAsync();
                CategoryModel catmodel = new CategoryModel();
                catmodel.Categories = await _offerDbContext.Categories.ToListAsync();
                catmodel.Category = await _offerDbContext.Categories.LastOrDefaultAsync();
                return RedirectToAction(nameof(CreateCategory));
            }
            else
            {
                ModelState.AddModelError("", "Couldn't create");
                return View();
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int Id)
        {
            CategoryModel catmodel = new CategoryModel();
            catmodel.Category = await _offerDbContext.Categories.SingleOrDefaultAsync(x => x.Id == Id);
            return View(catmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(Category category, int Id)
        {
            if(Id!= category.Id)
            {
                ModelState.AddModelError("", "Invalid Id");
                return View();
            }

            if (ModelState.IsValid)
            {
                CategoryModel catmodel = new CategoryModel();
                catmodel.Category = await _offerDbContext.Categories.SingleOrDefaultAsync(x => x.Id == Id);

                if (catmodel.Category != null)
                {
                    catmodel.Category.Name = category.Name;
                    await _offerDbContext.SaveChangesAsync();
                }
                return RedirectToAction(nameof(CreateCategory));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            CategoryModel catmodel = new CategoryModel();
            catmodel.Category = await _offerDbContext.Categories.SingleOrDefaultAsync(x => x.Id == Id);
            return View(catmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(Category category, int Id)
        {
            CategoryModel catmodel = new CategoryModel();
            catmodel.Category = await _offerDbContext.Categories.SingleOrDefaultAsync(x => x.Id == Id);
            category = catmodel.Category;
            _offerDbContext.Categories.Remove(category);
            await _offerDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(CreateCategory));
        }

        //Subcategory

        [HttpGet]
        public async Task<IActionResult> CreateSub()
        {
            SubViewModel subview = new SubViewModel();
            subview.Subcategories = await _offerDbContext.Subcategories.ToListAsync();
            subview.Categories = await _offerDbContext.Categories.ToListAsync();
            subview.CategoryList = await _offerDbContext.Categories.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToListAsync();

            return View(subview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSub(Subcategory subcategory)
        {
            
            if (ModelState.IsValid)
            {
                _offerDbContext.Subcategories.Add(subcategory);
                await _offerDbContext.SaveChangesAsync();
                SubViewModel subview = new SubViewModel();
                subview.CategoryList = await _offerDbContext.Categories.Select(a => new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }).ToListAsync();
                subview.Subcategories = await _offerDbContext.Subcategories.ToListAsync();
                subview.Categories = await _offerDbContext.Categories.ToListAsync();               
                subview.Subcategory = await _offerDbContext.Subcategories.LastOrDefaultAsync();
                return RedirectToAction(nameof(CreateSub));
            }
            else
            {
                ModelState.AddModelError("", "Couldn't create");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditSubcat(int Id)
        {
            SubViewModel subview = new SubViewModel();
            subview.Subcategory = await _offerDbContext.Subcategories.SingleOrDefaultAsync(x => x.Id == Id);
            subview.Categories = await _offerDbContext.Categories.ToListAsync();
            subview.CategoryList = await _offerDbContext.Categories.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToListAsync();

            return View(subview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubcat(Subcategory subcategory, int Id)
        {
            if(Id!= subcategory.Id)
            {
                ModelState.AddModelError("", "Invalid Id");
                return View();
            }

            if (ModelState.IsValid)
            {
                SubViewModel subview = new SubViewModel();
                subview.Subcategory = await _offerDbContext.Subcategories.SingleOrDefaultAsync(x => x.Id == Id);

                if (subview.Subcategory != null)
                {
                    subview.Subcategory.Name = subcategory.Name;
                    subview.Subcategory.CategoryId = subcategory.CategoryId;
                    await _offerDbContext.SaveChangesAsync();

                    subview.CategoryList = await _offerDbContext.Categories.Select(a => new SelectListItem()
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name
                    }).ToListAsync();
                    subview.Subcategories = await _offerDbContext.Subcategories.ToListAsync();
                    subview.Categories = await _offerDbContext.Categories.ToListAsync();
                    subview.Subcategory = await _offerDbContext.Subcategories.LastOrDefaultAsync();
                }
                return RedirectToAction(nameof(CreateSub));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSub(int Id)
        {
            SubViewModel subview = new SubViewModel();
            subview.Subcategory = await _offerDbContext.Subcategories.SingleOrDefaultAsync(x => x.Id == Id);
            return View(subview);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSub(Subcategory subcategory, int Id)
        {
            SubViewModel subview = new SubViewModel();
            subview.Subcategory = await _offerDbContext.Subcategories.SingleOrDefaultAsync(x => x.Id == Id);
            subcategory = subview.Subcategory;
            _offerDbContext.Subcategories.Remove(subcategory);
            await _offerDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(CreateSub));
        }

        //Company crud
        public async Task<IActionResult> CompanyList()
        {
            CompanyModel compmodel = new CompanyModel();
            compmodel.Companies = await _offerDbContext.Companies.ToListAsync();
            return View(compmodel);
        }


        [HttpGet]
        public IActionResult CreateComp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComp(IFormFile file, Company company)
        {

            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0)
                {
                    ModelState.AddModelError("", "No file selected");
                    return View();
                }
           
                string filepath = Path.Combine(_env.WebRootPath, "images", Path.GetFileName(file.FileName));

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                company.LogoPath = file.FileName;
                _offerDbContext.Companies.Add(company);
                await _offerDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(CompanyList));
            }
            else
            {
                ModelState.AddModelError("", "Couldn't create");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditComp(int Id)
        {
            CompanyModel compmodel = new CompanyModel();
            compmodel.Company = await _offerDbContext.Companies.SingleOrDefaultAsync(x => x.Id == Id);
            return View(compmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditComp(Company company, int Id, IFormFile file)
        {
            if(Id!= company.Id)
            {
                ModelState.AddModelError("", "Invalid Id");
            }

            if (ModelState.IsValid)
            {
                CompanyModel compmodel = new CompanyModel();
                compmodel.Company = await _offerDbContext.Companies.SingleOrDefaultAsync(x => x.Id == Id);
         
                if(compmodel.Company != null)
                {
                    if (file == null || file.Length == 0)
                    {
                        ModelState.AddModelError("", "No file selected");
                        return View();
                    }

                    string filepath = Path.Combine(_env.WebRootPath, "images", Path.GetFileName(file.FileName));

                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    company.LogoPath = file.FileName;
                    compmodel.Company.Name = company.Name;
                    compmodel.Company.VOEN = company.VOEN;
                    compmodel.Company.LogoPath = company.LogoPath;
                    compmodel.Company.Address = company.Address;
                    compmodel.Company.MoreLink = company.MoreLink;
                    compmodel.Company.Membership = company.Membership;

                    await _offerDbContext.SaveChangesAsync();
                    compmodel.Companies = await _offerDbContext.Companies.ToListAsync();
                }
                return RedirectToAction(nameof(CompanyList));
            }
            else
            {
                ModelState.AddModelError("", "Couldn't Edit");
                return View();
            }          

        }

        [HttpGet]
        public async Task<IActionResult> DeleteComp(int Id)
        {
            CompanyModel compmodel = new CompanyModel();
            compmodel.Company = await _offerDbContext.Companies.SingleOrDefaultAsync(x => x.Id == Id);
            return View(compmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComp(Company company, int Id)
        {
            CompanyModel compmodel = new CompanyModel();
            compmodel.Company = await _offerDbContext.Companies.SingleOrDefaultAsync(x => x.Id == Id);
            company = compmodel.Company;

              _offerDbContext.Companies.Remove(company);
            await _offerDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(CompanyList));
        }

        public int SpecCount()
        {
            AdminPostModel postModel = new AdminPostModel();
            postModel.Posts = _offerDbContext.Posts.ToList();
            int count = 0;
            foreach(Post item in postModel.Posts)
            {
                if(item.SpecDiscount == true)
                {
                    count++;
                }
            }
            return count;
        }
    }
}