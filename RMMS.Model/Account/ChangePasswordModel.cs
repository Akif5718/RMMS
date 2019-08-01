using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Model.Account
{
    public class ChangePasswordModel
    {
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword")]
        public string CPassword { get; set; }

        public string id { get; set; }
    }
}
