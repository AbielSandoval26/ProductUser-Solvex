using Application.DTO;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Product
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDTO>>
    {
        private readonly AppDbContext _context;
        public GetProductsQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Include(p => p.Variaciones)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    ImagenUrl = p.ImagenUrl,
                    Variaciones = p.Variaciones.Select(v => new ProductVariationDTO
                    {
                        Color = v.Color,
                        Precio = v.Precio
                    }).ToList()
                }).ToListAsync(cancellationToken);
        }
    }
}
