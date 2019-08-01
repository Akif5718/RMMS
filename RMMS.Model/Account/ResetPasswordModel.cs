using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Model.Account
{
    public class ResetPasswordModel
    {
        [Required]
        public string UserName { get; set; }
    }
}
