using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Model.Account
{
    public class RegistrationModel
    {
        [Display]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string CPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
