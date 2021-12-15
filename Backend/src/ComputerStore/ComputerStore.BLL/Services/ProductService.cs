using ComputerStore.BLL.Interfaces;
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
    public class ProductService : IProductService
    {
        private readonly ICategoryCharacteristicDoubleService _categoryCharacteristicDoubleService;
        private readonly ICategoryCharacteristicIntService _categoryCharacteristicIntService;
        private readonly ICategoryCharacteristicStringService _categoryCharacteristicStringService;

        private readonly ICharacteristicValueDoubleRepository _characteristicValueDoubleRepository;
        private readonly ICharacteristicValueIntRepository _characteristicValueIntRepository;
        private readonly ICharacteristicValueStringRepository _characteristicValueStringRepository;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<ProductDto> _productValidator;

        public ProductService(ICategoryCharacteristicDoubleService categoryCharacteristicDoubleService,
            ICategoryCharacteristicIntService categoryCharacteristicIntService,
            ICategoryCharacteristicStringService categoryCharacteristicStringService,
            IProductRepository productRepository,
            IValidator<ProductDto> productValidator,
            ICharacteristicValueDoubleRepository characteristicValueDoubleRepository,
            ICharacteristicValueIntRepository characteristicValueIntRepository,
            ICharacteristicValueStringRepository characteristicValueStringRepository)
        {
            _categoryCharacteristicDoubleService = categoryCharacteristicDoubleService;
            _categoryCharacteristicIntService = categoryCharacteristicIntService;
            _categoryCharacteristicStringService = categoryCharacteristicStringService;
            _productRepository = productRepository;
            _productValidator = productValidator;
            _characteristicValueDoubleRepository = characteristicValueDoubleRepository;
            _characteristicValueIntRepository = characteristicValueIntRepository;
            _characteristicValueStringRepository = characteristicValueStringRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsByProductCategoryIdAsync(int productCategoryId)
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

            return entitiesResult.Adapt<IEnumerable<ProductDto>>();
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

        public async Task<ProductDto> GetByIdWithAllCharacteristicsAsync(int itemId)
        {
            ProductEntity productEntity = await _productRepository.GetByIdWithAllCharacteristicsAsync(itemId);

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

            productEntity = await _productRepository.CreateAsync(productEntity);

            item.Id = productEntity.Id;

            foreach (var productCharacteristicDoubleDto in item.ProductCharacteristicsDouble)
            {
                var characteristicValueDoubleEntity = await _characteristicValueDoubleRepository
                    .GetByValueDoubleAndCharacteristicIdAsync(
                        productCharacteristicDoubleDto.CharacteristicValueDouble,
                        productCharacteristicDoubleDto.Id);

                if (characteristicValueDoubleEntity == null)
                {
                    var characteristicValueDoubleEntityNew = new CharacteristicValueDoubleEntity
                    {
                        ValueDouble = productCharacteristicDoubleDto.CharacteristicValueDouble,
                        CategoryCharacteristicDoubleId = productCharacteristicDoubleDto.Id
                    };

                    characteristicValueDoubleEntityNew.Products.Add(productEntity);

                    await _characteristicValueDoubleRepository
                        .CreateAsync(characteristicValueDoubleEntityNew);

                    continue;
                }

                characteristicValueDoubleEntity.Products.Add(productEntity);
                await _characteristicValueDoubleRepository.UpdateNoTrackingAsync(characteristicValueDoubleEntity);
            }

            foreach (var productCharacteristicIntDto in item.ProductCharacteristicsInt)
            {
                var characteristicValueIntEntity =
                    await _characteristicValueIntRepository.GetByValueIntAndCharacteristicIdAsync(
                        productCharacteristicIntDto.CharacteristicValueInt,
                        productCharacteristicIntDto.Id);

                if (characteristicValueIntEntity == null)
                {
                    var characteristicValueIntEntityNew = new CharacteristicValueIntEntity
                    {
                        ValueInt = productCharacteristicIntDto.CharacteristicValueInt,
                        CategoryCharacteristicIntId = productCharacteristicIntDto.Id
                    };

                    characteristicValueIntEntityNew.Products.Add(productEntity);

                    await _characteristicValueIntRepository
                        .CreateAsync(characteristicValueIntEntityNew);

                    continue;
                }

                characteristicValueIntEntity.Products.Add(productEntity);
                await _characteristicValueIntRepository.UpdateNoTrackingAsync(characteristicValueIntEntity);
            }

            foreach (var productCharacteristicStringDto in item.ProductCharacteristicsString)
            {
                var characteristicValueStringEntity =
                    await _characteristicValueStringRepository.GetByValueStringAndCharacteristicIdAsync(
                        productCharacteristicStringDto.CharacteristicValueString,
                        productCharacteristicStringDto.Id);

                if (characteristicValueStringEntity == null)
                {
                    var characteristicValueStringEntityNew = new CharacteristicValueStringEntity
                    {
                        ValueString = productCharacteristicStringDto.CharacteristicValueString,
                        CategoryCharacteristicStringId = productCharacteristicStringDto.Id
                    };

                    characteristicValueStringEntityNew.Products.Add(productEntity);

                    await _characteristicValueStringRepository
                        .CreateAsync(characteristicValueStringEntityNew);

                    continue;
                }

                characteristicValueStringEntity.Products.Add(productEntity);
                await _characteristicValueStringRepository.UpdateNoTrackingAsync(characteristicValueStringEntity);
            }

            return item;
        }

        public async Task UpdateAsync(ProductDto item)
        {
            var validationResult = await _productValidator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            ProductEntity productEntity = await _productRepository.GetByIdWithAllCharacteristicsAsync(item.Id);

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
                var characteristicValueDoubleEntity = await _characteristicValueDoubleRepository
                    .GetByValueDoubleAndCharacteristicIdAsync(
                        productCharacteristicDoubleDto.CharacteristicValueDouble,
                        productCharacteristicDoubleDto.Id);

                if (characteristicValueDoubleEntity == null)
                {
                    var characteristicValueDoubleEntityNew = new CharacteristicValueDoubleEntity
                    {
                        ValueDouble = productCharacteristicDoubleDto.CharacteristicValueDouble,
                        CategoryCharacteristicDoubleId = productCharacteristicDoubleDto.Id
                    };

                    characteristicValueDoubleEntityNew.Products.Add(productEntity);

                    await _characteristicValueDoubleRepository
                        .CreateAsync(characteristicValueDoubleEntityNew);

                    continue;
                }

                if (productEntity.CategoryCharacteristicsDouble
                    .All(x => x.Id != characteristicValueDoubleEntity.Id))
                {
                    characteristicValueDoubleEntity.Products.Add(productEntity);
                    await _characteristicValueDoubleRepository.UpdateNoTrackingAsync(characteristicValueDoubleEntity);
                }
            }

            foreach (var productCharacteristicIntDto in item.ProductCharacteristicsInt)
            {
                var characteristicValueIntEntity = await _characteristicValueIntRepository
                    .GetByValueIntAndCharacteristicIdAsync(
                        productCharacteristicIntDto.CharacteristicValueInt,
                        productCharacteristicIntDto.Id);

                if (characteristicValueIntEntity == null)
                {
                    var characteristicValueIntEntityNew = new CharacteristicValueIntEntity
                    {
                        ValueInt = productCharacteristicIntDto.CharacteristicValueInt,
                        CategoryCharacteristicIntId = productCharacteristicIntDto.Id
                    };

                    characteristicValueIntEntityNew.Products.Add(productEntity);

                    await _characteristicValueIntRepository
                        .CreateAsync(characteristicValueIntEntityNew);

                    continue;
                }

                if (productEntity.CategoryCharacteristicsInt
                    .All(x => x.Id != characteristicValueIntEntity.Id))
                {
                    characteristicValueIntEntity.Products.Add(productEntity);
                    await _characteristicValueIntRepository.UpdateNoTrackingAsync(characteristicValueIntEntity);
                }
            }

            foreach (var productCharacteristicStringDto in item.ProductCharacteristicsString)
            {
                var characteristicValueStringEntity = await _characteristicValueStringRepository
                    .GetByValueStringAndCharacteristicIdAsync(
                        productCharacteristicStringDto.CharacteristicValueString,
                        productCharacteristicStringDto.Id);

                if (characteristicValueStringEntity == null)
                {
                    var characteristicValueStringEntityNew = new CharacteristicValueStringEntity
                    {
                        ValueString = productCharacteristicStringDto.CharacteristicValueString,
                        CategoryCharacteristicStringId = productCharacteristicStringDto.Id
                    };

                    characteristicValueStringEntityNew.Products.Add(productEntity);

                    await _characteristicValueStringRepository
                        .CreateAsync(characteristicValueStringEntityNew);

                    continue;
                }

                if (productEntity.CategoryCharacteristicsString
                    .All(x => x.Id != characteristicValueStringEntity.Id))
                {
                    characteristicValueStringEntity.Products.Add(productEntity);
                    await _characteristicValueStringRepository.UpdateNoTrackingAsync(characteristicValueStringEntity);
                }
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
