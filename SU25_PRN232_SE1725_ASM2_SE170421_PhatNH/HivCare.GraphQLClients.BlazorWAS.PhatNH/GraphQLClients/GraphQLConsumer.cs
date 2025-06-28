using GraphQL;
using GraphQL.Client.Abstractions;
using HivCare.GraphQLClients.BlazorWAS.PhatNH.Models;

namespace HivCare.GraphQLClients.BlazorWAS.PhatNH.GraphQLClients
{
    public class GraphQLConsumer
    {
        private readonly IGraphQLClient _graphQLClient;
        public GraphQLConsumer(IGraphQLClient graphQLClient) => _graphQLClient = graphQLClient;
        // chon  số cột mà mình muốn

        public async Task<List<DoctorAvailabilityPhatNh>> GetDoctorAvailabilityPhatNhs()
        {
            try
            {
                var query = @"query DoctorAvailabilityPhatNhs {
                 doctorAvailabilityPhatNhs {
                 doctorAvailabilityPhatNhiD
                 doctorPhatNhiD
                 dayOfWeek
                 startTime
                 endTime
                 specificDate
                 maxAppointments
        breakStartTime
        breakEndTime
        status
        notes
        isEmergencyAvailable
        createdAt
        updatedAt
    }
}";


                var response = await _graphQLClient.SendQueryAsync<DoctorAvailabilityPhatNhsResponse>(query);
                var result = response?.Data?.doctorAvailabilityPhatNhs;
                return result.OrderByDescending(x=>x.DoctorAvailabilityPhatNhiD).ToList() ?? new List<DoctorAvailabilityPhatNh>();
            }
            catch (Exception ex)
            {
                // Log exception if needed
                // _logger.LogError(ex, "Error fetching doctor availability data");
                return new List<DoctorAvailabilityPhatNh>();
            }
        }

        public async Task<PaginationResult> GetDoctorAvailabilityPhatNhsWithPagination(ClassSearchDoctorRequestInput request)
        {
            try
            {
                var query = @"query SearchWithPaging {
    searchWithPaging(
        request: { 
            notes: """ + (request.Notes ?? "a") + @""", 
            status: """ + (request.Status ?? "a") + @""", 
            currentPage: " + (request.CurrentPage ?? 1) + @", 
            pageSize: " + (request.PageSize ?? 2) + @", 
            dayOfWeek: """ + (request.DayOfWeek ?? "a") + @""" 
        }
    ) {
        totalPages
        currentPage
        pageSize
        totalItems
        items {
            doctorAvailabilityPhatNhiD
            doctorPhatNhiD
            dayOfWeek
            startTime
            endTime
            specificDate
            maxAppointments
            breakStartTime
            breakEndTime
            status
            notes
            isEmergencyAvailable
            createdAt
            updatedAt
            doctorPhatNh {
                doctorPhatNhiD
                fullName
                email
                phone
                licenseNumber
                specialization
                experienceYears
                consultationFee
                department
                bio
                rating
                practicingSince
                certificationDetails
                isAvailableAfterHours
                createdAt
                updatedAt
            }
        }
    }
}";

                var response = await _graphQLClient.SendQueryAsync<DoctorAvailabilityPhatNhsWithPaginationResponse>(query);
                var result = response?.Data?.searchWithPaging;

                if (result?.items != null)
                {
                    // Sort items by ID descending
                    result.items = result.items.OrderByDescending(x => x.DoctorAvailabilityPhatNhiD).ToList();
                }

                return result ?? new PaginationResult
                {
                    totalPages = 0,
                    currentPage = request.CurrentPage ?? 1,
                    pageSize = request.PageSize ?? 10,
                    totalItems = 0,
                    items = new List<DoctorAvailabilityPhatNh>()
                };
            }
            catch (Exception ex)
            {
                // Log exception if needed
                // _logger.LogError(ex, "Error fetching doctor availability data with pagination");

                return new PaginationResult
                {
                    totalPages = 0,
                    currentPage = request.CurrentPage ?? 1,
                    pageSize = request.PageSize ?? 10,
                    totalItems = 0,
                    items = new List<DoctorAvailabilityPhatNh>()
                };
            }
        }
        public async Task<DoctorAvailabilityPhatNh> GetDoctorAvailabilityPhatNh(int id)
        {
            try
            {
                #region GraphQL Request

                var graphQLRequest = new GraphQLRequest
                {
                    Query = @"query DoctorAvailabilityPhatNhById($id: Int!) {
                    doctorAvailabilityPhatNhById(id: $id) {
         doctorAvailabilityPhatNhiD
        doctorPhatNhiD
        dayOfWeek
        startTime
        endTime
        specificDate
        maxAppointments
        breakStartTime
        breakEndTime
        status
        notes
        isEmergencyAvailable
        createdAt
        updatedAt
    }
}
"
                    ,
                    Variables = new { id = id }
                    //,OperationName = "CategoryBankAccounts"
                };
                #endregion


                var response = await _graphQLClient.SendQueryAsync<DoctorAvailabilityPhatNhdResponse>(graphQLRequest);
                var result = response?.Data?.doctorAvailabilityPhatNhById??new DoctorAvailabilityPhatNh();

                return result;
            }
            catch (Exception ex)
            {
                return new DoctorAvailabilityPhatNh();
            }
        }

        public async Task<int> UpdateDoctorAvailabilityPhatNh(DoctorAvailabilityPhatNh doctorAvailabilityPhatNh)
        {
            try
            {
                var graphQLRequest = new GraphQLRequest
                {
                    Query = @"
                mutation updateDoctorAvailabilityPhatNh ($input: DoctorAvailabilityPhatNhInput!) {
                    updateDoctorAvailabilityPhatNh (updateDoctorAvailabilityPhatNhInput: $input)
                }",
                    Variables = new { input = doctorAvailabilityPhatNh }
                };


                var response = await _graphQLClient.SendMutationAsync<UpdateDoctorAvailabilityPhatNhdResponse>(graphQLRequest);
                var result = response.Data.updateDoctorAvailabilityPhatNh;

                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<int> CreateDoctorAvailabilityPhatNh(DoctorAvailabilityPhatNh doctorAvailabilityPhatNh)
        {
            try
            {

                var graphQLRequest = new GraphQLRequest
                {
                    Query = @"
                mutation createDoctorAvailabilityPhatNh ($input: DoctorAvailabilityPhatNhInput!) {
                    createDoctorAvailabilityPhatNh (createDoctorAvailabilityPhatNhInput: $input)
                }",
                    Variables = new { input = doctorAvailabilityPhatNh }
                };




                var response = await _graphQLClient.SendMutationAsync<CreateDoctorAvailabilityPhatNhdResponse>(graphQLRequest);
                var result = response.Data.createDoctorAvailabilityPhatNh;

                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<List<DoctorPhatNh>> GetDoctorPhatNhs() { 
            try
            {
                var query = @"query DoctorPhatNhs {
    doctorPhatNhs {
        doctorPhatNhiD
        fullName
        email
        phone
        licenseNumber
        specialization
        experienceYears
        consultationFee
        department
        bio
        rating
        practicingSince
        certificationDetails
        isAvailableAfterHours
        createdAt
        updatedAt
    }
}
";
                var response = await _graphQLClient.SendQueryAsync<DoctorPhatNhsResponse>(query);
                var result = response?.Data?.doctorPhatNhs;
                return result.OrderByDescending(x => x.DoctorPhatNhiD).ToList() ?? new List<DoctorPhatNh>();
            }
            catch (Exception ex)
            {
                // Log exception if needed
                // _logger.LogError(ex, "Error fetching doctor availability data");
                return new List<DoctorPhatNh>();
            }
        }
        public async Task<bool> DeleteDoctorAvailabilityPhatNh(int id)
        {
            try
            {
                #region GraphQL Request
                var graphQLRequest = new GraphQLRequest
                {
                    Query = @"mutation deleteDoctorAvailabilityPhatNh($input: Int!) {
                deleteDoctorAvailabilityPhatNh (id: $input)
            }",
                    Variables = new { input = id } // Đổi thành input để khớp với $input
                };
                #endregion
                var response = await _graphQLClient.SendMutationAsync<DeleteDoctorAvailabilityPhatNhdResponse>(graphQLRequest);
                var result = response.Data.deleteDoctorAvailabilityPhatNh;
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }

}
