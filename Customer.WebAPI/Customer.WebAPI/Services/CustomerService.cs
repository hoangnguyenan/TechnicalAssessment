using AutoMapper;
using Customer.WebAPI.Configurations;
using Customer.WebAPI.Constants;
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

        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IConfiguration configuration, IMapper mapper,
            ICustomerRepository customerRepository,
            ILogger<CustomerService> logger)
        {
            _configuration = configuration;

            _mapper = mapper;

            _customerRepository = customerRepository;

            _logger = logger;
        }

        public async Task SaveCustomer(CustomerRequestDto request)
        {
            _logger.LogInformation("Start Save Customer");

            FileHelper.ValidateFileType(request.File.ContentType);

            string uploadedFileName = Guid.NewGuid().ToString() + request.File.FileName;

            string filePath = GetFilePath(uploadedFileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                _logger.LogInformation("Save file {0} to server successfully", request.File.FileName);

                var newCustomer = _mapper.Map<CustomerConfiguration>(request);

                newCustomer.FileName = uploadedFileName;

                _logger.LogInformation("Start insert new Customer");

                await _customerRepository.InsertAsync(newCustomer);

                _logger.LogInformation("End Save Customer");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error when saving customer - Ex: {0}", ex.Message);

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
            _logger.LogInformation("Start GetRequestByFileName");

            if (fileName == null)
            {
                _logger.LogInformation("Invalid file name- fileName: {0}", fileName);

                throw new ArgumentNullException($"File name not found - {fileName}");
            }

            var result = await _customerRepository.GetCustomerByFileNameAsync(fileName);

            if (result == null)
            {
                _logger.LogInformation("File name cound not be found - fileName: {0}", fileName);

                throw new ArgumentNullException($"The customer with file name: {fileName} cound not be found.");
            }

            string filePath = GetFilePath(fileName);

            FileHelper.CheckExistFile(filePath);

            _logger.LogInformation("End GetRequestByFileName");

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

        public string GetFilePathDownloadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                _logger.LogInformation("Invalid file name- fileName: {0}", fileName);

                throw new ArgumentNullException($"Invalid file name- fileName: {fileName}");
            }

            var filePath = GetFilePath(fileName);

            FileHelper.CheckExistFile(filePath);

            return filePath;
        }

        public string GetContentType(string fileName)
        {
            if (Path.GetExtension(fileName).Equals(CommonConstant.PdfExtension, StringComparison.OrdinalIgnoreCase))
            {
                return CommonConstant.PdfFileType;
            }
            else
            {
                return CommonConstant.ZipFileType;
            }
        }
    }
}