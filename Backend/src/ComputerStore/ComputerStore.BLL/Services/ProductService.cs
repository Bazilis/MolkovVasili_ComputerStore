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

            foreach (var characteristicValueDoubleEntity in productEntity.CategoryCharacteristicsDouble)
            {
                var categoryCharacteristicDoubleDto = await _categoryCharacteristicDoubleService
                    .GetByIdAsync(characteristicValueDoubleEntity.CategoryCharacteristicDoubleId);

                var productCharacteristicDoubleDto = categoryCharacteristicDoubleDto.Adapt<ProductCharacteristicDoubleDto>();

                productCharacteristicDoubleDto.CharacteristicValueDouble =
                    characteristicValueDoubleEntity.ValueDouble;

                productDto.ProductCharacteristicsDouble.Add(productCharacteristicDoubleDto);

                productDto.Description += productCharacteristicDoubleDto.Name +
                                          productCharacteristicDoubleDto.CharacteristicValueDouble +
                                          productCharacteristicDoubleDto.Dimension;
            }

            foreach (var characteristicValueIntEntity in productEntity.CategoryCharacteristicsInt)
            {
                var categoryCharacteristicIntDto = await _categoryCharacteristicIntService
                    .GetByIdAsync(characteristicValueIntEntity.CategoryCharacteristicIntId);

                var productCharacteristicIntDto = categoryCharacteristicIntDto.Adapt<ProductCharacteristicIntDto>();

                productCharacteristicIntDto.CharacteristicValueInt =
                    characteristicValueIntEntity.ValueInt;

                productDto.ProductCharacteristicsInt.Add(productCharacteristicIntDto);

                productDto.Description += productCharacteristicIntDto.Name +
                                          productCharacteristicIntDto.CharacteristicValueInt +
                                          productCharacteristicIntDto.Dimension;
            }

            foreach (var characteristicValueStringEntity in productEntity.CategoryCharacteristicsString)
            {
                var categoryCharacteristicStringDto = await _categoryCharacteristicStringService
                    .GetByIdAsync(characteristicValueStringEntity.CategoryCharacteristicStringId);

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

            foreach (var productCharacteristicDoubleDto in item.ProductCharacteristicsDouble)
            {
                var characteristicValueDoubleDto =
                    await _characteristicValueDoubleService
                        .GetByValueDoubleAndCharacteristicIdAsync(
                            productCharacteristicDoubleDto.CharacteristicValueDouble,
                            productCharacteristicDoubleDto.Id);

                if (characteristicValueDoubleDto == null)
                {
                    var characteristicValueDoubleDtoNew = new CharacteristicValueDoubleDto
                    {
                        ValueDouble = productCharacteristicDoubleDto.CharacteristicValueDouble,
                        CategoryCharacteristicDoubleId = productCharacteristicDoubleDto.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueDoubleService
                        .CreateAsync(characteristicValueDoubleDtoNew);

                    var characteristicValueDoubleEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueDoubleEntity>();

                    productEntity.CategoryCharacteristicsDouble.Add(characteristicValueDoubleEntity);
                }

                var characteristicValueDoubleEntity2 = characteristicValueDoubleDto.Adapt<CharacteristicValueDoubleEntity>();

                productEntity.CategoryCharacteristicsDouble.Add(characteristicValueDoubleEntity2);
            }

            foreach (var productCharacteristicIntDto in item.ProductCharacteristicsInt)
            {
                var characteristicValueIntDto =
                    await _characteristicValueIntService
                        .GetByValueIntAndCharacteristicIdAsync(
                            productCharacteristicIntDto.CharacteristicValueInt,
                            productCharacteristicIntDto.Id);

                if (characteristicValueIntDto == null)
                {
                    var characteristicValueIntDtoNew = new CharacteristicValueIntDto
                    {
                        ValueInt = productCharacteristicIntDto.CharacteristicValueInt,
                        CategoryCharacteristicIntId = productCharacteristicIntDto.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueIntService
                        .CreateAsync(characteristicValueIntDtoNew);

                    var characteristicValueIntEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueIntEntity>();

                    productEntity.CategoryCharacteristicsInt.Add(characteristicValueIntEntity);
                }

                var characteristicValueIntEntity2 = characteristicValueIntDto.Adapt<CharacteristicValueIntEntity>();

                productEntity.CategoryCharacteristicsInt.Add(characteristicValueIntEntity2);
            }

            foreach (var productCharacteristicStringDto in item.ProductCharacteristicsString)
            {
                var characteristicValueStringDto =
                    await _characteristicValueStringService
                        .GetByValueStringAndCharacteristicIdAsync(
                            productCharacteristicStringDto.CharacteristicValueString,
                            productCharacteristicStringDto.Id);

                if (characteristicValueStringDto == null)
                {
                    var characteristicValueStringDtoNew = new CharacteristicValueStringDto
                    {
                        ValueString = productCharacteristicStringDto.CharacteristicValueString,
                        CategoryCharacteristicStringId = productCharacteristicStringDto.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueStringService
                        .CreateAsync(characteristicValueStringDtoNew);

                    var characteristicValueStringEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueStringEntity>();

                    productEntity.CategoryCharacteristicsString.Add(characteristicValueStringEntity);
                }

                var characteristicValueStringEntity2 = characteristicValueStringDto.Adapt<CharacteristicValueStringEntity>();

                productEntity.CategoryCharacteristicsString.Add(characteristicValueStringEntity2);
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

            ProductEntity productEntity = await _productRepository.GetByIdAsync(item.Id);

            if (productEntity == null)
            {
                throw new NullReferenceException($"Product entity with Id {item.Id} not found in Database");
            }

            productEntity.Name = item.Name;
            productEntity.Price = item.Price;
            productEntity.QuantityInStorage = item.QuantityInStorage;
            productEntity.ProductCategoryId = item.ProductCategoryId;

            foreach (var productCharacteristicDoubleDto in item.ProductCharacteristicsDouble)
            {
                var characteristicValueDoubleDto =
                    await _characteristicValueDoubleService
                        .GetByValueDoubleAndCharacteristicIdAsync(
                            productCharacteristicDoubleDto.CharacteristicValueDouble,
                            productCharacteristicDoubleDto.Id);

                if (characteristicValueDoubleDto == null)
                {
                    var characteristicValueDoubleDtoNew = new CharacteristicValueDoubleDto
                    {
                        ValueDouble = productCharacteristicDoubleDto.CharacteristicValueDouble,
                        CategoryCharacteristicDoubleId = productCharacteristicDoubleDto.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueDoubleService
                        .CreateAsync(characteristicValueDoubleDtoNew);

                    var characteristicValueDoubleEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueDoubleEntity>();

                    productEntity.CategoryCharacteristicsDouble.Add(characteristicValueDoubleEntity);
                }

                var characteristicValueDoubleEntity2 = characteristicValueDoubleDto.Adapt<CharacteristicValueDoubleEntity>();

                productEntity.CategoryCharacteristicsDouble.Add(characteristicValueDoubleEntity2);
            }

            foreach (var productCharacteristicIntDto in item.ProductCharacteristicsInt)
            {
                var characteristicValueIntDto =
                    await _characteristicValueIntService
                        .GetByValueIntAndCharacteristicIdAsync(
                            productCharacteristicIntDto.CharacteristicValueInt,
                            productCharacteristicIntDto.Id);

                if (characteristicValueIntDto == null)
                {
                    var characteristicValueIntDtoNew = new CharacteristicValueIntDto
                    {
                        ValueInt = productCharacteristicIntDto.CharacteristicValueInt,
                        CategoryCharacteristicIntId = productCharacteristicIntDto.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueIntService
                        .CreateAsync(characteristicValueIntDtoNew);

                    var characteristicValueIntEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueIntEntity>();

                    productEntity.CategoryCharacteristicsInt.Add(characteristicValueIntEntity);
                }

                var characteristicValueIntEntity2 = characteristicValueIntDto.Adapt<CharacteristicValueIntEntity>();

                productEntity.CategoryCharacteristicsInt.Add(characteristicValueIntEntity2);
            }

            foreach (var productCharacteristicStringDto in item.ProductCharacteristicsString)
            {
                var characteristicValueStringDto =
                    await _characteristicValueStringService
                        .GetByValueStringAndCharacteristicIdAsync(
                            productCharacteristicStringDto.CharacteristicValueString,
                            productCharacteristicStringDto.Id);

                if (characteristicValueStringDto == null)
                {
                    var characteristicValueStringDtoNew = new CharacteristicValueStringDto
                    {
                        ValueString = productCharacteristicStringDto.CharacteristicValueString,
                        CategoryCharacteristicStringId = productCharacteristicStringDto.Id
                    };

                    var characteristicWithUpdatedId = await _characteristicValueStringService
                        .CreateAsync(characteristicValueStringDtoNew);

                    var characteristicValueStringEntity = characteristicWithUpdatedId.Adapt<CharacteristicValueStringEntity>();

                    productEntity.CategoryCharacteristicsString.Add(characteristicValueStringEntity);
                }

                var characteristicValueStringEntity2 = characteristicValueStringDto.Adapt<CharacteristicValueStringEntity>();

                productEntity.CategoryCharacteristicsString.Add(characteristicValueStringEntity2);
            }

            await _productRepository.UpdateAsync(productEntity);
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
