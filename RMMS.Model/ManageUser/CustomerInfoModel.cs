using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Model.ManageUser
{
    public class CustomerInfoModel
    {
        public int ID { get; set; }
        public int UserCode { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        public int UserTypeID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public double SortedBalance { get; set; }
        public double UnSortedBalance { get; set; }
        public double Value { get; set; }
        [Required]
        public double Rate { get; set; }
        public double ExtraBalance { get; set; }

        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
