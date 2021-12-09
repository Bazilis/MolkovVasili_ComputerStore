using ComputerStore.BLL.Interfaces.CategoryCharacteristics.String;
using ComputerStore.BLL.Models.CategoryCharacteristics.String;
using ComputerStore.DAL.Entities.CategoryCharacteristics.String;
using ComputerStore.DAL.Interfaces;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Services.CategoryCharacteristics.String
{
    public class CategoryCharacteristicStringService : ICategoryCharacteristicStringService
    {
        private readonly IGenericRepository<CategoryCharacteristicStringEntity> _repository;
        private readonly IValidator<CategoryCharacteristicStringDto> _validator;

        public CategoryCharacteristicStringService(
            IGenericRepository<CategoryCharacteristicStringEntity> repository,
            IValidator<CategoryCharacteristicStringDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<List<CategoryCharacteristicStringDto>> GetAllCategoryCharacteristicsStringByProductCategoryIdAsync(int productCategoryId)
        {
            var entitiesResult = await _repository.GetAll()
                .Where(ch => ch.ProductCategoryId == productCategoryId)
                .OrderBy(ch => ch.Name)
                .ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CategoryCharacteristicString entities " +
                                                 $"with ProductCategoryId {productCategoryId} in Database");
            }

            return entitiesResult.Adapt<List<CategoryCharacteristicStringDto>>();
        }

        public async Task<List<CategoryCharacteristicStringDto>> GetAllAsync()
        {
            var entitiesResult = await _repository.GetAll().ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CategoryCharacteristicString entities in Database");
            }

            return entitiesResult.Adapt<List<CategoryCharacteristicStringDto>>();
        }

        public async Task<CategoryCharacteristicStringDto> GetByIdAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CategoryCharacteristicString entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            return entityResult.Adapt<CategoryCharacteristicStringDto>();
        }

        public async Task<CategoryCharacteristicStringDto> CreateAsync(CategoryCharacteristicStringDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entityForCreate = item.Adapt<CategoryCharacteristicStringEntity>();

            var createdEntity = await _repository.CreateAsync(entityForCreate);

            item.Id = createdEntity.Id;

            return item;
        }

        public async Task UpdateAsync(CategoryCharacteristicStringDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entityForUpdate = item.Adapt<CategoryCharacteristicStringEntity>();

            await _repository.UpdateAsync(entityForUpdate);
        }

        public async Task DeleteAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CategoryCharacteristicString entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            await _repository.DeleteAsync(itemId);
        }
    }
}
