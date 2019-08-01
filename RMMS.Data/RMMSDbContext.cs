using RMMS.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Data
{
    public class RMMSDbContext:DbContext
    {
        public RMMSDbContext():base("RMMSConnectionString")
        {

        }
        public DbSet<UserInfo> UserInfos { get; set; }
    }
}
