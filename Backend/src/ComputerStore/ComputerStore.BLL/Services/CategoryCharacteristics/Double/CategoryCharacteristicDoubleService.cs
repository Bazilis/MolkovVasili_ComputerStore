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
        private readonly IGenericRepository<CharacteristicValueDoubleEntity> _valueRepository;
        private readonly IGenericRepository<CategoryCharacteristicDoubleEntity> _characteristicRepository;
        private readonly IValidator<CategoryCharacteristicDoubleDto> _characteristicValidator;

        public CategoryCharacteristicDoubleService(
            IGenericRepository<CharacteristicValueDoubleEntity> valueRepository,
            IGenericRepository<CategoryCharacteristicDoubleEntity> repository,
            IValidator<CategoryCharacteristicDoubleDto> validator)
        {
            _valueRepository = valueRepository;
            _characteristicRepository = repository;
            _characteristicValidator = validator;
        }

        public async Task<IEnumerable<CategoryCharacteristicDoubleListDto>> GetAllCategoryCharacteristicsDoubleListByProductCategoryIdAsync(int productCategoryId)
        {
            var categoryCharacteristicDoubleEntitiesList = await _characteristicRepository.GetAll()
                .Where(ch => ch.ProductCategoryId == productCategoryId)
                .OrderBy(ch => ch.Name)
                .ToListAsync();

            if (categoryCharacteristicDoubleEntitiesList == null)
            {
                throw new NullReferenceException("There is no CategoryCharacteristicDouble entities " +
                                                 $"with ProductCategoryId {productCategoryId} in Database");
            }

            var categoryCharacteristicDoubleListDtoList = new List<CategoryCharacteristicDoubleListDto>();

            foreach (var categoryCharacteristicDoubleEntity in categoryCharacteristicDoubleEntitiesList)
            {
                var characteristicValueDoubleEntitiesList = await _valueRepository.GetAll()
                    .Where(cv => cv.CategoryCharacteristicDoubleId == categoryCharacteristicDoubleEntity.Id)
                    .OrderBy(cv => cv.ValueDouble)
                    .ToListAsync();

                if (characteristicValueDoubleEntitiesList == null)
                {
                    throw new NullReferenceException("There is no CharacteristicValueDouble " +
                                                     $"with CategoryCharacteristicDoubleId {categoryCharacteristicDoubleEntity.Id} in Database");
                }

                var characteristicValueDoubleDtoList = new List<CharacteristicValueDoubleDto>();

                foreach (var entityResult in characteristicValueDoubleEntitiesList)
                {
                    var characteristicValueDoubleDto = new CharacteristicValueDoubleDto
                    {
                        Id = entityResult.Id,
                        ValueDouble = entityResult.ValueDouble,
                        CategoryCharacteristicDoubleId = entityResult.CategoryCharacteristicDoubleId
                    };

                    foreach (var productEntity in entityResult.Products)
                    {
                        characteristicValueDoubleDto.ProductIds.Add(productEntity.Id);
                    }

                    characteristicValueDoubleDtoList.Add(characteristicValueDoubleDto);
                }

                var categoryCharacteristicDoubleListDto = new CategoryCharacteristicDoubleListDto()
                {
                    Id = categoryCharacteristicDoubleEntity.Id,
                    Name = categoryCharacteristicDoubleEntity.Name,
                    Dimension = categoryCharacteristicDoubleEntity.Dimension,
                    ProductCategoryId = categoryCharacteristicDoubleEntity.ProductCategoryId,
                    CharacteristicValuesDouble = characteristicValueDoubleDtoList
                };

                categoryCharacteristicDoubleListDtoList.Add(categoryCharacteristicDoubleListDto);
            }

            return categoryCharacteristicDoubleListDtoList;
        }

        public async Task<IEnumerable<CategoryCharacteristicDoubleDto>> GetAllCategoryCharacteristicsDoubleByProductCategoryIdAsync(int productCategoryId)
        {
            var entitiesResult = await _characteristicRepository.GetAll()
                .Where(ch => ch.ProductCategoryId == productCategoryId)
                .OrderBy(ch => ch.Name)
                .ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CategoryCharacteristicDouble entities " +
                                                 $"with ProductCategoryId {productCategoryId} in Database");
            }

            return entitiesResult.Adapt<IEnumerable<CategoryCharacteristicDoubleDto>>();
        }

        public async Task<IEnumerable<CategoryCharacteristicDoubleDto>> GetAllAsync()
        {
            var entitiesResult = await _characteristicRepository.GetAll().ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CategoryCharacteristicDouble entities in Database");
            }

            return entitiesResult.Adapt<IEnumerable<CategoryCharacteristicDoubleDto>>();
        }

        public async Task<CategoryCharacteristicDoubleDto> GetByIdAsync(int itemId)
        {
            var entityResult = await _characteristicRepository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CategoryCharacteristicDouble entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            return entityResult.Adapt<CategoryCharacteristicDoubleDto>();
        }

        public async Task<CategoryCharacteristicDoubleDto> CreateAsync(CategoryCharacteristicDoubleDto item)
        {
            var validationResult = await _characteristicValidator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entityForCreate = item.Adapt<CategoryCharacteristicDoubleEntity>();

            var createdEntity = await _characteristicRepository.CreateAsync(entityForCreate);

            item.Id = createdEntity.Id;

            return item;
        }

        public async Task UpdateAsync(CategoryCharacteristicDoubleDto item)
        {
            var validationResult = await _characteristicValidator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entityForUpdate = item.Adapt<CategoryCharacteristicDoubleEntity>();

            await _characteristicRepository.UpdateAsync(entityForUpdate);
        }

        public async Task DeleteAsync(int itemId)
        {
            var entityResult = await _characteristicRepository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CategoryCharacteristicDouble entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            await _characteristicRepository.DeleteAsync(itemId);
        }
    }
}
