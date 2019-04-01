using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models.ViewModels
{
    public class LastOffersModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Image Image { get; set; }
        public string URL { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
