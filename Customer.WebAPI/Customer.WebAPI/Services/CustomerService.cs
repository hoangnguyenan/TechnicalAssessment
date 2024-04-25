using AutoMapper;
using Customer.WebAPI.Configurations;
using Customer.WebAPI.Dtos;
using Customer.WebAPI.Helpers;
using Customer.WebAPI.Repositories;

namespace Customer.WebAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IConfiguration configuration, IMapper mapper,
            ICustomerRepository customerRepository)
        {
            _configuration = configuration;

            _mapper = mapper;

            _customerRepository = customerRepository;
        }

        public async Task SaveCustomer(CustomerRequestDto request)
        {
            FileHelper.ValidateFileType(request.File.ContentType);

            string uploadedFileName = Guid.NewGuid().ToString() + request.File.FileName;

            string filePath = GetFilePath(uploadedFileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                var newCustomer = _mapper.Map<CustomerConfiguration>(request);

                newCustomer.FileName = uploadedFileName;

                await _customerRepository.InsertAsync(newCustomer);
            }
            catch (Exception ex)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                throw new ArgumentException($"Save customer Error - Ex: {ex.Message}");
            }
        }

        public async Task<PageResultDto<CustomerConfiguration>> SearchAsync(QuerySearchDefault param)
        {
            return await _customerRepository.GetCustomerPagingAsync(param);
        }

        public async Task<CustomerConfiguration> GetRequestByFileName(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException($"File name not found - {fileName}");
            }

            var result = await _customerRepository.GetCustomerByFileNameAsync(fileName);

            if (result == null)
            {
                throw new ArgumentNullException($"The customer with file name: {fileName} cound not be found.");
            }

            string filePath = GetFilePath(fileName);

            FileHelper.CheckExistFile(filePath);

            return new CustomerConfiguration
            {
                OutletName = result.OutletName,
                PhoneNumber = result.PhoneNumber,
                FileName = fileName
            };
        }

        public string GetFilePath(string fileName)
        {
            return Path.Combine(_configuration["SharedFolder"], fileName);
        }
    }
}