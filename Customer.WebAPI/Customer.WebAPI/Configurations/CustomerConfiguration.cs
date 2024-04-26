
using System.ComponentModel.DataAnnotations;

namespace Customer.WebAPI.Configurations
{
    public class CustomerConfiguration : EntityBase
    {
        [Required]
        public string OutletName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string FileName { get; set; }
    }
}