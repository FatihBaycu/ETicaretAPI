using ETicaretAPI.Application.Abstraction;
using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Abstraction.Services.Authentications;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.Customers;
using ETicaretAPI.Application.Repositories.Files;
using ETicaretAPI.Application.Repositories.InvoiceFiles;
using ETicaretAPI.Application.Repositories.Orders;
using ETicaretAPI.Application.Repositories.ProductImageFiles;
using ETicaretAPI.Application.Repositories.Products;
using ETicaretAPI.Domain.Entities.Identity;
using ETicaretAPI.Persistence.Concretes;
using ETicaretAPI.Persistence.Contexts;
using ETicaretAPI.Persistence.Repositories;
using ETicaretAPI.Persistence.Repositories.CustomerRepo;
using ETicaretAPI.Persistence.Repositories.Files;
using ETicaretAPI.Persistence.Repositories.InvoiceFiles;
using ETicaretAPI.Persistence.Repositories.OrderRepo;
using ETicaretAPI.Persistence.Repositories.ProductImageFiles;
using ETicaretAPI.Persistence.Repositories.ProductRepo;
using ETicaretAPI.Persistence.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services) {         

            services.AddSingleton<IProductService, ProductService>();


            services.AddDbContext<ETicaretAPIDbContext>(options => options.UseSqlServer
                (Configuration.ConnectionString));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            }
                ).AddEntityFrameworkStores<ETicaretAPIDbContext>();

            services.AddScoped<ICustomerReadRepository,CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>();

            services.AddScoped<IOrderReadRepository,OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository,OrderWriteRepository>();

            services.AddScoped<IProductReadRepository,ProductReadRepository>();
            services.AddScoped<IProductWriteRepository,ProductWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository,ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository,InvoiceFileWriteRepository>();

            services.AddScoped<IFileReadRepository,FileReadRepository>();
            services.AddScoped<IFileWriteRepository,FileWriteRepository>();

            services.AddScoped<IUserService,UserService>();

            //services.AddHttpClient();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();






        }
    }
}
