using Microsoft.AspNetCore.Mvc;

namespace e_commerce_store.DependencyInjection
{
    public static class ServiceRegistrationFilter
    {
        public static void AddDependencies(this IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<MyDbContext>(options =>
            //    options.UseSqlServer(connectionString));

            //services.AddControllers();

            //services.AddAutoMapper(typeof(Startup));

            //services.AddScoped<ICustomerRepository, CustomerRepository>();
            //services.AddScoped<ICustomerService, CustomerService>();

            //services.AddScoped<IMembershipRepository, MembershipRepository>();
            //services.AddScoped<IMembershipService, MembershipService>();

            //services.AddScoped<ICustomerMembershipRepository, CustomerMembershipRepository>();
            //services.AddScoped<ICustomerMembershipService, CustomerMembershipService>();

            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IProductService, ProductService>();

            //services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<IOrderService, OrderService>();

            //services.AddScoped<IOrderProductRepository, OrderProductRepository>();
            //services.AddScoped<IOrderProductService, OrderProductService>();

            //services.AddScoped<CustomExceptionFilter>();
        }
    }
}
