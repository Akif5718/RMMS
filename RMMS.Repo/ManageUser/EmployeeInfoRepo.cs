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
    public class EmployeeInfoRepo:BaseRepo
    {
        public Result<List<UserInfo>> loadEmployees()
        {
            var result = new Result<List<UserInfo>>();
            try
            {
                var employeeList = DbContext.UserInfos.AsNoTracking().Where(u => u.UserTypeID == (int)EnumCollection.UserTypeEnum.Employee).ToList();
                result.Data = employeeList;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<UserInfo> deleteEmployees(int id)
        {
            var result = new Result<UserInfo>();
            try
            {
                var employee = DbContext.UserInfos.FirstOrDefault(u => u.ID == id);
                if (employee == null)
                {
                    result.HasError = true;
                    result.Message = "User is not found";
                    return result;
                }
                employee.IsActive = false;
                DbContext.SaveChanges();
                result.Data = employee;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<UserInfo> revertEmployees(int id)
        {
            var result = new Result<UserInfo>();
            try
            {
                var employee = DbContext.UserInfos.FirstOrDefault(u => u.ID == id);
                if (employee == null)
                {
                    result.HasError = true;
                    result.Message = "User is not found";
                    return result;
                }
                employee.IsActive = true;
                DbContext.SaveChanges();
                result.Data = employee;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<UserInfo> deleteEmployeePermanently(int id)
        {
            var result = new Result<UserInfo>();
            try
            {
                var employee = DbContext.UserInfos.FirstOrDefault(u => u.ID == id);
                if (employee == null)
                {
                    result.HasError = true;
                    result.Message = "User is not found";
                    return result;
                }
                DbContext.UserInfos.Remove(employee);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<Employee> getEmployeeByID(int id)
        {
            var result = new Result<Employee>();
            try
            {
                result.Data = DbContext.Employees.AsNoTracking().FirstOrDefault(u => u.E_ID == id);
                result.Data.UserInfo = DbContext.UserInfos.AsNoTracking().FirstOrDefault(u => u.ID == id);
                if (result.Data == null)
                {
                    result.HasError = true;
                    result.Message = "Employee is not found";
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
        public Result<UserInfo> editEmployee(UserInfo userInfo, Employee employee)
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
                if (!IsValidEmail(user.Email))
                {
                    result.HasError = true;
                    result.Message = "Invalid Email Address";
                    return result;
                }
                user.Email = userInfo.Email;
                user.Name = userInfo.Name;
                if (userInfo.Password != null)
                    user.Password = userInfo.Password;
                if (DbContext.UserInfos.Any(u => u.UserName == userInfo.UserName && u.ID != user.ID))
                {
                    result.HasError = true;
                    result.Message = "UserName Exists";
                    return result;
                }
                user.UserName = userInfo.UserName;
                var emp = DbContext.Employees.FirstOrDefault(u => u.E_ID == employee.E_ID);
                emp.Salary = emp.Salary;
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
