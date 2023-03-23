

namespace e_commerce_store.DependencyInjection
{
    public static class ServiceRegistrationFilter
    {
        public static void AddMyServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IMembershipService, MembershipService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IOrderProductRepository, OrderProductRepository>();
            services.AddScoped<IOrderProductService, OrderProductService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
