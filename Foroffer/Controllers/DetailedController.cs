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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Foroffer.Controllers
{
    public class DetailedController : Controller
    {
        private readonly OfferDbContext _offerDbContext;
        private readonly IHostingEnvironment _hostenv;
        private readonly IStringLocalizer<DetailedController> _localizer;
        private static bool _edited;

        public DetailedController(OfferDbContext offerDbContext, IHostingEnvironment hostenv, IStringLocalizer<DetailedController> localizer)
        {
            _offerDbContext = offerDbContext;
            _hostenv = hostenv;
            _localizer = localizer;
        }

        public async Task<IActionResult> Detailed(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Detailed = await _offerDbContext.Detaileds.Where(x => x.PostId == Id).SingleOrDefaultAsync();
            dvmodel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(y => y.Id == Id);
            dvmodel.Detailed.Pictures = await _offerDbContext.Pictures.Where(e => e.Detailed.PostId == Id).ToListAsync();
            dvmodel.Detailed.Features = await _offerDbContext.Features.Where(f => f.Detailed.PostId == Id).ToListAsync();
            dvmodel.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View(dvmodel);
        }

        public async Task<IActionResult> DetailedList()
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Detaileds = await _offerDbContext.Detaileds.ToListAsync();
            dvmodel.Pictures = await _offerDbContext.Pictures.ToListAsync();
            dvmodel.Features = await _offerDbContext.Features.ToListAsync();
            dvmodel.Posts = await _offerDbContext.Posts.ToListAsync();
            if(_edited == true)
            {
                ViewBag.Message = "Successfully edited";
            } 
            return View(dvmodel);
        }

        //CRUD
        //Detailed
            [HttpGet]
            public async Task<IActionResult> CreateDetailed(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(x => x.Id == Id);
            return View(dvmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetailed(Detailed detailed, int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(x => x.Id == Id);
            detailed.PostId = Id;
            _offerDbContext.Detaileds.Add(detailed);
            await _offerDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Moderate));
        }
          
        [HttpGet]
        public async Task<IActionResult> Moderate()
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Detailed = await _offerDbContext.Detaileds.OrderByDescending(a => a.Id).FirstOrDefaultAsync();
            return View(dvmodel);
        }
        
        //Picture
        [HttpGet]
        public async Task<IActionResult> AddPicture(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Detailed = await _offerDbContext.Detaileds.SingleOrDefaultAsync(x => x.Id == Id);
            return View(dvmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPicture(List<IFormFile> files, int Id)
        {
            if (files == null || files.Count == 0)
            {
                ModelState.AddModelError("", "No file selected");
                return View();
            }

            foreach(var file in files)
            {
                if(file.Length > 0)
                {
                    Picture picture = new Picture();
                    string filePath = Path.Combine(_hostenv.WebRootPath, "images", Path.GetFileName(file.FileName));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }                   
                    picture.PicPath = file.FileName;
                    DetailedViewModel dvmodel = new DetailedViewModel();
                    dvmodel.Detailed = await _offerDbContext.Detaileds.SingleOrDefaultAsync(x => x.Id == Id);
                    picture.DetailedId = Id;
                    _offerDbContext.Pictures.Add(picture);
                    await _offerDbContext.SaveChangesAsync();
                }  
            }
            return RedirectToAction(nameof(GoToFeature));
        }

        //Feature
        [HttpGet]
        public async Task<IActionResult> SetFeature(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Detailed = await _offerDbContext.Detaileds.SingleOrDefaultAsync(x => x.Id == Id);
            dvmodel.Features = await _offerDbContext.Features.Where(a => a.DetailedId == Id).ToListAsync();
            return View(dvmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetFeature(Feature feature, int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Detailed = await _offerDbContext.Detaileds.SingleOrDefaultAsync(x => x.Id == Id);
            feature.DetailedId = Id;
            _offerDbContext.Features.Add(feature);
            await _offerDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(SetFeature));
        }

        [HttpGet]
        public async Task<IActionResult> GoToFeature()
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Detailed = await _offerDbContext.Detaileds.OrderByDescending(a => a.Id).FirstOrDefaultAsync();
            return View(dvmodel);
        }

        //Edit
        [HttpGet]
        public async Task<IActionResult> EditDetailed(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Detailed = await _offerDbContext.Detaileds.SingleOrDefaultAsync(x => x.Id == Id);
            return View(dvmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetailed(int Id, Detailed detailed)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Detailed = await _offerDbContext.Detaileds.SingleOrDefaultAsync(x => x.Id == Id);

            if(Id != detailed.Id)
            {
                ModelState.AddModelError("", "Invalid Id");
            }

            if (ModelState.IsValid)
            {
                dvmodel.Detailed.Outline = detailed.Outline;
                dvmodel.Detailed.CompanyInfo = detailed.CompanyInfo;
                await _offerDbContext.SaveChangesAsync();
                _edited = true;
                return RedirectToAction(nameof(DetailedList));
            }
            else
            {
                ModelState.AddModelError("", "Couldn't Edit");
                return View();
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDetailed(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Detailed = await _offerDbContext.Detaileds.SingleOrDefaultAsync(c => c.Id == Id);
            dvmodel.Detailed.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(d => d.Detailed.Id == Id);
            return View(dvmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDetailed(int Id, Detailed detailed)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Detailed = await _offerDbContext.Detaileds.SingleOrDefaultAsync(c => c.Id == Id);
            detailed = dvmodel.Detailed;
            _offerDbContext.Detaileds.Remove(detailed);
            await _offerDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(DetailedList));
        }

        [HttpGet]
        public async Task<IActionResult> GetPictures(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Pictures = await _offerDbContext.Pictures.Where(a => a.DetailedId == Id).ToListAsync();
            return View(dvmodel);
        }

        [HttpGet]
        public async Task<IActionResult> EditPicture(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Picture = await _offerDbContext.Pictures.SingleOrDefaultAsync(y => y.Id == Id);
            return View(dvmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPicture(int Id, IFormFile file, Picture picture)
        {
            if(Id != picture.Id)
            {
                ModelState.AddModelError("", "invalid picture id");
            }

            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Picture = await _offerDbContext.Pictures.SingleOrDefaultAsync(y => y.Id == Id);

            if(dvmodel.Picture != null)
            {
                if(file != null || file.Length > 0)
                {
                    string filePath = Path.Combine(_hostenv.WebRootPath, "images", Path.GetFileName(file.FileName));

                    using(var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    picture.PicPath = file.FileName;
                    dvmodel.Picture.PicPath = picture.PicPath;
                    await _offerDbContext.SaveChangesAsync();
                }

                _edited = true;
            }
            return RedirectToAction(nameof(DetailedList));
        }

        [HttpGet]
        public async Task<IActionResult> DeletePicture(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Picture = await _offerDbContext.Pictures.SingleOrDefaultAsync(y => y.Id == Id);
            return View(dvmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePicture(int Id, Picture picture)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Picture = await _offerDbContext.Pictures.SingleOrDefaultAsync(y => y.Id == Id);
            picture = dvmodel.Picture;

            _offerDbContext.Pictures.Remove(picture);
            await _offerDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(GetPictures));
        }

        [HttpGet]
        public async Task<IActionResult> GetFeatures(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Features = await _offerDbContext.Features.Where(c => c.DetailedId == Id).ToListAsync();
            return View(dvmodel);
        }

       [HttpGet]
       public async Task<IActionResult> EditFeature(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Feature = await _offerDbContext.Features.SingleOrDefaultAsync(z => z.Id == Id);
            return View(dvmodel);
        }

       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> EditFeature(int Id, Feature feature)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Feature = await _offerDbContext.Features.SingleOrDefaultAsync(z => z.Id == Id);

            if(Id != feature.Id)
            {
                ModelState.AddModelError("", "Invalid feature Id");
            }

            if (ModelState.IsValid)
            {
                if(feature != null)
                {
                    dvmodel.Feature.Name = feature.Name;
                    dvmodel.Feature.Parameter = feature.Parameter;
                    await _offerDbContext.SaveChangesAsync();
                }
                _edited = true;
            }     
            return RedirectToAction(nameof(DetailedList));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFeature(int Id)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Feature = await _offerDbContext.Features.SingleOrDefaultAsync(b => b.Id == Id);
            return View(dvmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFeature(int Id, Feature feature)
        {
            DetailedViewModel dvmodel = new DetailedViewModel();
            dvmodel.Feature = await _offerDbContext.Features.SingleOrDefaultAsync(z => z.Id == Id);
            feature = dvmodel.Feature;
            _offerDbContext.Features.Remove(feature);
            await _offerDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(DetailedList));
        }
    }
}