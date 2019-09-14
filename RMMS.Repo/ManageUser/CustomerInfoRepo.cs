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
        public Result<Customer> getCustomerByID(int id)
        {
            var result = new Result<Customer>();
            try
            {
                result.Data = DbContext.Customers.AsNoTracking().FirstOrDefault(u => u.C_ID == id);
                result.Data.UserInfo = DbContext.UserInfos.AsNoTracking().FirstOrDefault(u => u.ID == id);
                if (result.Data == null)
                {
                    result.HasError = true;
                    result.Message = "Customer is not found";
                    return result;
                }
                if (result.Data.UserInfo == null)
                {
                    result.HasError = true;
                    result.Message = "User is not found";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<UserInfo> editCustomers(UserInfo userInfo, Customer customer)
        {
            var result = new Result<UserInfo>();
            try
            {
                var user = DbContext.UserInfos.FirstOrDefault(u => u.ID == userInfo.ID);
                if (user == null)
                {
                    result.HasError = true;
                    result.Message = "User is not found";
                    return result;
                }
                user.Address = userInfo.Address;
                if(!IsValidEmail(user.Email))
                {
                    result.HasError = true;
                    result.Message = "Invalid Email Address";
                    return result;
                }
                user.Email = userInfo.Email;
                user.Name = userInfo.Name;
                if(userInfo.Password != null)
                    user.Password = userInfo.Password;
                if(DbContext.UserInfos.Any(u=>u.UserName == userInfo.UserName && u.ID != user.ID))
                {
                    result.HasError = true;
                    result.Message = "UserName Exists";
                    return result;
                }
                user.UserName = userInfo.UserName;
                var cust = DbContext.Customers.FirstOrDefault(u => u.C_ID == customer.C_ID);
                cust.Rate = customer.Rate;
                DbContext.SaveChanges();
                result.Data = user;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
