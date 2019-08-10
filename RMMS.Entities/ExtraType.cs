using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Entities
{
    public partial class ExtraType
    {
        [Key]
        public int Extra_ID { get; set; }
        [Required]
        public string ExtraName { get; set; }
    }
}
