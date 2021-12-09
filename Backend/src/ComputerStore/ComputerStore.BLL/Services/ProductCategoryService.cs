using ComputerStore.BLL.Interfaces;
using ComputerStore.BLL.Models;
using ComputerStore.DAL.Entities;
using ComputerStore.DAL.Interfaces;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IGenericRepository<ProductCategoryEntity> _repository;
        private readonly IValidator<ProductCategoryDto> _validator;

        public ProductCategoryService(
            IGenericRepository<ProductCategoryEntity> repository,
            IValidator<ProductCategoryDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetAllAsync()
        {
            var entitiesResult = await _repository.GetAll().ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no ProductCategory entities in Database");
            }

            return entitiesResult.Adapt<IEnumerable<ProductCategoryDto>>();
        }

        public async Task<ProductCategoryDto> GetByIdAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("ProductCategory entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            return entityResult.Adapt<ProductCategoryDto>();
        }

        public async Task<ProductCategoryDto> CreateAsync(ProductCategoryDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entityForCreate = item.Adapt<ProductCategoryEntity>();

            var createdEntity = await _repository.CreateAsync(entityForCreate);

            item.Id = createdEntity.Id;

            return item;
        }

        public async Task UpdateAsync(ProductCategoryDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entityForUpdate = item.Adapt<ProductCategoryEntity>();

            await _repository.UpdateAsync(entityForUpdate);
        }

        public async Task DeleteAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("ProductCategory entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            await _repository.DeleteAsync(itemId);
        }
    }
}
