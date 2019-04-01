using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models.ViewModels
{
    public class SpecialDiscountModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Image Image { get; set; }
        public Company Company { get; set; }
    }
}
