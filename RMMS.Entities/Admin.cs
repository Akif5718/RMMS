using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Entities
{
    public partial class Admin
    {
        [Key]
        public int A_ID { get; set; }
        [Required]
        public double ExtraBalance { get; set; }
        [ForeignKey("A_ID")]
        public UserInfo UserInfo { get; set; }
    }
}
