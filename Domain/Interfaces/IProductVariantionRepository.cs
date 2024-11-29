using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductVariationRepository
    {
        Task AddAsync(ProductVariation variation);
        Task DeleteByProductIdAsync(int productId);
        Task<IEnumerable<ProductVariation>> GetByProductIdAsync(int productId);
        Task<bool> isExistProductVariation(ProductVariation variation);
        
    }
}
