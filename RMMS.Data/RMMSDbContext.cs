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
        public DbSet<ResetPasswordRequest> ResetPasswordRequests { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<RiceType> RiceTypes { get; set; }
        public DbSet<ExtraType> ExtraTypes { get; set; }
        public DbSet<ExtraBalanceDetail> ExtraBalanceDetails { get; set; }
    }
}
