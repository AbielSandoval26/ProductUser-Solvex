using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Product
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepositoy _productRepository;
        private readonly IProductVariationRepository _productVariationRepository;

        public UpdateProductCommandHandler(IProductRepositoy productRepository, IProductVariationRepository productVariationRepository)
        {
            _productRepository = productRepository;
            _productVariationRepository = productVariationRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return false;
            }

            product.Nombre = request.Nombre;
            product.Descripcion = request.Descripcion;
            product.ImagenUrl = request.ImagenUrl;
            product.FechaModificacion = DateTime.UtcNow;

            await _productRepository.UpdateAsync(product);

            await _productVariationRepository.DeleteByProductIdAsync(product.Id);

            foreach (var variation in request.Variaciones)
            {
                var newVariation = new ProductVariation
                {
                    ProductoId = product.Id,
                    Color = variation.Color,
                    Precio = variation.Precio,
                    FechaCreacion = DateTime.UtcNow
                };
                await _productVariationRepository.AddAsync(newVariation);
            }

            return true;
        }
    }
}
