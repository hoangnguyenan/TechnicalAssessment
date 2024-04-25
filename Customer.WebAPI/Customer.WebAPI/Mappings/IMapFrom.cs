
using AutoMapper;

namespace Customer.WebAPI.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }

    public interface IMapFrom
    {
        void Mapping(Profile profile);
    }
}