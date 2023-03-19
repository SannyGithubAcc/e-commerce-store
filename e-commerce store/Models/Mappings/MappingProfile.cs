using AutoMapper;
using e_commerce_store.Dto;
using e_commerce_store.Dto.Membership;
using e_commerce_store.Models;
using e_commerce_store.Models.DBModels;
using e_commerce_store.Models.Dto.Order;
using e_commerce_store.Models.Dto.ProductDto;

namespace e_commerce_store.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();

            CreateMap<Membership, MembershipDto>();
            CreateMap<CreateMembershipDto, Membership>();
            CreateMap<UpdateMembershipDto, Membership>();

            CreateMap<Product, OrderDto>();
            CreateMap<CreateUpdateProductDto, Product>();

            CreateMap<Order,OrderDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<UpdateOrderDto, Order>();

        }

    }

}
