
using System.ComponentModel.DataAnnotations;

namespace Customer.WebAPI.Configurations
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public virtual string Id { get; set; }
    }
}