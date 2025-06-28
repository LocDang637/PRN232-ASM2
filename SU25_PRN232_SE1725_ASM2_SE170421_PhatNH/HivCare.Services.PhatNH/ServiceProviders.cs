using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HivCare.Services.PhatNH
{
    public interface IServiceProviders
    {
        SystemUserAccountService UserAccountService { get; }
  
        DoctorPhatNhService DoctorPhatNhService { get; }
        IDoctorAvailabilityPhatNhService DoctorAvailabilityService { get; }
    }

    public class ServiceProviders : IServiceProviders
    {
        private SystemUserAccountService _userAccountService;
        private DoctorPhatNhService _doctorPhatNhService;
        private IDoctorAvailabilityPhatNhService _doctorAvailabilityService;

        public ServiceProviders() { }

        public SystemUserAccountService UserAccountService
        {
            get { return _userAccountService ??= new SystemUserAccountService(); }
        }

        public DoctorPhatNhService DoctorPhatNhService
        {
            get { return _doctorPhatNhService ??= new DoctorPhatNhService(); }
        }

        public IDoctorAvailabilityPhatNhService DoctorAvailabilityService
        {
            get { return _doctorAvailabilityService ??= new DoctorAvailabilityPhatNhService(); }
        }
    }
}