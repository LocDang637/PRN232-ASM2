using HivCare.Repository.PhatNH;
using HivCare.Repository.PhatNH.ModelExtensions;
using HivCare.Repository.PhatNH.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HivCare.Services.PhatNH
{
    public class DoctorAvailabilityPhatNhService : IDoctorAvailabilityPhatNhService
    {
        private readonly DoctorAvailabilityPhatNhRepository _doctorAvailabilityPhatNhRepository;
        public DoctorAvailabilityPhatNhService() => _doctorAvailabilityPhatNhRepository ??= new DoctorAvailabilityPhatNhRepository();

        public async Task<int> CreateAsync(DoctorAvailabilityPhatNh item)
        {
            return await _doctorAvailabilityPhatNhRepository.CreateAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _doctorAvailabilityPhatNhRepository.GetByIdAsync(id);
            return await _doctorAvailabilityPhatNhRepository.RemoveAsync(entity);
        }

        public async Task<DoctorAvailabilityPhatNh> GetGetByIdAsync(int id)
        {
            return await _doctorAvailabilityPhatNhRepository.GetByIdAsync(id);
        }

        public async Task<PaginationResult<DoctorAvailabilityPhatNh>> GetAllAsync(int? currentPage = 1, int? pageSize = 0)
        {
            return await _doctorAvailabilityPhatNhRepository.GetAllAsyncWithPagination(currentPage:(int) currentPage, pageSize: (int) pageSize);
        }

        public async Task<List<DoctorAvailabilityPhatNh>> SearchAsync(string? DayOfWeek, string? Notes, string Status)
        {

            return await _doctorAvailabilityPhatNhRepository.SearchAsync(DayOfWeek: DayOfWeek, Notes: Notes, Status: Status);
        }

        public async Task<PaginationResult<DoctorAvailabilityPhatNh>> SearchAsyncWithPagination(string? DayOfWeek, string? Notes, string? Status, int currentPage, int pageSize)
        {

            var list = await _doctorAvailabilityPhatNhRepository.SearchAsync(DayOfWeek: DayOfWeek, Notes: Notes, Status: Status);
            var totalItems = list.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            list = list.Skip((currentPage-1) * pageSize).Take(pageSize).ToList();
            var result = new PaginationResult<DoctorAvailabilityPhatNh>
            {
                CurrentPage = currentPage,
                Items = list,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
            return result ?? new PaginationResult<DoctorAvailabilityPhatNh>();
        }



        public async Task<int> UpdateAsync(DoctorAvailabilityPhatNh doctorAvailabilityPhatNh)
        {
            var existingItem = await _doctorAvailabilityPhatNhRepository.GetByIdAsync(doctorAvailabilityPhatNh.DoctorAvailabilityPhatNhiD);
            existingItem.DoctorPhatNhiD = doctorAvailabilityPhatNh.DoctorPhatNhiD;
            existingItem.DayOfWeek = doctorAvailabilityPhatNh.DayOfWeek;
            existingItem.StartTime = doctorAvailabilityPhatNh.StartTime;
            existingItem.EndTime = doctorAvailabilityPhatNh.EndTime;
            existingItem.SpecificDate = doctorAvailabilityPhatNh.SpecificDate;
            existingItem.MaxAppointments = doctorAvailabilityPhatNh.MaxAppointments;
            existingItem.BreakStartTime = doctorAvailabilityPhatNh.BreakStartTime;
            existingItem.BreakEndTime = doctorAvailabilityPhatNh.BreakEndTime;
            existingItem.Status = doctorAvailabilityPhatNh.Status;
            existingItem.Notes = doctorAvailabilityPhatNh.Notes;
            existingItem.UpdatedAt = DateTime.Now;
            existingItem.IsEmergencyAvailable = doctorAvailabilityPhatNh.IsEmergencyAvailable;


            return await _doctorAvailabilityPhatNhRepository.UpdateAsync(existingItem);
        }

        public async Task<PaginationResult<DoctorAvailabilityPhatNh>> SearchAsyncWithPagination(ClassSearchDoctorRequest request)
        {
            var list = await _doctorAvailabilityPhatNhRepository.SearchAsync(DayOfWeek: request.DayOfWeek, Notes: request.Notes, Status: request.Status);
            var totalItem = list.Count();
            var totalPages = (int)Math.Ceiling((double)totalItem / (request.PageSize>0? request.PageSize:1));
            if (request.PageSize > 0) { 
            list = list.Skip(((int)request.CurrentPage - 1) * (int)request.PageSize).Take((int)request.PageSize).ToList();

            }
            return  new PaginationResult<DoctorAvailabilityPhatNh> { 
                CurrentPage = request.CurrentPage,
                PageSize = request.PageSize,
                Items = list,
                TotalItems = totalItem,
                TotalPages = totalPages,
            };
        }
    }
}
