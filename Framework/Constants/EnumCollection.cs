using Framework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Constants
{
    public class EnumCollection
    {
        #region UserType
        public enum UserTypeEnum
        {
            Admin = 1,
            Customer = 2,
            Employee = 4
        }
        public static List<EnumDetail> GetUserTypeEnums()
        {
            var list = new List<EnumDetail>();
            list.Add(new EnumDetail() {
                ID = 1, Name = "Admin"
            });
            list.Add(new EnumDetail()
            {
                ID = 2,
                Name = "Customer"
            });
            list.Add(new EnumDetail()
            {
                ID = 1,
                Name = "Employee"
            });
            return list;
        }
        #endregion
    }
}
