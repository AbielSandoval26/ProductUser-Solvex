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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepositoy _productRepository;
        private readonly IProductVariationRepository _productVariationRepository;

        public DeleteProductCommandHandler(IProductRepositoy productRepository, IProductVariationRepository productVariationRepository)
        {
            _productRepository = productRepository;
            _productVariationRepository = productVariationRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return false;
            }

            await _productVariationRepository.DeleteByProductIdAsync(request.Id);

            await _productRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}

