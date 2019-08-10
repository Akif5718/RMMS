using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Model.ManageUser
{
    public class CustomerInfoModel
    {
        public int ID { get; set; }
        public int UserCode { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int UserTypeID { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
