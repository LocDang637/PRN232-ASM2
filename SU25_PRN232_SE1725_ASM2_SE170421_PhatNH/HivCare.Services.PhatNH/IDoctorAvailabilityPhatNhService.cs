using HivCare.Repository.PhatNH.ModelExtensions;
using HivCare.Repository.PhatNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HivCare.Services.PhatNH
{
    public interface IDoctorAvailabilityPhatNhService
    {


        Task<DoctorAvailabilityPhatNh> GetGetByIdAsync(int id);

        Task<List<DoctorAvailabilityPhatNh>> SearchAsync(string? DayOfWeek, string? Notes, string Status);
        Task<PaginationResult<DoctorAvailabilityPhatNh>> SearchAsyncWithPagination(string? DayOfWeek, string? Notes, string? Status, int currentPage, int pageSize);
        Task<PaginationResult<DoctorAvailabilityPhatNh>> SearchAsyncWithPagination(ClassSearchDoctorRequest request);
        Task<PaginationResult<DoctorAvailabilityPhatNh>> GetAllAsync(int? currentPage = 1, int? pageSize = 0);

        Task<int> CreateAsync(DoctorAvailabilityPhatNh item);
        Task<int> UpdateAsync( DoctorAvailabilityPhatNh doctorAvailabilityPhatNh);
        Task<bool> DeleteAsync(int id);
    }
}
