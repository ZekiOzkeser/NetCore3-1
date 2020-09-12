using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore3_1.BLL.Abstract;
using NetCore3_1.Models.Dtos;

namespace NetCore3_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        private IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var productList = await _productService.GetAsync();
                if (productList == null)
                    return NotFound();

                return Ok(productList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto productModel)
        {
            try
            {
                var productId = await _productService.AddAsync(productModel);
                if (productId > 0)
                    return Ok(productId);

                else
                    return BadRequest("An Error Occured While Creating New product");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(int? id, [FromBody] ProductDto productModel)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var result = await _productService.UpdateAsync(id, productModel);
                if (result.Status)
                    return Ok();
                else
                    return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var result = await _productService.DeleteAsync(id);
                if (result.Status)
                    return Ok();

                else
                    return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
