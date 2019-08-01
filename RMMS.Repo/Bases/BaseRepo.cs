using RMMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMS.Repo.Bases
{
    public class BaseRepo
    {
        private static RMMSDbContext _context;
        public static RMMSDbContext DbContext
        {
            get
            {
                if (_context == null)
                    _context = new RMMSDbContext();
                return _context;
            }
        }
    }
}
