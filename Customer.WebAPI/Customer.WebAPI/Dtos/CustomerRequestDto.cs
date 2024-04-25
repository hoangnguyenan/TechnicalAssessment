
using AutoMapper;
using FluentValidation;
using Customer.WebAPI.Configurations;
using Customer.WebAPI.Constants;
using Customer.WebAPI.Mappings;

namespace Customer.WebAPI.Dtos
{
    public class CustomerRequestDto : IMapFrom
    {
        public string OutletName { get; set; }

        public string PhoneNumber { get; set; }

        public IFormFile File { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerRequestDto, CustomerConfiguration>();
        }
    }

    public class CustomerRequestDtoValidator : AbstractValidator<CustomerRequestDto>
    {
        public CustomerRequestDtoValidator()
        {
            RuleFor(x => x.OutletName)
                .NotEmpty().WithMessage(CustomerConstants.OutletNameRequiredErrorMessage);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(CustomerConstants.PhoneNumberRequiredErrorMessage)
                .Length(10).WithMessage(CustomerConstants.PhoneNumberInValidLengthErrorMessage)
                .Matches("^[0-9]*$").WithMessage(CustomerConstants.PhoneNumberInValidErrorMessage);

            RuleFor(x => x.File)
                .NotNull().WithMessage(CustomerConstants.FileRequiredErrorMessage)
                .Must(file => file.Length < 20 * 1024 * 1024).WithMessage(CustomerConstants.FileLengthErrorMessage);
        }
    }
}