using HivCare.Repository.PhatNH.Models;
using HivCare.Services.PhatNH;

namespace HivCare.GraphQLAPIServices.PhatNH.GraphQLs
{
    public class Mutations
    {
        private readonly IServiceProviders _serviceProvider;
        public Mutations(IServiceProviders serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<int> CreateDoctorAvailabilityPhatNh(DoctorAvailabilityPhatNh createDoctorAvailabilityPhatNhInput)
        {
            try
            {
                var result = await _serviceProvider.DoctorAvailabilityService.CreateAsync(createDoctorAvailabilityPhatNhInput);

                return (int)result;
            }
            catch (Exception ex)
            {
            }

            return 0;
        }

        public async Task<int> UpdateDoctorAvailabilityPhatNh(DoctorAvailabilityPhatNh updateDoctorAvailabilityPhatNhInput)
        {
            try
            {
                var result = await _serviceProvider.DoctorAvailabilityService.UpdateAsync(updateDoctorAvailabilityPhatNhInput);

                return (int)result;
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        public async Task<bool> DeleteDoctorAvailabilityPhatNh(int id)
        {
            try
            {
                var result = await _serviceProvider.DoctorAvailabilityService.DeleteAsync(id);

                return (bool)result;
            }
            catch (Exception ex)
            {
            }

            return false;
        }
    }
}
