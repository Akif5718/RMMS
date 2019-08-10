using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Entities
{
    public partial class Employee
    {
        [Key]
        public int E_ID { get; set; }
        [Required]
        public double Salary { get; set; }
        [ForeignKey("E_ID")]
        public UserInfo UserInfo { get; set; }
    }
}
