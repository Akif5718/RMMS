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
    }
}
