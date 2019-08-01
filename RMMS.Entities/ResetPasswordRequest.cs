using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Entities
{
    public partial class ResetPasswordRequest
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public DateTime RequestOn { get; set; }
        [Required]
        public string GUID { get; set; }
        [ForeignKey("UserID")]
        public UserInfo UserInfo { get; set; }

    }
}
