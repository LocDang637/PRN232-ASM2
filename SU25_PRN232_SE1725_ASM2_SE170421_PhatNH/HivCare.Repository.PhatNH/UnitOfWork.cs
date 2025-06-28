using HivCare.Repository.PhatNH.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HivCare.Repository.PhatNH
{
    public interface IUnitOfWork : IDisposable
    {
        SystemUserAccountRepository SystemUserAccountRepository { get; }
         DoctorAvailabilityPhatNhRepository DoctorAvailabilityPhatNhRepository { get; }
             DoctorPhatNhRepository DoctorPhatNhRepository { get; }    
    int SaveChangesWithTransaction();
        Task<int> SaveChangesWithTransactionAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
     
        private SystemUserAccountRepository _systemUserAccountRepository;
        private DoctorAvailabilityPhatNhRepository _doctorAvailabilityPhatNhRepository;
        private DoctorPhatNhRepository _doctorPhatNhRepository;
        private readonly HivCareContext _context;

    public UnitOfWork() =>  _context ??= new HivCareContext();

        public SystemUserAccountRepository SystemUserAccountRepository
        {
            get { return _systemUserAccountRepository ??= new SystemUserAccountRepository(_context); }
        }
        public DoctorAvailabilityPhatNhRepository DoctorAvailabilityPhatNhRepository
        {
            get { return _doctorAvailabilityPhatNhRepository ??= new DoctorAvailabilityPhatNhRepository(_context); }
        }

        public DoctorPhatNhRepository DoctorPhatNhRepository
        {
            get { return _doctorPhatNhRepository ??= new DoctorPhatNhRepository(_context); }
        }

        public void Dispose() =>_context.Dispose();

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
    }
}
