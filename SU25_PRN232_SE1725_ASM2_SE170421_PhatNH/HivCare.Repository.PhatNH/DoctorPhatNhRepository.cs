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
    public class DoctorPhatNhRepository : GenericRepository<DoctorPhatNh>
    {
        public DoctorPhatNhRepository() => _context ??= new DBContext.HivCareContext();

        public DoctorPhatNhRepository(HivCareContext context) => _context = context;
        public async Task<int> CreateAsync(DoctorPhatNh entity)
        {
            try
            {
                var item = await _context.DoctorPhatNhs.OrderByDescending(x => x.DoctorPhatNhiD).FirstOrDefaultAsync();
                var id = (item != null) ? item.DoctorPhatNhiD + 1 : 1;
                entity.DoctorPhatNhiD = id;
                _context.Add(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
