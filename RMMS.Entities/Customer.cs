using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Entities
{
    public partial class Customer
    {
        [Key]
        public int C_ID { get; set; }
        [Required]
        public double SortedBalance { get; set; }
        [Required]
        public double UnsortedBalance { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public double ExtraBalance { get; set; }
        [ForeignKey("C_ID")]
        public UserInfo UserInfo { get; set; }
    }
}
