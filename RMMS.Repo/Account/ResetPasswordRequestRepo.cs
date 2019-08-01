using Framework.Objects;
using RMMS.Entities;
using RMMS.Repo.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Repo.Account
{
    public class ResetPasswordRequestRepo : BaseRepo
    {
        public Result<ResetPasswordRequest> GetMail(string emailOrUserName)
        {
            var result = new Result<ResetPasswordRequest>();
            try
            {
                var userInfoObject = DbContext.UserInfos.FirstOrDefault(u => u.UserName.Equals(emailOrUserName.Trim()) || u.Email.Equals(emailOrUserName));
                if (userInfoObject == null)
                {
                    result.HasError = true;
                    result.Message = "Email/UserName is not exist";
                    return result;
                }
                string guid = Guid.NewGuid().ToString();
                ForgotPassword.SendPasswordResetEmail(userInfoObject.Email, userInfoObject.Name, guid);
                var objToSave = new ResetPasswordRequest();
                objToSave.UserID = userInfoObject.ID;
                objToSave.RequestOn = DateTime.Now;
                objToSave.GUID = guid;
                DbContext.ResetPasswordRequests.Add(objToSave);

                DbContext.SaveChanges();

                result.Data = objToSave;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;

            }
            return result;

        }
        public bool isUrlValid(string guid)
        {
            try
            {
                List<ResetPasswordRequest> resetPasswordRequestObj = DbContext.ResetPasswordRequests.Where(r => r.GUID.Trim().Equals(guid.Trim())).ToList();
                if (resetPasswordRequestObj.Count() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
