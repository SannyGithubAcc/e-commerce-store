using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using e_commerce_store.Data;
using e_commerce_store.Exceptions;
using Azure.Identity;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Identity.Web;
using System.Net.Http.Headers;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;
using e_commerce_store.Filters;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Configure authentication
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

//builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration["KeyVault:Endpoint"]), new DefaultAzureCredential());
//var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
//    new AzureServiceTokenProvider().KeyVaultTokenCallback));

//string secretValue = keyVaultClient.GetSecretAsync(builder.Configuration["KeyVault:Endpoint"].ToString(),
//    builder.Configuration["KeyVault:SecretName"]).Result.Value;

//builder.Services.AddDbContext<EcommerceStoreDbContext>(options => options.UseSqlServer(secretValue));
//builder.Services.AddApplicationInsightsTelemetry();

//var connectionString = builder.Configuration.GetConnectionString("constring");
//Console.WriteLine($"Retrieved connection string: {connectionString}");

builder.Services.AddDbContext<EcommerceStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

builder.Services.AddMemoryCache();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
builder.Services.AddScoped<IMembershipService, MembershipService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddScoped<IOrderProductService, OrderProductService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddMvcCore(options =>
{
    options.Filters.Add(typeof(CustomExceptionFilter));
})
    .AddApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "E-commerce API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "E-commerce API V1");
    });

}
else
{
    app.UseExceptionHandler("/error");
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.Use(async (context, next) =>
{
    var token = config.GetValue<string>("Token");
    var middleware = new TokenAuthenticationMiddleware(next, token);
    await middleware.Invoke(context);
});
app.MapControllers();
app.Run();



