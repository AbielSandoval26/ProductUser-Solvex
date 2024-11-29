using Domain.Interfaces;
using MediatR;

namespace Application.Queries.Product
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Domain.Entities.Product>
    {
        private readonly IProductRepositoy _productRepository;

        public GetProductByIdQueryHandler(IProductRepositoy productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Domain.Entities.Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByIdAsync(request.Id);
        }
    }
}
