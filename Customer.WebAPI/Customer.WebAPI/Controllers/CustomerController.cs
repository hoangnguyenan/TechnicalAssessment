using Microsoft.AspNetCore.Mvc;
using Customer.WebAPI.Constants;
using Customer.WebAPI.Dtos;
using Customer.WebAPI.Helpers;
using Customer.WebAPI.Services;

namespace Customer.WebAPI.Controllers
{
    [Route("api/customer")]
    public class CustomerController : ApiBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerPagedAsync([FromQuery] QuerySearchDefault param)
        {
            return Ok(await _customerService.SearchAsync(param));
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Add([FromForm] CustomerRequestDto request)
        {
            Validate(request);

            await _customerService.SaveCustomer(request);

            return Ok("Request submitted successfully.");
        }

        [HttpGet("fileName")]
        public async Task<IActionResult> GetRequestByName(string fileName)
        {
            return Ok(await _customerService.GetRequestByFileName(fileName));
        }

        [HttpGet("download")]
        public IActionResult DownloadFile(string fileName)
        {
            var filePath = _customerService.GetFilePath(fileName);

            return PhysicalFile(filePath, CommonConstant.PdfFileType, fileName);
        }
    }

}
