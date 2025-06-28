using HivCare.Repository.PhatNH.Basic;
using HivCare.Repository.PhatNH.DBContext;
using HivCare.Repository.PhatNH.ModelExtensions;
using HivCare.Repository.PhatNH.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HivCare.Repository.PhatNH
{
    public class DoctorAvailabilityPhatNhRepository : GenericRepository<DoctorAvailabilityPhatNh>
    {
        public DoctorAvailabilityPhatNhRepository() => _context ??= new DBContext.HivCareContext();

        public DoctorAvailabilityPhatNhRepository(HivCareContext context) => _context = context;

        // Lấy toàn bộ lịch sử khám và include thông tin bệnh nhân
        public async Task<List<DoctorAvailabilityPhatNh>> GetAllAsync()
        {
            var availabilityDoctors = await _context.DoctorAvailabilityPhatNhs
                .Include(v => v.DoctorPhatNh).OrderByDescending(x => x.DoctorAvailabilityPhatNhiD)
                .ToListAsync();

            return availabilityDoctors ?? new List<DoctorAvailabilityPhatNh>();
        }

        // Lấy một lần khám theo ID
        public async Task<DoctorAvailabilityPhatNh> GetByIdAsync(int id)
        {
            var visit = await _context.DoctorAvailabilityPhatNhs
                .Include(v => v.DoctorPhatNh).AsNoTracking()
                .FirstOrDefaultAsync(v => v.DoctorAvailabilityPhatNhiD == id);

            return visit ?? new DoctorAvailabilityPhatNh();
        }
        public async Task<int> CreateAsync(DoctorAvailabilityPhatNh entity)
        {
            try
            {
                var item = await _context.DoctorAvailabilityPhatNhs.OrderByDescending(x => x.DoctorAvailabilityPhatNhiD).FirstOrDefaultAsync();
                var id = (item != null) ? item.DoctorAvailabilityPhatNhiD + 1 : 1;
                entity.DoctorAvailabilityPhatNhiD = id;
                _context.Add(entity);
               return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // Tìm kiếm theo 3 điều kiện: CD4Count, ViralLoad, Diagnosis (cho phép null)
        public async Task<List<DoctorAvailabilityPhatNh>> SearchAsync(string? DayOfWeek, string? Notes, string Status)
        {
            var query = _context.DoctorAvailabilityPhatNhs
       .Include(v => v.DoctorPhatNh)
       .OrderByDescending(x => x.DoctorAvailabilityPhatNhiD)
       .AsQueryable();

            if (!string.IsNullOrWhiteSpace(DayOfWeek))
            {
                query = query.Where(d => d.DayOfWeek != null && d.DayOfWeek.ToLower().Equals(DayOfWeek.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                query = query.Where(v => v.Status != null && v.Status.ToLower().Equals(Status.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(Notes))
            {
                query = query.Where(d => d.Notes != null && d.Notes.ToLower().Contains(Notes.ToLower()));
            }

            return await query.ToListAsync();
        }


        public async Task<PaginationResult<DoctorAvailabilityPhatNh>> GetAllAsyncWithPagination(int currentPage, int pageSize)
        {
            var availabilityDoctors = _context.DoctorAvailabilityPhatNhs
                .Include(v => v.DoctorPhatNh).OrderByDescending(x => x.DoctorAvailabilityPhatNhiD)
                .AsQueryable();

            var TotalItem = availabilityDoctors.Count();
            var totalPages = 1;
            if (pageSize > 0)
            {
                totalPages = (int)Math.Ceiling((double)TotalItem / pageSize);

                availabilityDoctors = availabilityDoctors.Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var result = new PaginationResult<DoctorAvailabilityPhatNh>
            {

                CurrentPage = currentPage,
                Items = availabilityDoctors,
                PageSize = pageSize,
                TotalItems = TotalItem,
                TotalPages = totalPages
            };
            return result ?? new PaginationResult<DoctorAvailabilityPhatNh>();
        }
        public async Task<PaginationResult<DoctorAvailabilityPhatNh>> GetAllAsyncWithPagination(string? DayOfWeek, DateTime? SpecificDate, string Status, int currentPage, int pageSize)
        {
            var query = _context.DoctorAvailabilityPhatNhs
               .Include(v => v.DoctorPhatNh)
               .AsQueryable();

            if (!String.IsNullOrEmpty(DayOfWeek))
                query = query.Where(v => v.DayOfWeek == DayOfWeek);

            if (SpecificDate.HasValue)
                query = query.Where(v => v.SpecificDate.Value.Date == SpecificDate.Value.Date);

            if (Status != null)
                query = query.Where(v => v.Status.Contains(Status));

            var TotalItem = query.Count();
            var totalPages = (int)Math.Ceiling((double)TotalItem / pageSize);
            query = query.Skip((currentPage-1) * pageSize).Take(pageSize);
            var result = new PaginationResult<DoctorAvailabilityPhatNh>
            {
                CurrentPage = currentPage,
                Items = query,
                PageSize = pageSize,
                TotalItems = TotalItem,
                TotalPages = totalPages
            };
            return result ?? new PaginationResult<DoctorAvailabilityPhatNh>();
        }
        public async Task<string> existSchedule(int id)
        {
            bool hasSchedules = await _context.DoctorAvailabilityPhatNhs
         .AnyAsync(a => a.DoctorPhatNhiD == id);
            if (hasSchedules)
                return "Cannot delete doctor because exist history remains.";
            return null;

        }
    }

}
