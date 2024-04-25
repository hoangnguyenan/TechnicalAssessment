
using Customer.WebAPI.Configurations;
using Customer.WebAPI.Dtos;

namespace Customer.WebAPI.Services
{
    public interface ICustomerService
    {
        Task SaveCustomer(CustomerRequestDto request);

        Task<PageResultDto<CustomerConfiguration>> SearchAsync(QuerySearchDefault param);
        
        Task<CustomerConfiguration> GetRequestByFileName(string fileName);

        string GetFilePath(string fileName);
    }
}