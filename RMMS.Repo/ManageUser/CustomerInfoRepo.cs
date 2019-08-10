using Framework.Constants;
using Framework.Objects;
using RMMS.Entities;
using RMMS.Repo.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Repo.ManageUser
{
    public class CustomerInfoRepo:BaseRepo
    {
        public Result<List<UserInfo>> loadCustomers()
        {
            var result = new Result<List<UserInfo>>();
            try
            {
                var customerList = DbContext.UserInfos.AsNoTracking().Where(u => u.UserTypeID == (int)EnumCollection.UserTypeEnum.Customer).ToList();
                result.Data = customerList;
            }catch(Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<UserInfo> deleteCustomers(int id)
        {
            var result = new Result<UserInfo>();
            try
            {
                var customer = DbContext.UserInfos.FirstOrDefault(u=>u.ID == id);
                if(customer == null)
                {
                    result.HasError = true;
                    result.Message = "User is not found";
                    return result;
                }
                customer.IsActive = false;
                DbContext.SaveChanges();
                result.Data = customer;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<UserInfo> revertCustomers(int id)
        {
            var result = new Result<UserInfo>();
            try
            {
                var customer = DbContext.UserInfos.FirstOrDefault(u => u.ID == id);
                if (customer == null)
                {
                    result.HasError = true;
                    result.Message = "User is not found";
                    return result;
                }
                customer.IsActive = true;
                DbContext.SaveChanges();
                result.Data = customer;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<UserInfo> deleteCustomerPermanently(int id)
        {
            var result = new Result<UserInfo>();
            try
            {
                var customer = DbContext.UserInfos.FirstOrDefault(u => u.ID == id);
                if (customer == null)
                {
                    result.HasError = true;
                    result.Message = "User is not found";
                    return result;
                }
                DbContext.UserInfos.Remove(customer);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
