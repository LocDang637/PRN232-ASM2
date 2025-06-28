using HivCare.Repository.PhatNH.Models;
using HivCare.Repository.PhatNH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HivCare.Services.PhatNH
{
    public class SystemUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SystemUserAccountService() => _unitOfWork ??= new UnitOfWork();


        public async Task<SystemUserAccount> GetUserAccount(string usermame, string password)
        {
            return await _unitOfWork.SystemUserAccountRepository.GetUserAccountAsync(usermame, password);
        }

        public async Task<List<SystemUserAccount>> GetAllAsync()
        {
            return await _unitOfWork.SystemUserAccountRepository.GetAllAsync();
        }

    }
}
