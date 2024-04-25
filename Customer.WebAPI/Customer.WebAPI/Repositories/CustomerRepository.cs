using Microsoft.EntityFrameworkCore;
using Customer.WebAPI.Configurations;
using Customer.WebAPI.DbContexts;
using Customer.WebAPI.Dtos;

namespace Customer.WebAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _databaseContext;

        public CustomerRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task InsertAsync(CustomerConfiguration customer)
        {
            await _databaseContext.CustomerRequests.AddAsync(customer);

            await _databaseContext.SaveChangesAsync();
        }

        public async Task<PageResultDto<CustomerConfiguration>> GetCustomerPagingAsync(QuerySearchDefault param)
        {
            var query = _databaseContext.CustomerRequests.AsQueryable();

            if (!string.IsNullOrWhiteSpace(param.SearchKey))
            {
                _databaseContext.CustomerRequests.Where(x => x.OutletName.Equals(param.SearchKey.Trim()));
            }

            if (string.IsNullOrWhiteSpace(param.SortField))
            {
                query = query.OrderByDescending(x => x.OutletName).ThenBy(x => x.PhoneNumber);
            }

            int totalRecord = await query.CountAsync();

            var result = await query.Skip(param.GetSkip()).Take(param.GetTake()).ToListAsync();

            return new PageResultDto<CustomerConfiguration>(totalRecord, param.GetTake(), result);
        }

        public async Task<CustomerConfiguration> GetCustomerByFileNameAsync(string fileName)
        {
            return await _databaseContext.CustomerRequests.FirstOrDefaultAsync(x => x.FileName == fileName);
        }
    }
}