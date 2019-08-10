using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Entities
{
    public partial class RiceType
    {
        [Key]
        public int Rice_ID { get; set; }
        [Required]
        public string RiceName { get; set; }
    }
}
