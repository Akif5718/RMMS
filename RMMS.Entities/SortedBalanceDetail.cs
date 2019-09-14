using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Entities
{
    public partial class SortedBalanceDetail
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int U_ID { get; set; }
        [Required]
        public int Rice_ID { get; set; }
        [Required]
        public double Amount { get; set; }

        [ForeignKey("U_ID")]
        public UserInfo UserInfo { get; set; }
        [ForeignKey("Rice_ID")]
        public RiceType RiceType { get; set; }
    }
}
