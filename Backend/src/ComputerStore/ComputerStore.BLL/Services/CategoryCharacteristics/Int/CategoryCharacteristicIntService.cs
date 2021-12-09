using ComputerStore.BLL.Interfaces.CategoryCharacteristics.Int;
using ComputerStore.BLL.Models.CategoryCharacteristics.Int;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Int;
using ComputerStore.DAL.Interfaces;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Services.CategoryCharacteristics.Int
{
    public class CategoryCharacteristicIntService : ICategoryCharacteristicIntService
    {
        private readonly IGenericRepository<CategoryCharacteristicIntEntity> _repository;
        private readonly IValidator<CategoryCharacteristicIntDto> _validator;

        public CategoryCharacteristicIntService(
            IGenericRepository<CategoryCharacteristicIntEntity> repository,
            IValidator<CategoryCharacteristicIntDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<CategoryCharacteristicIntDto>> GetAllCategoryCharacteristicsIntByProductCategoryIdAsync(int productCategoryId)
        {
            var entitiesResult = await _repository.GetAll()
                .Where(ch => ch.ProductCategoryId == productCategoryId)
                .OrderBy(ch => ch.Name)
                .ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CategoryCharacteristicInt entities " +
                                                 $"with ProductCategoryId {productCategoryId} in Database");
            }

            return entitiesResult.Adapt<IEnumerable<CategoryCharacteristicIntDto>>();
        }

        public async Task<IEnumerable<CategoryCharacteristicIntDto>> GetAllAsync()
        {
            var entitiesResult = await _repository.GetAll().ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CategoryCharacteristicInt entities in Database");
            }

            return entitiesResult.Adapt<IEnumerable<CategoryCharacteristicIntDto>>();
        }

        public async Task<CategoryCharacteristicIntDto> GetByIdAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CategoryCharacteristicInt entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            return entityResult.Adapt<CategoryCharacteristicIntDto>();
        }

        public async Task<CategoryCharacteristicIntDto> CreateAsync(CategoryCharacteristicIntDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entityForCreate = item.Adapt<CategoryCharacteristicIntEntity>();

            var createdEntity = await _repository.CreateAsync(entityForCreate);

            item.Id = createdEntity.Id;

            return item;
        }

        public async Task UpdateAsync(CategoryCharacteristicIntDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entityForUpdate = item.Adapt<CategoryCharacteristicIntEntity>();

            await _repository.UpdateAsync(entityForUpdate);
        }

        public async Task DeleteAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CategoryCharacteristicInt entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            await _repository.DeleteAsync(itemId);
        }
    }
}
