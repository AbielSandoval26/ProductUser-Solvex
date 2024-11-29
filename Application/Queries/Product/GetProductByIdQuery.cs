using MediatR;

namespace Application.Queries.Product
{
    public class GetProductByIdQuery : IRequest<Domain.Entities.Product>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int productId)
        {
            Id = productId;
        }
    }
}
