using System.ComponentModel.DataAnnotations;

namespace RMMS.Entities
{
    public partial class UserInfo
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int UserTypeID { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
