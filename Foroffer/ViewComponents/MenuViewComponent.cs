using Foroffer.Models;
using Foroffer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Resources;

namespace Foroffer.ViewComponents
{
    public class MenuViewComponent:ViewComponent
    {
        private readonly OfferDbContext _offerDbContext;
        private readonly IStringLocalizer<MenuViewComponent> _localizer;

        public MenuViewComponent(OfferDbContext offerDbContext, IStringLocalizer<MenuViewComponent> localizer)
        {
            _offerDbContext = offerDbContext;
            _localizer = localizer;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menus = await _offerDbContext.Categories.Include(x => x.Subcategories)
                                             .Select(x => new MenuModel()
                                             {
                                                 Category = x,
                                                 Id = x.Id,
                                                 Action = x.Action,
                                                 Controller = x.Controller,
                                                 Subcategories = x.Subcategories.Select(y => new Subcategory()
                                                 {  
                                                     Id = y.Id,
                                                     Name = y.Name,
                                                     Action = y.Action,
                                                     Controller = y.Controller
                                                 })
                                             }).ToListAsync();

            return View(menus);
        }
    }
}
