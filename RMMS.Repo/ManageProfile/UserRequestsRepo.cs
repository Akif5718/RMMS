using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Helper;
using Framework.Objects;
using RMMS.Entities;
using RMMS.Repo.Bases;

namespace RMMS.Repo.ManageProfile
{
    public class UserRequestsRepo : BaseRepo
    {
        public Result<UserRequest> SendRequest(UserRequest userRequest)
        {
            Result<UserRequest> result = new Result<UserRequest>();
            try
            {
                //var objectToSave = DbContext.UserInfos.AsNoTracking().FirstOrDefault(u => u.ID == userInfo.ID);
                
                 var   objectToSave = new UserRequest();
                    DbContext.UserRequests.Add(objectToSave);
                
                objectToSave.UserName = userRequest.UserName;
                objectToSave.Name = userRequest.Name;
                objectToSave.Email = userRequest.Email;
                objectToSave.Password = userRequest.Password;
               
                objectToSave.Address = userRequest.Address;
                objectToSave.Created_at = DateTime.Now;


                if (!IsValid(objectToSave, result))
                {
                    DetachAllEntities();
                    return result;
                }

                DbContext.SaveChanges();
            }
            catch(Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }
            return result;
        }
        public Result<List<UserRequest>> LoadRequests()
        {
            Result<List<UserRequest>> result = new Result<List<UserRequest>>();
            try
            {
                var objectToSave = DbContext.UserRequests.AsNoTracking().OrderByDescending(o=>o.Created_at).ToList();
                result.Data = objectToSave;
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }
            return result;
        }
        public Result<UserRequest> deleteRequest(int id)
        {
            var result = new Result<UserRequest>();
            try
            {
                var objToDel = DbContext.UserRequests.FirstOrDefault(ur => ur.ID == id);
                if(objToDel == null)
                {
                    result.HasError = true;
                    result.Message = "User is not found";
                    return result;
                }
                DbContext.UserRequests.Remove(objToDel);
                DbContext.SaveChanges();
                result.Data = objToDel;
            }catch(Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }



        private bool IsValid(UserRequest obj, Result<UserRequest> result)
        {
            if (!ValidationHelper.IsStringValid(obj.Name))
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

            if (!ValidationHelper.IsStringValid(obj.Password))
            {
                result.HasError = true;
                result.Message = "Invalid Address";
                return false;
            }

            if (DbContext.UserInfos.Any(u => u.UserName == obj.UserName && u.ID != obj.ID))
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
            if (DbContext.UserRequests.Any(u => u.UserName == obj.UserName && u.ID != obj.ID))
            {
                result.HasError = true;
                result.Message = "UserName Exists";
                return false;
            }
            if (DbContext.UserRequests.Any(u => u.Email == obj.Email && u.ID != obj.ID))
            {
                result.HasError = true;
                result.Message = "Email Exists";
                return false;
            }
            return true;
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
