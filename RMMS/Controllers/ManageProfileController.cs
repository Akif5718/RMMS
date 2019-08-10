using Framework.Constants;
using RMMS.Entities;
using RMMS.Framework.Attributes;
using RMMS.Framework.Base;
using RMMS.Model.ManageProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Objects;
using System.Threading;

namespace RMMS.Controllers
{
    public class ManageProfileController : BaseController
    {
        // GET: ManageProfile
        [RMMSAuthorize(EnumCollection.UserTypeEnum.Admin)]
        public ActionResult UserRequests()
        {
            var model = new List<UserRequestModel>();
            var result = UserRequestsRepo.LoadRequests();
            if(result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }
            foreach (var v in result.Data)
            {
                var modelObj = new UserRequestModel();
                modelObj.UserName = v.UserName;
                modelObj.FullName = v.Name;
                modelObj.Email = v.Email;
                modelObj.Address = v.Address;
                modelObj.ID = v.ID;
                modelObj.Status = "Not Approved";
                modelObj.selectedList = "0";
               

                model.Add(modelObj);
                
            }
            
            return View(model);
        }
        [HttpPost]
        public ActionResult UserRequests(int id,string type)
        {
            if(id != 0 && !string.IsNullOrEmpty(type) && type!="0")
            {
                try
                {
                    var userRequest = UserRequestsRepo.LoadRequests().Data.FirstOrDefault(d => d.ID == id);
                    var userInfo = new UserInfo()
                    {
                        Address = userRequest.Address,
                        Email = userRequest.Email,
                        Name = userRequest.Name,
                        Password = userRequest.Password,
                        UserName = userRequest.UserName,
                        UserTypeID = Int32.Parse(type)
                    };
                    UserInfoRepo.Save(userInfo);
                    UserRequestsRepo.deleteRequest(id);
                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        /* run your code here */
                        ForgotPassword.ConfirmationMail(userRequest.Email, userRequest.Name, "Your account has been created Successfully. Please go to the following link to continue.", userRequest.UserName, userRequest.Password);
                    }).Start();
                    
                    //TempData["ConfirmationMsg"] = "A confirmation mail has been sent";
                }
                catch(Exception ex)
                {

                }
                

            }
            return JavaScript("location.reload(true)");
        }
        [HttpPost]
        public ActionResult DeleteRequest(int id)
        {
            var result = UserRequestsRepo.deleteRequest(id);
            if (result.HasError)
            {

            }
            //return JavaScript("window.location = '/Main/Dashboard'");
            return JavaScript("location.reload(true)");
            
        }

    }
}