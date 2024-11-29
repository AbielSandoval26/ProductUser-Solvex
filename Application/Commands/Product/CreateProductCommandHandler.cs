using Application.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Commands.Product
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepositoy _productRepository;
        private readonly IProductVariationRepository _productVariationRepository;

        public CreateProductCommandHandler(IProductRepositoy productRepository, IProductVariationRepository productVariationRepository)
        {
            _productRepository = productRepository;
            _productVariationRepository = productVariationRepository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                ImagenUrl = request.ImagenUrl,
                Variaciones = request.Variaciones.Select(v => new ProductVariation
                {
                    Color = v.Color,
                    Precio = v.Precio
                }).ToList()
            };

            await _productRepository.AddAsync(product);

            foreach (var variationRequest in product.Variaciones)
            {
                var productVariationExists = await _productVariationRepository.isExistProductVariation(variationRequest);

                if (productVariationExists)
                    continue;


                var variation = new ProductVariation
                {

                    ProductoId = variationRequest.ProductoId,
                    Color = variationRequest.Color,
                    Precio = variationRequest.Precio
                };

                await _productVariationRepository.AddAsync(variation);
            }

            return product.Id;
        }
    }
}
