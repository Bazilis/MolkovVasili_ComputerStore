using ComputerStore.BLL.Interfaces.CategoryCharacteristics.Double;
using ComputerStore.BLL.Interfaces.CategoryCharacteristics.Int;
using ComputerStore.BLL.Interfaces.CategoryCharacteristics.String;
using ComputerStore.BLL.Models;
using ComputerStore.BLL.Models.CategoryCharacteristics.Double;
using ComputerStore.BLL.Models.CategoryCharacteristics.Int;
using ComputerStore.BLL.Models.CategoryCharacteristics.String;
using ComputerStore.DAL.Entities;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Double;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Int;
using ComputerStore.DAL.Entities.CategoryCharacteristics.String;
using ComputerStore.DAL.Interfaces;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Services
{
    public class ProductService
    {
        private readonly ICategoryCharacteristicDoubleService _categoryCharacteristicDoubleService;
        private readonly ICharacteristicValueDoubleService _characteristicValueDoubleService;
        private readonly ICategoryCharacteristicIntService _categoryCharacteristicIntService;
        private readonly ICharacteristicValueIntService _characteristicValueIntService;
        private readonly ICategoryCharacteristicStringService _categoryCharacteristicStringService;
        private readonly ICharacteristicValueStringService _characteristicValueStringService;

        private readonly IGenericRepository<ProductEntity> _productRepository;
        private readonly IValidator<ProductDto> _productValidator;

        public ProductService(ICategoryCharacteristicDoubleService categoryCharacteristicDoubleService,
            ICharacteristicValueDoubleService characteristicValueDoubleService,
            ICategoryCharacteristicIntService categoryCharacteristicIntService,
            ICharacteristicValueIntService characteristicValueIntService,
            ICategoryCharacteristicStringService categoryCharacteristicStringService,
            ICharacteristicValueStringService characteristicValueStringService,
            IGenericRepository<ProductEntity> productRepository,
            IValidator<ProductDto> productValidator)
        {
            _categoryCharacteristicDoubleService = categoryCharacteristicDoubleService;
            _characteristicValueDoubleService = characteristicValueDoubleService;
            _categoryCharacteristicIntService = categoryCharacteristicIntService;
            _characteristicValueIntService = characteristicValueIntService;
            _categoryCharacteristicStringService = categoryCharacteristicStringService;
            _characteristicValueStringService = characteristicValueStringService;
            _productRepository = productRepository;
            _productValidator = productValidator;
        }

        public async Task<List<ProductDto>> GetAllProductsByProductCategoryIdAsync(int productCategoryId)
        {
            var entitiesResult = await _productRepository.GetAll()
                .Where(ch => ch.ProductCategoryId == productCategoryId)
                .OrderBy(ch => ch.Name)
                .ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CategoryCharacteristicDouble entities " +
                                                 $"with ProductCategoryId {productCategoryId} in Database");
            }

            return entitiesResult.Adapt<List<ProductDto>>();
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var entitiesResult = await _productRepository.GetAll().ToListAsync();

            if (entitiesResult == null)
            {
                throw new NullReferenceException("There is no CategoryCharacteristicDouble entities in Database");
            }

            return entitiesResult.Adapt<IEnumerable<ProductDto>>();
        }

        public async Task<ProductDto> GetByIdAsync(int itemId)
        {
            ProductEntity productEntity = await _productRepository.GetByIdAsync(itemId);

            if (productEntity == null)
            {
                throw new NullReferenceException($"Product entity with Id {itemId} not found in Database");
            }

            var productDto = new ProductDto
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Price = productEntity.Price,
                QuantityInStorage = productEntity.QuantityInStorage,
                ProductCategoryId = productEntity.ProductCategoryId
            };

            var categoryCharacteristicDoubleDtoList =
                await _categoryCharacteristicDoubleService.GetAllCategoryCharacteristicsDoubleByProductCategoryIdAsync(
                    productEntity.ProductCategoryId);

            foreach (var characteristicValueDoubleEntity in productEntity.CategoryCharacteristicsDouble)
            {
                var categoryCharacteristicDoubleDto = categoryCharacteristicDoubleDtoList
                    .Find(x =>x.Id == characteristicValueDoubleEntity.CategoryCharacteristicDoubleId);

                if (categoryCharacteristicDoubleDto == null)
                {
                    throw new NullReferenceException("CategoryCharacteristicDouble with Id " +
                                                     $"{characteristicValueDoubleEntity.CategoryCharacteristicDoubleId} not found in Database");
                }

                var productCharacteristicDoubleDto = categoryCharacteristicDoubleDto.Adapt<ProductCharacteristicDoubleDto>();

                productCharacteristicDoubleDto.CharacteristicValueDouble =
                    characteristicValueDoubleEntity.ValueDouble;

                productDto.ProductCharacteristicsDouble.Add(productCharacteristicDoubleDto);

                productDto.Description += productCharacteristicDoubleDto.Name +
                                          productCharacteristicDoubleDto.CharacteristicValueDouble +
                                          productCharacteristicDoubleDto.Dimension;
            }

            var categoryCharacteristicIntDtoList =
                await _categoryCharacteristicIntService.GetAllCategoryCharacteristicsIntByProductCategoryIdAsync(
                    productEntity.ProductCategoryId);

            foreach (var characteristicValueIntEntity in productEntity.CategoryCharacteristicsInt)
            {
                var categoryCharacteristicIntDto = categoryCharacteristicIntDtoList
                    .Find(x => x.Id == characteristicValueIntEntity.CategoryCharacteristicIntId);

                if (categoryCharacteristicIntDto == null)
                {
                    throw new NullReferenceException("CategoryCharacteristicInt with Id " +
                                                     $"{characteristicValueIntEntity.CategoryCharacteristicIntId} not found in Database");
                }

                var productCharacteristicIntDto = categoryCharacteristicIntDto.Adapt<ProductCharacteristicIntDto>();

                productCharacteristicIntDto.CharacteristicValueInt =
                    characteristicValueIntEntity.ValueInt;

                productDto.ProductCharacteristicsInt.Add(productCharacteristicIntDto);

                productDto.Description += productCharacteristicIntDto.Name +
                                          productCharacteristicIntDto.CharacteristicValueInt +
                                          productCharacteristicIntDto.Dimension;
            }

            var categoryCharacteristicStringDtoList =
                await _categoryCharacteristicStringService.GetAllCategoryCharacteristicsStringByProductCategoryIdAsync(
                    productEntity.ProductCategoryId);

            foreach (var characteristicValueStringEntity in productEntity.CategoryCharacteristicsString)
            {
                var categoryCharacteristicStringDto = categoryCharacteristicStringDtoList
                    .Find(x => x.Id == characteristicValueStringEntity.CategoryCharacteristicStringId);

                if (categoryCharacteristicStringDto == null)
                {
                    throw new NullReferenceException("CategoryCharacteristicString with Id " +
                                                     $"{characteristicValueStringEntity.CategoryCharacteristicStringId} not found in Database");
                }

                var productCharacteristicStringDto = categoryCharacteristicStringDto.Adapt<ProductCharacteristicStringDto>();

                productCharacteristicStringDto.CharacteristicValueString =
                    characteristicValueStringEntity.ValueString;

                productDto.ProductCharacteristicsString.Add(productCharacteristicStringDto);

                productDto.Description += productCharacteristicStringDto.Name +
                                          productCharacteristicStringDto.CharacteristicValueString +
                                          productCharacteristicStringDto.Dimension;
            }

            return productDto;
        }

        public async Task<ProductDto> CreateAsync(ProductDto item)
        {
            var validationResult = await _productValidator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var productEntity = new ProductEntity
            {
                Name = item.Name,
                Price = item.Price,
                QuantityInStorage = item.QuantityInStorage,
                ProductCategoryId = item.ProductCategoryId
            };

            var categoryCharacteristicDoubleDtoList =
                await _categoryCharacteristicDoubleService.GetAllCategoryCharacteristicsDoubleByProductCategoryIdAsync(
                    productEntity.ProductCategoryId);

            foreach (var productCharacteristicDoubleDto in item.ProductCharacteristicsDouble)
            {
                if (!categoryCharacteristicDoubleDtoList.Exists(x => x.Id == productCharacteristicDoubleDto.Id))
                {
                    var categoryCharacteristicDoubleDto = productCharacteristicDoubleDto.Adapt<CategoryCharacteristicDoubleDto>();

                    var categoryWithUpdatedId = await _categoryCharacteristicDoubleService.CreateAsync(categoryCharacteristicDoubleDto);

                    var characteristicValueDoubleDto = new CharacteristicValueDoubleDto
                    {
                        ValueDouble = productCharacteristicDoubleDto.CharacteristicValueDouble,
                        CategoryCharacteristicDoubleId = categoryWithUpdatedId.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueDoubleService.CreateAsync(characteristicValueDoubleDto);

                    var characteristicValueDoubleEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueDoubleEntity>();

                    productEntity.CategoryCharacteristicsDouble.Add(characteristicValueDoubleEntity);
                }

                var characteristicValueDoubleList =
                    await _characteristicValueDoubleService
                        .GetAllCharacteristicValuesDoubleByCategoryCharacteristicDoubleIdAsync(productCharacteristicDoubleDto.Id);

                if (!characteristicValueDoubleList.Exists(x =>
                    x.ValueDouble == productCharacteristicDoubleDto.CharacteristicValueDouble))
                {
                    var characteristicValueDoubleDto = new CharacteristicValueDoubleDto
                    {
                        ValueDouble = productCharacteristicDoubleDto.CharacteristicValueDouble,
                        CategoryCharacteristicDoubleId = productCharacteristicDoubleDto.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueDoubleService.CreateAsync(characteristicValueDoubleDto);

                    var characteristicValueDoubleEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueDoubleEntity>();

                    productEntity.CategoryCharacteristicsDouble.Add(characteristicValueDoubleEntity);
                }
            }

            var categoryCharacteristicIntDtoList =
                await _categoryCharacteristicIntService.GetAllCategoryCharacteristicsIntByProductCategoryIdAsync(
                    productEntity.ProductCategoryId);

            foreach (var productCharacteristicIntDto in item.ProductCharacteristicsInt)
            {
                if (!categoryCharacteristicIntDtoList.Exists(x => x.Id == productCharacteristicIntDto.Id))
                {
                    var categoryCharacteristicIntDto = productCharacteristicIntDto.Adapt<CategoryCharacteristicIntDto>();

                    var categoryWithUpdatedId = await _categoryCharacteristicIntService.CreateAsync(categoryCharacteristicIntDto);

                    var characteristicValueIntDto = new CharacteristicValueIntDto
                    {
                        ValueInt = productCharacteristicIntDto.CharacteristicValueInt,
                        CategoryCharacteristicIntId = categoryWithUpdatedId.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueIntService.CreateAsync(characteristicValueIntDto);

                    var characteristicValueIntEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueIntEntity>();

                    productEntity.CategoryCharacteristicsInt.Add(characteristicValueIntEntity);
                }

                var characteristicValueIntList =
                    await _characteristicValueIntService
                        .GetAllCharacteristicValuesIntByCategoryCharacteristicIntIdAsync(productCharacteristicIntDto.Id);

                if (!characteristicValueIntList.Exists(x =>
                    x.ValueInt == productCharacteristicIntDto.CharacteristicValueInt))
                {
                    var characteristicValueIntDto2 = new CharacteristicValueIntDto
                    {
                        ValueInt = productCharacteristicIntDto.CharacteristicValueInt,
                        CategoryCharacteristicIntId = productCharacteristicIntDto.Id
                    };

                    var characteristicWithUpdatedId2 = await _characteristicValueIntService.CreateAsync(characteristicValueIntDto2);

                    var characteristicValueIntEntity2 = characteristicWithUpdatedId2.Adapt<CharacteristicValueIntEntity>();

                    productEntity.CategoryCharacteristicsInt.Add(characteristicValueIntEntity2);
                }
            }

            var categoryCharacteristicStringDtoList =
                await _categoryCharacteristicStringService.GetAllCategoryCharacteristicsStringByProductCategoryIdAsync(
                    productEntity.ProductCategoryId);

            foreach (var productCharacteristicStringDto in item.ProductCharacteristicsString)
            {
                if (!categoryCharacteristicStringDtoList.Exists(x => x.Id == productCharacteristicStringDto.Id))
                {
                    var categoryCharacteristicStringDto = productCharacteristicStringDto.Adapt<CategoryCharacteristicStringDto>();

                    var categoryWithUpdatedId = await _categoryCharacteristicStringService.CreateAsync(categoryCharacteristicStringDto);

                    var characteristicValueStringDto = new CharacteristicValueStringDto
                    {
                        ValueString = productCharacteristicStringDto.CharacteristicValueString,
                        CategoryCharacteristicStringId = categoryWithUpdatedId.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueStringService.CreateAsync(characteristicValueStringDto);

                    var characteristicValueStringEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueStringEntity>();

                    productEntity.CategoryCharacteristicsString.Add(characteristicValueStringEntity);
                }

                var characteristicValueStringList =
                    await _characteristicValueStringService
                        .GetAllCharacteristicValuesStringByCategoryCharacteristicStringIdAsync(productCharacteristicStringDto.Id);

                if (!characteristicValueStringList.Exists(x =>
                    x.ValueString == productCharacteristicStringDto.CharacteristicValueString))
                {
                    var characteristicValueStringDto = new CharacteristicValueStringDto
                    {
                        ValueString = productCharacteristicStringDto.CharacteristicValueString,
                        CategoryCharacteristicStringId = productCharacteristicStringDto.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueStringService.CreateAsync(characteristicValueStringDto);

                    var characteristicValueStringEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueStringEntity>();

                    productEntity.CategoryCharacteristicsString.Add(characteristicValueStringEntity);
                }
            }

            var productEntityWithUpdatedId = await _productRepository.CreateAsync(productEntity);

            item.Id = productEntityWithUpdatedId.Id;

            return item;
        }

        public async Task UpdateAsync(ProductDto item)
        {
            var validationResult = await _productValidator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var productEntity = new ProductEntity
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                QuantityInStorage = item.QuantityInStorage,
                ProductCategoryId = item.ProductCategoryId
            };

            foreach (var productCharacteristicDoubleDto in item.ProductCharacteristicsDouble)
            {
                var characteristicValueDoubleList =
                    await _characteristicValueDoubleService
                        .GetAllCharacteristicValuesDoubleByCategoryCharacteristicDoubleIdAsync(productCharacteristicDoubleDto.Id);

                if (!characteristicValueDoubleList.Exists(x =>
                    x.ValueDouble == productCharacteristicDoubleDto.CharacteristicValueDouble))
                {
                    var characteristicValueDoubleDto = new CharacteristicValueDoubleDto
                    {
                        ValueDouble = productCharacteristicDoubleDto.CharacteristicValueDouble,
                        CategoryCharacteristicDoubleId = productCharacteristicDoubleDto.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueDoubleService.CreateAsync(characteristicValueDoubleDto);

                    var characteristicValueDoubleEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueDoubleEntity>();

                    productEntity.CategoryCharacteristicsDouble.Add(characteristicValueDoubleEntity);
                }
            }

            //var entityForUpdate = item.Adapt<ProductEntity>();

            //await _productRepository.UpdateAsync(entityForUpdate);
        }

        public async Task DeleteAsync(int itemId)
        {
            var entityResult = await _productRepository.GetByIdAsync(itemId);

            if (entityResult == null)
            {
                throw new NullReferenceException("CategoryCharacteristicDouble entity " +
                                                 $"with Id {itemId} not found in Database");
            }

            await _productRepository.DeleteAsync(itemId);
        }
    }
}
