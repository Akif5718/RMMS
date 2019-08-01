using RMMS.Data;
using RMMS.Repo.Account;
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
    }
}
