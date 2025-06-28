using HivCare.Repository.PhatNH.Basic;
using HivCare.Repository.PhatNH.DBContext;
using HivCare.Repository.PhatNH.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HivCare.Repository.PhatNH
{
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccountRepository() => _context ??= new DBContext.HivCareContext();

        public SystemUserAccountRepository(HivCareContext context) => _context = context;

        public async Task<SystemUserAccount> GetUserAccountAsync(string user, string passwor)
        {
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(d => d.UserName == user && d.Password == passwor && d.IsActive == true);

            return await _context.SystemUserAccounts.FirstOrDefaultAsync(d => d.UserName == user && d.Password == passwor);
        }
    }
}
