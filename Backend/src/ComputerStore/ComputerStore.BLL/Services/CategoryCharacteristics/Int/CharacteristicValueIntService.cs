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
    public class CharacteristicValueIntService : ICharacteristicValueIntService
    {
        private readonly IGenericRepository<CharacteristicValueIntEntity> _repository;
        private readonly IValidator<CharacteristicValueIntDto> _validator;

        public CharacteristicValueIntService(
            IGenericRepository<CharacteristicValueIntEntity> repository,
            IValidator<CharacteristicValueIntDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<CharacteristicValueIntDto>> GetAllCharacteristicValuesIntByCategoryCharacteristicIntIdAsync(int categoryCharacteristicIntId)
        {
            var entitiesResult = await _repository.GetAll()
                .Where(cv => cv.CategoryCharacteristicIntId == categoryCharacteristicIntId)
                .OrderBy(cv => cv.ValueInt)
                .ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CharacteristicValueInt " +
                                                 $"with CategoryCharacteristicIntId {categoryCharacteristicIntId} in Database");
            }

            return entitiesResult.Adapt<IEnumerable<CharacteristicValueIntDto>>();
        }

        public async Task<IEnumerable<CharacteristicValueIntDto>> GetAllAsync()
        {
            var entitiesResult = await _repository.GetAll()
                .OrderBy(cv => cv.ValueInt)
                .ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CharacteristicValueInt entities in Database");
            }

            return entitiesResult.Adapt<IEnumerable<CharacteristicValueIntDto>>();
        }

        public async Task<CharacteristicValueIntDto> GetByIdAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CharacteristicValueInt " +
                                                 $"with Id {itemId} not found in Database");
            }

            return entityResult.Adapt<CharacteristicValueIntDto>();
        }

        public async Task<CharacteristicValueIntDto> GetByValueIntAndCharacteristicIdAsync(int value, int id)
        {
            var entityResult = await _repository.GetAll()
                .FirstOrDefaultAsync(x =>
                    x.ValueInt == value &&
                    x.CategoryCharacteristicIntId == id);

            return entityResult?.Adapt<CharacteristicValueIntDto>();
        }

        public async Task<CharacteristicValueIntDto> CreateAsync(CharacteristicValueIntDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entityForCreate = item.Adapt<CharacteristicValueIntEntity>();

            var createdEntity = await _repository.CreateAsync(entityForCreate);

            item.Id = createdEntity.Id;

            return item;
        }

        public async Task UpdateAsync(CharacteristicValueIntDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entityForUpdate = item.Adapt<CharacteristicValueIntEntity>();

            await _repository.UpdateAsync(entityForUpdate);
        }

        public async Task DeleteAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CharacteristicValueInt entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            await _repository.DeleteAsync(itemId);
        }
    }
}
