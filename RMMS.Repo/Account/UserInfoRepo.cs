using Framework.Helper;
using Framework.Objects;
using RMMS.Entities;
using RMMS.Repo.Bases;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Repo.Account
{
    public class UserInfoRepo:BaseRepo
    {
        public Result<UserInfo> Login(string emailOrUserName ,string password)
        {
            var result = new Result<UserInfo>();
            try
            {
                var objToSave = DbContext.UserInfos.FirstOrDefault(u=>(u.Email == emailOrUserName || u.UserName == emailOrUserName) && u.Password == password);
                if(objToSave == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Email/UserName or Password ";
                    return result;
                }
                result.Data = objToSave;

            }catch(Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
                return result;
            }
            return result;
        }
        public Result<UserInfo> Save(UserInfo userInfo)
        {
            var result = new Result<UserInfo>();
            try
            {
                var objectToSave = DbContext.UserInfos.AsNoTracking().FirstOrDefault(u => u.ID == userInfo.ID);
                if (objectToSave == null)
                {
                    objectToSave = new UserInfo();
                    DbContext.UserInfos.Add(objectToSave);
                }
                objectToSave.UserName = userInfo.UserName;
                objectToSave.Name = userInfo.Name;
                objectToSave.Email = userInfo.Email;
                objectToSave.Password = userInfo.Password;
                objectToSave.UserTypeID = userInfo.UserTypeID;

                if (!IsValid(objectToSave, result))
                {
                    DetachAllEntities();
                    return result;
                }
                    
                DbContext.SaveChanges();
                result.Data = objectToSave;

            }
            catch(Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            
            
            return result;
        }
        private bool IsValid(UserInfo obj, Result<UserInfo> result)
        {
            if(!ValidationHelper.IsStringValid(obj.Name))
            {
                result.HasError = true;
                result.Message = "Invalid Name";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.UserName))
            {
                result.HasError = true;
                result.Message = "Invalid UserName";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Email))
            {
                result.HasError = true;
                result.Message = "Invalid Email";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Password))
            {
                result.HasError = true;
                result.Message = "Invalid Password";
                return false;
            }
            if(DbContext.UserInfos.Any(u=>u.UserName == obj.UserName && u.ID != obj.ID))
            {
                result.HasError = true;
                result.Message = "UserName Exists";
                return false;
            }
            if (DbContext.UserInfos.Any(u => u.Email == obj.Email && u.ID != obj.ID))
            {
                result.HasError = true;
                result.Message = "Email Exists";
                return false;
            }
            return true;
        }
        public Result<UserInfo> ChangePassword(string guid,string password)
        {
            var result = new Result<UserInfo>();
            try
            {
                List<ResetPasswordRequest> resetPasswordRequestObj = DbContext.ResetPasswordRequests.Where(r => r.GUID.Trim().Equals(guid.Trim())).ToList();
                if(resetPasswordRequestObj.Count() == 0)
                {
                    result.HasError = true;
                    result.Message = "Request is expired";
                    return result;
                }
                else if(resetPasswordRequestObj.Count() > 1)
                {
                    result.HasError = true;
                    result.Message = "Something went wrong";
                    return result;
                }
                ResetPasswordRequest oTmp = resetPasswordRequestObj[0];
                var userInfoObj = DbContext.UserInfos.FirstOrDefault(u => u.ID == oTmp.UserID);
                if(userInfoObj == null)
                {
                    result.HasError = true;
                    result.Message = "Something went wrong";
                    return result;
                }
                userInfoObj.Password = password;
                DbContext.SaveChanges();
                result.Data = userInfoObj;
                

            }catch(Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public void DetachAllEntities()
        {
            var changedEntriesCopy = DbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
    }
}
