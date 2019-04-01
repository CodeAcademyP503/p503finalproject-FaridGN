using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Foroffer.Models;
using Foroffer.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Foroffer.Controllers
{
    public class PostController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly OfferDbContext _offerDbContext;
        private readonly IStringLocalizer<PostController> _localizer;

        public PostController(IHostingEnvironment env, OfferDbContext offerDbContext, IStringLocalizer<PostController> localizer)
        {
            _env = env;
            _offerDbContext = offerDbContext;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Postadd()
        {
            AdminPostModel postModel = new AdminPostModel();
            postModel.Companies = await _offerDbContext.Companies.ToListAsync();
            postModel.Categories = await _offerDbContext.Categories.ToListAsync();
            postModel.CompanyList = await _offerDbContext.Companies.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name

            }).ToListAsync();

            //ViewBag.Categories = postModel.Categories;

            postModel.CategoryList = await _offerDbContext.Categories.Select(y => new SelectListItem()
            {
                Value = y.Id.ToString(),
                Text = y.Name
            }).ToListAsync();

            return View(postModel);
        }

        public IActionResult getSubList(int Id)
        {
            List<Subcategory> list = new List<Subcategory>();
            list = _offerDbContext.Subcategories.Where(a => a.CategoryId == Id).ToList();
           // list.Insert(0, new Subcategory { Id = 0, Name = "Please select" });
            return Json(new SelectList(list, "Id", "Name"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Postadd(Post post, IFormFile file, DateTime startDate, DateTime endDate, bool gifted, bool specDiscount)
        {
            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0)
                {
                    ModelState.AddModelError("", "No file selected");
                    return View();
                }

                string mypath = Path.Combine(_env.WebRootPath, "images", Path.GetFileName(file.FileName));

                using (var stream = new FileStream(mypath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                AdminPostModel postModel = new AdminPostModel();
                postModel.Companies = await _offerDbContext.Companies.ToListAsync();
                postModel.CompanyList = await _offerDbContext.Companies.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name

                }).ToListAsync();

                if(gifted == true)
                {
                    post.Gifted = true;
                }

                if(specDiscount == true)
                {
                    post.SpecDiscount = true;
                }
                post.CreatedDate = startDate;
                post.ExpirationDate = endDate;
                post.Image = file.FileName;              
                _offerDbContext.Posts.Add(post);
                await _offerDbContext.SaveChangesAsync();
                return RedirectToAction("Admin", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Couldn't create");
                return View();
            }  
           
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(int Id)
        {
            AdminPostModel postModel = new AdminPostModel();
            postModel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(x=> x.Id == Id);
            postModel.Companies = await _offerDbContext.Companies.ToListAsync();
            postModel.CompanyList = await _offerDbContext.Companies.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name

            }).ToListAsync();

            postModel.SubcategoryList = await _offerDbContext.Subcategories.Select(y => new SelectListItem()
            {
                Value = y.Id.ToString(),
                Text = y.Name

            }).ToListAsync();

            return View(postModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Post post, int Id, IFormFile file, DateTime startDate, DateTime endDate, bool gifted, bool specDiscount)
        {
            if(Id != post.Id)
            {
                ModelState.AddModelError("", "Invalid Id");
            }

            if (ModelState.IsValid)
            {
                AdminPostModel postModel = new AdminPostModel();
                postModel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(x => x.Id == Id);

                if(postModel.Post != null)
                {
                    if (file == null || file.Length == 0)
                    {
                        ModelState.AddModelError("", "No file selected");
                        return View();
                    }

                    string mypath = Path.Combine(_env.WebRootPath, "images", Path.GetFileName(file.FileName));

                    using (var stream = new FileStream(mypath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                   if(gifted == true)
                    {
                        post.Gifted = true;
                    }

                   if(specDiscount == true)
                    {
                        post.SpecDiscount = true;
                    }

                    post.Image = file.FileName;
                    post.CreatedDate = startDate;
                    post.ExpirationDate = endDate;
                    postModel.Post.Title = post.Title;
                    postModel.Post.Description = post.Description;
                    postModel.Post.CreatedDate = post.CreatedDate;
                    postModel.Post.ExpirationDate = post.ExpirationDate;
                    postModel.Post.Image = file.FileName;
                    postModel.Post.URL = post.URL;
                    postModel.Post.CompanyId = post.CompanyId;
                    postModel.Post.SubcategoryId = post.SubcategoryId;
                    postModel.Post.Gifted = post.Gifted;
                    postModel.Post.SpecDiscount = post.SpecDiscount;

                    await _offerDbContext.SaveChangesAsync();
                   
                }
                return RedirectToAction("Admin", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Couldn't edit");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeletePost(int Id)
        {
            AdminPostModel postModel = new AdminPostModel();
            postModel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(x => x.Id == Id);
            return View(postModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Post post, int Id)
        {
            AdminPostModel postModel = new AdminPostModel();
            postModel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(x => x.Id == Id);
            post = postModel.Post;

            _offerDbContext.Posts.Remove(post);
            await _offerDbContext.SaveChangesAsync();
            return RedirectToAction("Admin", "Admin");
        }
    }
}