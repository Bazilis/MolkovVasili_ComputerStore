using ComputerStore.BLL.Interfaces;
using ComputerStore.BLL.Interfaces.CategoryCharacteristics.Double;
using ComputerStore.BLL.Interfaces.CategoryCharacteristics.Int;
using ComputerStore.BLL.Interfaces.CategoryCharacteristics.String;
using ComputerStore.BLL.Models;
using ComputerStore.BLL.Models.CategoryCharacteristics.Double;
using ComputerStore.BLL.Models.CategoryCharacteristics.Int;
using ComputerStore.BLL.Models.CategoryCharacteristics.String;
using ComputerStore.BLL.Services;
using ComputerStore.BLL.Services.CategoryCharacteristics.Double;
using ComputerStore.BLL.Services.CategoryCharacteristics.Int;
using ComputerStore.BLL.Services.CategoryCharacteristics.String;
using ComputerStore.BLL.Validators;
using ComputerStore.BLL.Validators.CategoryCharacteristics.Double;
using ComputerStore.BLL.Validators.CategoryCharacteristics.Int;
using ComputerStore.BLL.Validators.CategoryCharacteristics.String;
using ComputerStore.DAL.EF;
using ComputerStore.DAL.Entities;
using ComputerStore.DAL.Interfaces;
using ComputerStore.DAL.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerStore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ComputerStore.WebApi", Version = "v1" });
            });

            services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICharacteristicValueDoubleRepository, CharacteristicValueDoubleRepository>();
            services.AddScoped<ICharacteristicValueIntRepository, CharacteristicValueIntRepository>();
            services.AddScoped<ICharacteristicValueStringRepository, CharacteristicValueStringRepository>();
            services.AddScoped<ICategoryCharacteristicDoubleService, CategoryCharacteristicDoubleService>();
            services.AddScoped<ICategoryCharacteristicIntService, CategoryCharacteristicIntService>();
            services.AddScoped<ICategoryCharacteristicStringService, CategoryCharacteristicStringService>();
            services.AddScoped<ICharacteristicValueDoubleService, CharacteristicValueDoubleService>();
            services.AddScoped<ICharacteristicValueIntService, CharacteristicValueIntService>();
            services.AddScoped<ICharacteristicValueStringService, CharacteristicValueStringService>();

            services.AddScoped<IValidator<CategoryCharacteristicDoubleDto>, CategoryCharacteristicDoubleValidator>();
            services.AddScoped<IValidator<CategoryCharacteristicIntDto>, CategoryCharacteristicIntValidator>();
            services.AddScoped<IValidator<CategoryCharacteristicStringDto>, CategoryCharacteristicStringValidator>();
            services.AddScoped<IValidator<CharacteristicValueDoubleDto>, CharacteristicValueDoubleValidator>();
            services.AddScoped<IValidator<CharacteristicValueIntDto>, CharacteristicValueIntValidator>();
            services.AddScoped<IValidator<CharacteristicValueStringDto>, CharacteristicValueStringValidator>();
            services.AddScoped<IValidator<ProductDto>, ProductValidator>();
            services.AddScoped<IProductService, ProductService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComputerStore.WebApi v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
