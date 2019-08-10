using RMMS.Data;
using RMMS.Repo.Account;
using RMMS.Repo.ManageProfile;
using RMMS.Repo.ManageUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RMMS.Framework.Base
{
    public class BaseController: Controller
    {
        private static RMMSDbContext _context;
        public static RMMSDbContext DbContext
        {
            get {
                if(_context==null)
                    _context = new RMMSDbContext();
                return _context;
            }
        }
        private static UserInfoRepo _userInfoRepo;
        public static UserInfoRepo UserInfoRepo
        {
            get
            {
                if (_userInfoRepo == null)
                    _userInfoRepo = new UserInfoRepo();
                return _userInfoRepo;
            }
        }
        private static ResetPasswordRequestRepo _resetPasswordRequestRepo;
        public static ResetPasswordRequestRepo ResetPasswordRequestRepo
        {
            get
            {
                if (_resetPasswordRequestRepo == null)
                    _resetPasswordRequestRepo = new ResetPasswordRequestRepo();
                return _resetPasswordRequestRepo;
            }
        }

        private static UserRequestsRepo _userRequestRepo;
        public static UserRequestsRepo UserRequestsRepo
        {
            get
            {
                if (_userRequestRepo == null)
                    _userRequestRepo = new UserRequestsRepo();
                return _userRequestRepo;
            }
        }
        private static CustomerInfoRepo _customerInfoRepo;
        public static CustomerInfoRepo CustomerInfoRepo
        {
            get
            {
                if (_customerInfoRepo == null)
                    _customerInfoRepo = new CustomerInfoRepo();
                return _customerInfoRepo;
            }
        }
        private static EmployeeInfoRepo _employeeInfoRepo;
        public static EmployeeInfoRepo EmployeeInfoRepo
        {
            get
            {
                if (_employeeInfoRepo == null)
                    _employeeInfoRepo = new EmployeeInfoRepo();
                return _employeeInfoRepo;
            }
        }
    }
}
