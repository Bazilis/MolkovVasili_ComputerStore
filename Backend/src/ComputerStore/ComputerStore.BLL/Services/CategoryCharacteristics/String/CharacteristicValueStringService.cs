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
    public class CharacteristicValueStringService : ICharacteristicValueStringService
    {
        private readonly IGenericRepository<CharacteristicValueStringEntity> _repository;
        private readonly IValidator<CharacteristicValueStringDto> _validator;

        public CharacteristicValueStringService(
            IGenericRepository<CharacteristicValueStringEntity> repository,
            IValidator<CharacteristicValueStringDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<CharacteristicValueStringDto>> GetAllCharacteristicValuesStringByCategoryCharacteristicStringIdAsync(int categoryCharacteristicStringId)
        {
            var entitiesResult = await _repository.GetAll()
                .Where(cv => cv.CategoryCharacteristicStringId == categoryCharacteristicStringId)
                .OrderBy(cv => cv.ValueString)
                .Select(cv => cv.ValueString)
                .ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CharacteristicValueString " +
                                                 $"with CategoryCharacteristicStringId {categoryCharacteristicStringId} in Database");
            }

            return entitiesResult.Adapt<IEnumerable<CharacteristicValueStringDto>>();
        }

        public async Task<IEnumerable<CharacteristicValueStringDto>> GetAllAsync()
        {
            var entitiesResult = await _repository.GetAll()
                .OrderBy(cv => cv.ValueString)
                .ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CharacteristicValueString entities in Database");
            }

            return entitiesResult.Adapt<IEnumerable<CharacteristicValueStringDto>>();
        }

        public async Task<CharacteristicValueStringDto> GetByIdAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CharacteristicValueString " +
                                                 $"with Id {itemId} not found in Database");
            }

            return entityResult.Adapt<CharacteristicValueStringDto>();
        }

        public async Task<CharacteristicValueStringDto> GetByValueStringAndCharacteristicIdAsync(string value, int id)
        {
            var entityResult = await _repository.GetAll()
                .FirstOrDefaultAsync(x =>
                    x.ValueString == value &&
                    x.CategoryCharacteristicStringId == id);

            return entityResult?.Adapt<CharacteristicValueStringDto>();
        }

        public async Task<CharacteristicValueStringDto> CreateAsync(CharacteristicValueStringDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entityForCreate = item.Adapt<CharacteristicValueStringEntity>();

            var createdEntity = await _repository.CreateAsync(entityForCreate);

            item.Id = createdEntity.Id;

            return item;
        }

        public async Task UpdateAsync(CharacteristicValueStringDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entityForUpdate = item.Adapt<CharacteristicValueStringEntity>();

            await _repository.UpdateAsync(entityForUpdate);
        }

        public async Task DeleteAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CharacteristicValueString entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            await _repository.DeleteAsync(itemId);
        }
    }
}
