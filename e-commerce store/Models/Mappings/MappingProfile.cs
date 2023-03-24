using AutoMapper;
using e_commerce_store.Dto;
using e_commerce_store.Dto.Membership;
using e_commerce_store.Models;
using e_commerce_store.Models.DBModels;
using e_commerce_store.Models.Dto.Order;
using e_commerce_store.Models.Dto.ProductDto;

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
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();

        CreateMap<Order, OrderDto>();
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();

        CreateMap<OrderProduct, OrderProductDto>();
        CreateMap<CreateOrderProductDto, OrderProduct>();
        CreateMap<UpdateOrderProductDto, OrderProduct>();

    }
}
