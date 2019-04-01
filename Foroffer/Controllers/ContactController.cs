using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foroffer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Foroffer.Controllers
{
    public class ContactController : Controller
    {
        private readonly OfferDbContext _offerDbContext;
        private readonly IStringLocalizer<ContactController> _localizer;

        public ContactController(OfferDbContext offerDbContext, IStringLocalizer<ContactController> localizer)
        {
            _offerDbContext = offerDbContext;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult Contact(string name)
        {
            if(name != null)
            {
                ViewBag.Message = "Mesajinizi gonderdiyiniz ucun tesekkurler";
            }    
            return View();
        }
    }
}