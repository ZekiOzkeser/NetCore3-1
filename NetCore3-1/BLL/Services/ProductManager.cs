using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NetCore3_1.BLL.Abstract;
using NetCore3_1.Entities;
using NetCore3_1.Models;
using NetCore3_1.Models.Dtos;
using NetCore3_1.Uow;

namespace NetCore3_1.BLL.Services
{
    public class ProductManager:IProductService
    {
        private readonly IUnitOfWork _uow;

        private readonly ILogger<ProductManager> _logger;

        private readonly IMapper _mapper;

        public ProductManager(IUnitOfWork uow, ILogger<ProductManager> logger, IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                await _uow.ProductRepository.AddAsync(product);
                await _uow.Commit();
                return product.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Exception Error During Add Product",productDto);
                throw e;
            }
        }

        public async Task<WepApiResponse> UpdateAsync(int? productId, ProductDto productDto)
        {
            try
            {
                var product = await _uow.ProductRepository.GetByIdAsync(productId);
                if (product!=null)
                {
                    product.Name = productDto.Name;
                    product.Price = productDto.Price;
                    product.Summary = productDto.Summary;
                    product.Thumbnail = productDto.Thumbnail;

                    await _uow.ProductRepository.UpdateAsync(product);
                    await _uow.Commit();

                    return new WepApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = true

                    };
                }
                else
                {
                    return new WepApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Status = false
                    };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Exception Error During Update Product", productDto);
                throw e;
            }
        }

        public async Task<WepApiResponse> DeleteAsync(int? productId)
        {
            try
            {
                var product = await _uow.ProductRepository.FindByAsync(a => a.Id == productId);
                if (product!=null)
                {
                    await _uow.ProductRepository.DeleteAsync(product);
                    await _uow.Commit();
                    return new WepApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = true
                    };
                }
                else
                {
                    return new WepApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Status = false
                    };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Exception Error During Delete Product",productId);
                throw e;
            }
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            try
            {
                return await _uow.ProductRepository.GetAllAsync();
            }
            catch (Exception e)
            { 
                throw e;
            }
        }

        public async Task<Product> GetByIdAsync(int? productId)
        {
            try
            {
                var result = await _uow.ProductRepository.GetByIdAsync(productId);
                return result;
            }
            catch (Exception e)
            {
               
                throw e;
            }
        }
    }
}
