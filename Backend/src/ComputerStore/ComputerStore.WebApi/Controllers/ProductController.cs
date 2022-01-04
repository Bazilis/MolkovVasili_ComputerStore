using ComputerStore.BLL.Interfaces;
using ComputerStore.BLL.Models;
using ComputerStore.BLL.Models.FilterModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get(
            [FromQuery] DoubleFilterModel[] dob,
            [FromQuery] IntFilterModel[] num,
            [FromQuery] StringFilterModel[] str)
        {
            var result = await _productService
                .GetAllProductsByQueryParamsAsync(dob, num, str);

            return Ok(result);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            return await _productService.GetByIdWithAllCharacteristicsAsync(id);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto product)
        {
            var result = await _productService.CreateAsync(product);

            var uri = new Uri($"{Request.Path.Value}/{result.Id}".ToLower(), UriKind.Relative);

            return Created(uri, result);
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public async Task Put([FromBody] ProductDto product)
        {
            await _productService.UpdateAsync(product);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
