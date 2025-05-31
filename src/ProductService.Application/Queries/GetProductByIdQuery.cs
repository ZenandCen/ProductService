using MediatR;
using ProductService.Application.Dtos;

namespace ProductService.Application.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
}
