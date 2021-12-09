using ComputerStore.BLL.Interfaces.CategoryCharacteristics.Double;
using ComputerStore.BLL.Models.CategoryCharacteristics.Double;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Double;
using ComputerStore.DAL.Interfaces;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Services.CategoryCharacteristics.Double
{
    public class CategoryCharacteristicDoubleService : ICategoryCharacteristicDoubleService
    {
        private readonly IGenericRepository<CategoryCharacteristicDoubleEntity> _repository;
        private readonly IValidator<CategoryCharacteristicDoubleDto> _validator;

        public CategoryCharacteristicDoubleService(
            IGenericRepository<CategoryCharacteristicDoubleEntity> repository,
            IValidator<CategoryCharacteristicDoubleDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<List<CategoryCharacteristicDoubleDto>> GetAllCategoryCharacteristicsDoubleByProductCategoryIdAsync(int productCategoryId)
        {
            var entitiesResult = await _repository.GetAll()
                .Where(ch => ch.ProductCategoryId == productCategoryId)
                .OrderBy(ch => ch.Name)
                .ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CategoryCharacteristicDouble entities " +
                                                 $"with ProductCategoryId {productCategoryId} in Database");
            }

            return entitiesResult.Adapt<List<CategoryCharacteristicDoubleDto>>();
        }

        public async Task<List<CategoryCharacteristicDoubleDto>> GetAllAsync()
        {
            var entitiesResult = await _repository.GetAll().ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CategoryCharacteristicDouble entities in Database");
            }

            return entitiesResult.Adapt<List<CategoryCharacteristicDoubleDto>>();
        }

        public async Task<CategoryCharacteristicDoubleDto> GetByIdAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CategoryCharacteristicDouble entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            return entityResult.Adapt<CategoryCharacteristicDoubleDto>();
        }

        public async Task<CategoryCharacteristicDoubleDto> CreateAsync(CategoryCharacteristicDoubleDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entityForCreate = item.Adapt<CategoryCharacteristicDoubleEntity>();

            var createdEntity = await _repository.CreateAsync(entityForCreate);

            item.Id = createdEntity.Id;

            return item;
        }

        public async Task UpdateAsync(CategoryCharacteristicDoubleDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entityForUpdate = item.Adapt<CategoryCharacteristicDoubleEntity>();

            await _repository.UpdateAsync(entityForUpdate);
        }

        public async Task DeleteAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CategoryCharacteristicDouble entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            await _repository.DeleteAsync(itemId);
        }
    }
}
