using Customer.WebAPI.Configurations;
using Customer.WebAPI.Dtos;

namespace Customer.WebAPI.Repositories
{
    public interface ICustomerRepository
    {
        Task InsertAsync(CustomerConfiguration customer);

        Task<PageResultDto<CustomerConfiguration>> GetCustomerPagingAsync(QuerySearchDefault param);

        Task<CustomerConfiguration> GetCustomerByFileNameAsync(string fileName);
    }
}