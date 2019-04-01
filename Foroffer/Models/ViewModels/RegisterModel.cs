using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models.ViewModels
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required] 
        [Display(Name ="Full Name")]
        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }

        public static implicit operator AppUser(RegisterModel registerModel)
        {
            return new AppUser
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName,

            };
        }
    }
}
