using AutoMapper;
using e_commerce_store.Dto;
using e_commerce_store.Models;

namespace e_commerce_store.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();
        }
    }

}
