using AutoMapper;
using e_commerce_store.Dto;
using e_commerce_store.Dto.Membership;
using e_commerce_store.Models;
using e_commerce_store.Models.DBModels;
using e_commerce_store.Models.Dto.Order;
using e_commerce_store.Models.Dto.OrderProduct;
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

            CreateMap<Product, ProductDto>();
            CreateMap<CreateUpdateProductDto, Product>();

            CreateMap<Order,OrderDto>();
            CreateMap<CreateOrderDto, Order>()
            .ForMember(dest => dest.Customer_ID, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
             .IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<UpdateOrderDto, Order>();

            CreateMap<OrderProduct, OrderProductDto>();
            CreateMap<OrderProductDto, OrderProduct>()
           .ForMember(dest => dest.Product_ID, opt => opt.MapFrom(src => src.Product_ID))
           .ForMember(dest => dest.MembershipName, opt => opt.MapFrom(src => src.MembershipName))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price));
            CreateMap<UpdateOrderProductDto, OrderProduct>();

        }

    }

}
