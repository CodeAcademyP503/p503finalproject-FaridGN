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

namespace Foroffer.Controllers
{
    public class SlidesController : Controller
    {
        private readonly OfferDbContext _offerDbContext;
        private readonly IHostingEnvironment _hostenv;

        public SlidesController(OfferDbContext offerDbContext, IHostingEnvironment hostenv)
        {
            _offerDbContext = offerDbContext;
            _hostenv = hostenv;
        }

        [HttpGet]
        public async Task<IActionResult> Slide()
        {
            SlideViewModel svmodel = new SlideViewModel();
            svmodel.Images = await _offerDbContext.Images.ToListAsync();
            return View(svmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Slide(IFormFile file, Image myimage)
        {
            if(file == null || file.Length == 0)
            {
                Content("No file chosen");
                return View();
            }

            string slidePath = Path.Combine(_hostenv.WebRootPath, "images", Path.GetFileName(file.FileName));

            using (var stream = new FileStream(slidePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            myimage.Path = file.FileName;
            _offerDbContext.Images.Add(myimage);
            await _offerDbContext.SaveChangesAsync();
            SlideViewModel svmodel = new SlideViewModel();
            svmodel.Images = await _offerDbContext.Images.ToListAsync();
            return RedirectToAction(nameof(Slide));
        }

        [HttpGet]
        public async Task<IActionResult> EditSlide(int Id)
        {
            SlideViewModel slideViewModel = new SlideViewModel();
            slideViewModel.Image = await _offerDbContext.Images.SingleOrDefaultAsync(x => x.Id == Id);
            return View(slideViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSlide(int Id, Image newimage, IFormFile file)
        {
            if(Id != newimage.Id)
            {
                Content("Invalid Id");
            }

            SlideViewModel slideViewModel = new SlideViewModel();
            slideViewModel.Image = await _offerDbContext.Images.SingleOrDefaultAsync(x => x.Id == Id);

            if(slideViewModel.Image != null)
            {
                if (file == null || file.Length == 0)
                {
                    Content("No file chosen");
                    return View();
                }

                string slidePath = Path.Combine(_hostenv.WebRootPath, "images", Path.GetFileName(file.FileName));

                using (var stream = new FileStream(slidePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                newimage.Path = file.FileName;
                slideViewModel.Image.Path = newimage.Path;
                await _offerDbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Slide));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSlide(int Id)
        {
            SlideViewModel svmodel = new SlideViewModel();
            svmodel.Image = await _offerDbContext.Images.SingleOrDefaultAsync(x => x.Id == Id);
            return View(svmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSlide(int Id, Image currentimage)
        {
            SlideViewModel svmodel = new SlideViewModel();
            svmodel.Image = await _offerDbContext.Images.SingleOrDefaultAsync(x => x.Id == Id);

            currentimage = svmodel.Image;
            _offerDbContext.Images.Remove(currentimage);
           await _offerDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Slide));
        }
    }
}