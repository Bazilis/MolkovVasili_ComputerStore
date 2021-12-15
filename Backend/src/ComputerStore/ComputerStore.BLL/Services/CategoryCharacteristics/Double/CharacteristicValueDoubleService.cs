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
    public class CharacteristicValueDoubleService : ICharacteristicValueDoubleService
    {
        private readonly IGenericRepository<CharacteristicValueDoubleEntity> _repository;
        private readonly IValidator<CharacteristicValueDoubleDto> _validator;

        public CharacteristicValueDoubleService(
            IGenericRepository<CharacteristicValueDoubleEntity> repository,
            IValidator<CharacteristicValueDoubleDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<CharacteristicValueDoubleDto>> GetAllCharacteristicValuesDoubleByCategoryCharacteristicDoubleIdAsync(int categoryCharacteristicDoubleId)
        {
            var entitiesResult = await _repository.GetAll()
                .Where(cv => cv.CategoryCharacteristicDoubleId == categoryCharacteristicDoubleId)
                .OrderBy(cv => cv.ValueDouble)
                .ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CharacteristicValueDouble " +
                                                 $"with CategoryCharacteristicDoubleId {categoryCharacteristicDoubleId} in Database");
            }

            return entitiesResult.Adapt<IEnumerable<CharacteristicValueDoubleDto>>();
        }

        public async Task<IEnumerable<CharacteristicValueDoubleDto>> GetAllAsync()
        {
            var entitiesResult = await _repository.GetAll()
                .OrderBy(cv => cv.ValueDouble)
                .ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CharacteristicValueDouble entities in Database");
            }

            return entitiesResult.Adapt<IEnumerable<CharacteristicValueDoubleDto>>();
        }

        public async Task<CharacteristicValueDoubleDto> GetByIdAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CharacteristicValueDouble " +
                                                 $"with Id {itemId} not found in Database");
            }

            var productDto = new CharacteristicValueDoubleDto
            {
                Id = entityResult.Id,
                ValueDouble = entityResult.ValueDouble,
                CategoryCharacteristicDoubleId = entityResult.CategoryCharacteristicDoubleId
            };

            foreach (var productEntity in entityResult.Products)
            {
                productDto.ProductIds.Add(productEntity.Id);
            }

            return productDto;
        }

        public async Task<CharacteristicValueDoubleDto> GetByValueDoubleAndCharacteristicIdAsync(double value, int id)
        {
            var entityResult = await _repository.GetAll()
                .FirstOrDefaultAsync(x =>
                    //Math.Abs(x.ValueDouble - value) <= 0.001 &&
                    (x.ValueDouble > value ? x.ValueDouble - value : value - x.ValueDouble) <= 0.001 &&
                    x.CategoryCharacteristicDoubleId == id);

            return entityResult?.Adapt<CharacteristicValueDoubleDto>();
        }

        public async Task<CharacteristicValueDoubleDto> CreateAsync(CharacteristicValueDoubleDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entityForCreate = item.Adapt<CharacteristicValueDoubleEntity>();

            var createdEntity = await _repository.CreateAsync(entityForCreate);

            item.Id = createdEntity.Id;

            return item;
        }

        public async Task UpdateAsync(CharacteristicValueDoubleDto item)
        {
            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entityForUpdate = item.Adapt<CharacteristicValueDoubleEntity>();

            await _repository.UpdateAsync(entityForUpdate);
        }

        public async Task DeleteAsync(int itemId)
        {
            var entityResult = await _repository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CharacteristicValueDouble entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            await _repository.DeleteAsync(itemId);
        }
    }
}
