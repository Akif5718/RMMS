using Framework.Constants;
using RMMS.Framework.Attributes;
using RMMS.Framework.Base;
using RMMS.Model.ManageUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMMS.Controllers
{
    public class ManageUserController : BaseController
    {
        #region Customer
        [RMMSAuthorize(EnumCollection.UserTypeEnum.Admin)]
        public ActionResult Customer()
        {
            var result = CustomerInfoRepo.loadCustomers();
            var model = new List<CustomerInfoModel>();
            if(!result.HasError)
            {
                foreach(var v in result.Data)
                {
                    var cust = new CustomerInfoModel() {
                        Address = v.Address,
                        Email = v.Email,
                        ID = v.ID,
                        IsActive = v.IsActive,
                        Name = v.Name,
                        UserCode = v.UserCode,
                        UserName = v.UserName,
                        UserTypeID = v.UserTypeID

                    };
                    model.Add(cust);
                }
            }
            if(TempData["trash"] != null)
            {
                ViewBag.Success = TempData["trash"];
            }
            if (TempData["trashError"] != null)
            {
                ViewBag.Error = TempData["trashError"];
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteCustomer(int id)
        {
            var result = CustomerInfoRepo.deleteCustomers(id);
            if (!result.HasError)
            {
                TempData["trash"] = "Customer has been moved into trash";
            }
            else
            {
                TempData["trashError"] = result.Message;
            }
            return JavaScript("window.location = '/ManageUser/Customer'");
        }
        [HttpPost]
        public ActionResult RevertCustomer(int id)
        {
            var result = CustomerInfoRepo.revertCustomers(id);
            if (!result.HasError)
            {
                TempData["trash"] = "Customer has been activated";
            }
            else
            {
                TempData["trashError"] = result.Message;
            }
            return JavaScript("window.location = '/ManageUser/Customer'");
        }
        [HttpPost]
        public ActionResult DeleteCustomerPermanently(int id)
        {
            var result = CustomerInfoRepo.deleteCustomerPermanently(id);
            if (!result.HasError)
            {
                TempData["trash"] = "Customer has been removed successfully";
            }
            else
            {
                TempData["trashError"] = result.Message;
            }
            return JavaScript("window.location = '/ManageUser/Customer'");
        }
        public JsonResult GetSearchingDataCustomerActive(string SearchValue)
        {
            List<CustomerInfoModel> customers = new List<CustomerInfoModel>();
            var finalList = new List<CustomerInfoModel>();
            try
            {
                var result = CustomerInfoRepo.loadCustomers();
                if(result.HasError)
                {
                    //return JavaScript("window.location = '/ManageUser/Customer'");
                }
                foreach(var v in result.Data)
                {
                    if(v.IsActive)
                    {
                        var cust = new CustomerInfoModel()
                        {
                            Address = v.Address,
                            Email = v.Email,
                            ID = v.ID,
                            IsActive = v.IsActive,
                            Name = v.Name,
                            UserCode = v.UserCode,
                            UserName = v.UserName,
                            UserTypeID = v.UserTypeID
                        };
                        customers.Add(cust);
                    }
                    


                }
                
                var listByUserCode = customers.Where(u => u.UserCode.ToString().Contains(SearchValue.Trim())).ToList();
                var listByUserName = customers.Where(u => u.UserName.Contains(SearchValue.Trim())).ToList();
                var listByName = customers.Where(u => u.Name.Contains(SearchValue.Trim())).ToList();
                var listByMail = customers.Where(u => u.Email.Contains(SearchValue.Trim())).ToList();
                if (string.IsNullOrEmpty(SearchValue) || string.IsNullOrWhiteSpace(SearchValue))
                    finalList = customers;
                else if ((listByUserCode.Count > listByUserName.Count || listByUserCode.Count == listByUserName.Count) && (listByUserCode.Count > listByName.Count || listByUserCode.Count == listByName.Count) && (listByUserCode.Count > listByMail.Count || listByUserCode.Count == listByMail.Count))
                    finalList = listByUserCode;
                else if ((listByUserName.Count > listByUserCode.Count || listByUserName.Count == listByUserCode.Count) && (listByUserName.Count > listByName.Count || listByUserName.Count > listByName.Count) && (listByUserName.Count > listByMail.Count || listByUserName.Count == listByMail.Count))
                    finalList = listByUserName;
                else if ((listByName.Count > listByUserName.Count || listByName.Count == listByUserName.Count) && (listByName.Count > listByUserCode.Count || listByName.Count == listByUserCode.Count) && (listByName.Count > listByMail.Count || listByName.Count == listByMail.Count))
                    finalList = listByName;
                else if ((listByMail.Count > listByUserName.Count || listByMail.Count == listByUserName.Count) && (listByMail.Count > listByName.Count || listByMail.Count == listByName.Count) && (listByMail.Count > listByUserCode.Count || listByMail.Count == listByUserCode.Count))
                    finalList = listByMail;
            }
            catch (FormatException)
            {
                //return JavaScript("window.location = '/ManageUser/Customer'");
            }
            return Json(finalList, JsonRequestBehavior.AllowGet);

            
        }
        public JsonResult GetSearchingDataCustomerTrash(string SearchValue)
        {
            List<CustomerInfoModel> customers = new List<CustomerInfoModel>();
            var finalList = new List<CustomerInfoModel>();
            try
            {
                var result = CustomerInfoRepo.loadCustomers();
                if (result.HasError)
                {
                    //return JavaScript("window.location = '/ManageUser/Customer'");
                }
                foreach (var v in result.Data)
                {
                    if (!v.IsActive)
                    {
                        var cust = new CustomerInfoModel()
                        {
                            Address = v.Address,
                            Email = v.Email,
                            ID = v.ID,
                            IsActive = v.IsActive,
                            Name = v.Name,
                            UserCode = v.UserCode,
                            UserName = v.UserName,
                            UserTypeID = v.UserTypeID
                        };
                        customers.Add(cust);
                    }



                }

                var listByUserCode = customers.Where(u => u.UserCode.ToString().Contains(SearchValue.Trim())).ToList();
                var listByUserName = customers.Where(u => u.UserName.Contains(SearchValue.Trim())).ToList();
                var listByName = customers.Where(u => u.Name.Contains(SearchValue.Trim())).ToList();
                var listByMail = customers.Where(u => u.Email.Contains(SearchValue.Trim())).ToList();
                if (string.IsNullOrEmpty(SearchValue) || string.IsNullOrWhiteSpace(SearchValue))
                    finalList = customers;
                else if ((listByUserCode.Count > listByUserName.Count || listByUserCode.Count == listByUserName.Count) && (listByUserCode.Count > listByName.Count || listByUserCode.Count == listByName.Count) && (listByUserCode.Count > listByMail.Count || listByUserCode.Count == listByMail.Count))
                    finalList = listByUserCode;
                else if ((listByUserName.Count > listByUserCode.Count || listByUserName.Count == listByUserCode.Count) && (listByUserName.Count > listByName.Count || listByUserName.Count > listByName.Count) && (listByUserName.Count > listByMail.Count || listByUserName.Count == listByMail.Count))
                    finalList = listByUserName;
                else if ((listByName.Count > listByUserName.Count || listByName.Count == listByUserName.Count) && (listByName.Count > listByUserCode.Count || listByName.Count == listByUserCode.Count) && (listByName.Count > listByMail.Count || listByName.Count == listByMail.Count))
                    finalList = listByName;
                else if ((listByMail.Count > listByUserName.Count || listByMail.Count == listByUserName.Count) && (listByMail.Count > listByName.Count || listByMail.Count == listByName.Count) && (listByMail.Count > listByUserCode.Count || listByMail.Count == listByUserCode.Count))
                    finalList = listByMail;
            }
            catch (FormatException)
            {
                //return JavaScript("window.location = '/ManageUser/Customer'");
            }
            return Json(finalList, JsonRequestBehavior.AllowGet);


        }
        #endregion





        #region Employee
        [RMMSAuthorize(EnumCollection.UserTypeEnum.Admin)]
        public ActionResult Employee()
        {
            var result = EmployeeInfoRepo.loadEmployees();
            var model = new List<EmployeeInfoModel>();
            if (!result.HasError)
            {
                foreach (var v in result.Data)
                {
                    var emp = new EmployeeInfoModel()
                    {
                        Address = v.Address,
                        Email = v.Email,
                        ID = v.ID,
                        IsActive = v.IsActive,
                        Name = v.Name,
                        UserCode = v.UserCode,
                        UserName = v.UserName,
                        UserTypeID = v.UserTypeID

                    };
                    model.Add(emp);
                }
            }
            if (TempData["trash"] != null)
            {
                ViewBag.Success = TempData["trash"];
            }
            if (TempData["trashError"] != null)
            {
                ViewBag.Error = TempData["trashError"];
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            var result = EmployeeInfoRepo.deleteEmployees(id);
            if (!result.HasError)
            {
                TempData["trash"] = "Employee has been moved into trash";
            }
            else
            {
                TempData["trashError"] = result.Message;
            }
            return JavaScript("window.location = '/ManageUser/Employee'");
        }
        [HttpPost]
        public ActionResult RevertEmployee(int id)
        {
            var result = EmployeeInfoRepo.revertEmployees(id);
            if (!result.HasError)
            {
                TempData["trash"] = "Employee has been activated";
            }
            else
            {
                TempData["trashError"] = result.Message;
            }
            return JavaScript("window.location = '/ManageUser/Employee'");
        }
        [HttpPost]
        public ActionResult DeleteEmployeePermanently(int id)
        {
            var result = EmployeeInfoRepo.deleteEmployeePermanently(id);
            if (!result.HasError)
            {
                TempData["trash"] = "Employee has been removed successfully";
            }
            else
            {
                TempData["trashError"] = result.Message;
            }
            return JavaScript("window.location = '/ManageUser/Employee'");
        }
        public JsonResult GetSearchingDataEmployeeActive(string SearchValue)
        {
            List<EmployeeInfoModel> empomers = new List<EmployeeInfoModel>();
            var finalList = new List<EmployeeInfoModel>();
            try
            {
                var result = EmployeeInfoRepo.loadEmployees();
                if (result.HasError)
                {
                    //return JavaScript("window.location = '/ManageUser/Employee'");
                }
                foreach (var v in result.Data)
                {
                    if (v.IsActive)
                    {
                        var emp = new EmployeeInfoModel()
                        {
                            Address = v.Address,
                            Email = v.Email,
                            ID = v.ID,
                            IsActive = v.IsActive,
                            Name = v.Name,
                            UserCode = v.UserCode,
                            UserName = v.UserName,
                            UserTypeID = v.UserTypeID
                        };
                        empomers.Add(emp);
                    }



                }

                var listByUserCode = empomers.Where(u => u.UserCode.ToString().Contains(SearchValue.Trim())).ToList();
                var listByUserName = empomers.Where(u => u.UserName.Contains(SearchValue.Trim())).ToList();
                var listByName = empomers.Where(u => u.Name.Contains(SearchValue.Trim())).ToList();
                var listByMail = empomers.Where(u => u.Email.Contains(SearchValue.Trim())).ToList();
                if (string.IsNullOrEmpty(SearchValue) || string.IsNullOrWhiteSpace(SearchValue))
                    finalList = empomers;
                else if ((listByUserCode.Count > listByUserName.Count || listByUserCode.Count == listByUserName.Count) && (listByUserCode.Count > listByName.Count || listByUserCode.Count == listByName.Count) && (listByUserCode.Count > listByMail.Count || listByUserCode.Count == listByMail.Count))
                    finalList = listByUserCode;
                else if ((listByUserName.Count > listByUserCode.Count || listByUserName.Count == listByUserCode.Count) && (listByUserName.Count > listByName.Count || listByUserName.Count > listByName.Count) && (listByUserName.Count > listByMail.Count || listByUserName.Count == listByMail.Count))
                    finalList = listByUserName;
                else if ((listByName.Count > listByUserName.Count || listByName.Count == listByUserName.Count) && (listByName.Count > listByUserCode.Count || listByName.Count == listByUserCode.Count) && (listByName.Count > listByMail.Count || listByName.Count == listByMail.Count))
                    finalList = listByName;
                else if ((listByMail.Count > listByUserName.Count || listByMail.Count == listByUserName.Count) && (listByMail.Count > listByName.Count || listByMail.Count == listByName.Count) && (listByMail.Count > listByUserCode.Count || listByMail.Count == listByUserCode.Count))
                    finalList = listByMail;
            }
            catch (FormatException)
            {
                //return JavaScript("window.location = '/ManageUser/Employee'");
            }
            return Json(finalList, JsonRequestBehavior.AllowGet);


        }
        public JsonResult GetSearchingDataEmployeeTrash(string SearchValue)
        {
            List<EmployeeInfoModel> empomers = new List<EmployeeInfoModel>();
            var finalList = new List<EmployeeInfoModel>();
            try
            {
                var result = EmployeeInfoRepo.loadEmployees();
                if (result.HasError)
                {
                    //return JavaScript("window.location = '/ManageUser/Employee'");
                }
                foreach (var v in result.Data)
                {
                    if (!v.IsActive)
                    {
                        var emp = new EmployeeInfoModel()
                        {
                            Address = v.Address,
                            Email = v.Email,
                            ID = v.ID,
                            IsActive = v.IsActive,
                            Name = v.Name,
                            UserCode = v.UserCode,
                            UserName = v.UserName,
                            UserTypeID = v.UserTypeID
                        };
                        empomers.Add(emp);
                    }



                }

                var listByUserCode = empomers.Where(u => u.UserCode.ToString().Contains(SearchValue.Trim())).ToList();
                var listByUserName = empomers.Where(u => u.UserName.Contains(SearchValue.Trim())).ToList();
                var listByName = empomers.Where(u => u.Name.Contains(SearchValue.Trim())).ToList();
                var listByMail = empomers.Where(u => u.Email.Contains(SearchValue.Trim())).ToList();
                if (string.IsNullOrEmpty(SearchValue) || string.IsNullOrWhiteSpace(SearchValue))
                    finalList = empomers;
                else if ((listByUserCode.Count > listByUserName.Count || listByUserCode.Count == listByUserName.Count) && (listByUserCode.Count > listByName.Count || listByUserCode.Count == listByName.Count) && (listByUserCode.Count > listByMail.Count || listByUserCode.Count == listByMail.Count))
                    finalList = listByUserCode;
                else if ((listByUserName.Count > listByUserCode.Count || listByUserName.Count == listByUserCode.Count) && (listByUserName.Count > listByName.Count || listByUserName.Count > listByName.Count) && (listByUserName.Count > listByMail.Count || listByUserName.Count == listByMail.Count))
                    finalList = listByUserName;
                else if ((listByName.Count > listByUserName.Count || listByName.Count == listByUserName.Count) && (listByName.Count > listByUserCode.Count || listByName.Count == listByUserCode.Count) && (listByName.Count > listByMail.Count || listByName.Count == listByMail.Count))
                    finalList = listByName;
                else if ((listByMail.Count > listByUserName.Count || listByMail.Count == listByUserName.Count) && (listByMail.Count > listByName.Count || listByMail.Count == listByName.Count) && (listByMail.Count > listByUserCode.Count || listByMail.Count == listByUserCode.Count))
                    finalList = listByMail;
            }
            catch (FormatException)
            {
                //return JavaScript("window.location = '/ManageUser/Employee'");
            }
            return Json(finalList, JsonRequestBehavior.AllowGet);


        }
        #endregion
    }
}