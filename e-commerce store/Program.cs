using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using e_commerce_store.Data;
using e_commerce_store.Interfaces;
using e_commerce_store.Repositories;
using e_commerce_store.Services;
using e_commerce_store.Exceptions;
using Azure.Identity;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration["KeyVault:Endpoint"]), new DefaultAzureCredential());
var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
    new AzureServiceTokenProvider().KeyVaultTokenCallback));

string secretValue = keyVaultClient.GetSecretAsync(builder.Configuration["KeyVault:Endpoint"].ToString(),
    builder.Configuration["KeyVault:SecretName"]).Result.Value;

builder.Services.AddDbContext<EcommerceStoreDbContext>(options => options.UseSqlServer(secretValue));
builder.Services.AddApplicationInsightsTelemetry();

//var connectionString = builder.Configuration.GetConnectionString("constring");
//Console.WriteLine($"Retrieved connection string: {connectionString}");

//builder.Services.AddDbContext<EcommerceStoreDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
//        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

builder.Services.AddMemoryCache();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddMvcCore(options =>
{
    options.Filters.Add(typeof(CustomExceptionFilter));
})
    .AddApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "e-commerce store API2", Version = "v1" });
    c.DescribeAllParametersInCamelCase();

});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseExceptionHandler("/error");
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
