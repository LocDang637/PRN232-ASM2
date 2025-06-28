using HivCare.Repository.PhatNH;
using HivCare.Repository.PhatNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HivCare.Services.PhatNH
{
    public class DoctorPhatNhService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DoctorPhatNhService() => _unitOfWork ??= new UnitOfWork();


        public async Task<List<DoctorPhatNh>> GetAllAsync()
        {
            return await _unitOfWork.DoctorPhatNhRepository.GetAllAsync();
        }
        
    }
}
