﻿using RMMS.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMMS.Entities;
using RMMS.Framework.Base;
using Framework.Constants;
using System.Web.Security;
using Framework.Objects;
using Newtonsoft.Json;
using RMMS.Framework.Util;

namespace RMMS.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Registration()
        {
            if(User.Identity.IsAuthenticated && HttpUtil.UserProfile != null)
            {
                return RedirectToAction("Dashboard", "Main");
            }
            var model = new RegistrationModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Registration(RegistrationModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var UserInfo = new UserInfo() {
                UserName = model.UserName,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                UserTypeID = (int)EnumCollection.UserTypeEnum.Customer

            };
            var result = UserInfoRepo.Save(UserInfo);
            if(result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }
            return RedirectToAction("Index","Home");
        }


        public ActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated && HttpUtil.UserProfile != null)
            {
                return RedirectToAction("About", "Home");
            }
            var model = new LogInModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = UserInfoRepo.Login(model.UserName,model.Password);
            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }
            var userProfile = new UserProfile() {
                ID = result.Data.ID,
                Name = result.Data.Name,
                UserName = result.Data.UserName,
                Email = result.Data.Email,
                UserTypeID = result.Data.UserTypeID
            };
            var UserProfileJason = JsonConvert.SerializeObject(userProfile);
            FormsAuthentication.SetAuthCookie(UserProfileJason, false);
            return RedirectToAction("Dashboard", "Main");
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Account");
        }

    }
}