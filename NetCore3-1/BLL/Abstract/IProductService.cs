using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore3_1.Entities;
using NetCore3_1.Models;
using NetCore3_1.Models.Dtos;

namespace NetCore3_1.BLL.Abstract
{
   public interface IProductService
   {
       Task<int> AddAsync(ProductDto entity);

       Task<WepApiResponse> UpdateAsync(int? productId, ProductDto entity);

       Task<WepApiResponse> DeleteAsync(int? productId);

       Task<IEnumerable<Product>> GetAsync();
       Task<Product> GetByIdAsync(int? productId);

   }
}
