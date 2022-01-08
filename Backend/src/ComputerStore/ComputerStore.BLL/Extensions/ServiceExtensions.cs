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
using ComputerStore.DAL.Interfaces;
using ComputerStore.DAL.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerStore.BLL.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddBllServices(this IServiceCollection services)
        {
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
    }
}
