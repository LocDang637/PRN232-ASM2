using HivCare.Repository.PhatNH.ModelExtensions;
using HivCare.Repository.PhatNH.Models;
using HivCare.Services.PhatNH;

namespace HivCare.GraphQLAPIServices.PhatNH.GraphQLs
{
    public class Queries
    {
        private readonly IServiceProviders _serviceProvider;
        public Queries(IServiceProviders serviceProvider)
        {
                _serviceProvider = serviceProvider;
        }
        public async Task<List<DoctorAvailabilityPhatNh>> GetDoctorAvailabilityPhatNhs() {
            try
            {
                var result = await _serviceProvider.DoctorAvailabilityService.GetAllAsync();
                return result.Items.ToList() ?? new List<DoctorAvailabilityPhatNh>();
            }
            catch (Exception ex) { 
                return new List<DoctorAvailabilityPhatNh>();
            }

           
        }
        // Method for getting doctor availability by ID - fixed parameter type
        public async Task<DoctorAvailabilityPhatNh?> GetDoctorAvailabilityPhatNhById(int id)
        {
            try
            {
                var result = await _serviceProvider.DoctorAvailabilityService.GetGetByIdAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PaginationResult<DoctorAvailabilityPhatNh>> SearchWithPagingAsync(ClassSearchDoctorRequest request)
        {
            try
            {
                var result = await _serviceProvider.DoctorAvailabilityService.SearchAsyncWithPagination(request);

                return result ?? new PaginationResult<DoctorAvailabilityPhatNh>();
            }
            catch (Exception ex)
            {
            }

            return new PaginationResult<DoctorAvailabilityPhatNh>();
        }

        public async Task<List<DoctorPhatNh>> GetDoctorPhatNhs() { 
            return await _serviceProvider.DoctorPhatNhService.GetAllAsync();
        }


    }
}
