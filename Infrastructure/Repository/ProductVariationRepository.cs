using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ProductVariationRepository : IProductVariationRepository
    {
        private readonly AppDbContext _context;

        public ProductVariationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> isExistProductVariation(ProductVariation variation)
        {
            return await _context.ProductVariations
                    .AsNoTracking()
                    .AnyAsync(v => v.ProductoId == variation.ProductoId && v.Color == variation.Color);

        }

        public async Task AddAsync(ProductVariation variation)
        {
            await _context.ProductVariations.AddAsync(variation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByProductIdAsync(int productId)
        {
            var variations = await _context.ProductVariations.Where(v => v.ProductoId == productId).ToListAsync();
            _context.ProductVariations.RemoveRange(variations);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductVariation>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductVariations.Where(v => v.ProductoId == productId).ToListAsync();
        }
    }
}
