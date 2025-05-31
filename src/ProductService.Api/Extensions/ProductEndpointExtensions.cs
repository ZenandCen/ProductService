using AutoMapper;
using MediatR;
using ProductService.Api.Data.Models;
using ProductService.Application.Commands;
using ProductService.Application.Queries;

namespace ProductService.Api.Extensions
{
    public static class ProductEndpointExtensions
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/products/{id}", async (Guid id, IMediator mediator, IMapper mapper) =>
            {
                var query = new GetProductByIdQuery(id);

                // Gửi query tới MediatR để lấy sản phẩm
                var productDto = await mediator.Send(query);

                // Nếu không tìm thấy sản phẩm, trả về 404
                if (productDto == null)
                {
                    return Results.NotFound(ApiResponse<ProductResponse>.Error(
                        message: "Product not found",
                        code: 1004,
                        errors: null
                    ));
                }

                // Ánh xạ kết quả thành ProductResponse
                var response = mapper.Map<ProductResponse>(productDto);

                return Results.Ok(ApiResponse<ProductResponse>.Success(response));
            });

            app.MapPost("/api/products", async (ProductRequest request, IMediator mediator, IMapper mapper) =>
            {
                // Trong validation của POST /api/products
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return Results.BadRequest(ApiResponse<ProductResponse>.Error(
                        message: "Product name cannot be empty",
                        code: ErrorCodes.ProductNameEmpty,
                        errors: new { name = "Product name is required" }
                    ));
                }

                if (request.Price < 0)
                {
                    return Results.BadRequest(ApiResponse<ProductResponse>.Error(
                        message: "Price cannot be negative",
                        code: ErrorCodes.PriceNegative,
                        errors: new { price = "Price must be greater than or equal to 0" }
                    ));
                }

                var command = new CreateProductCommand(
                    Name: request.Name,
                    Price: request.Price
                );
                var productId = await mediator.Send(command);

                var response = new ProductResponse
                {
                    Id = productId,
                    Name = request.Name,
                    Price = request.Price
                };

                return Results.Created($"/products/{productId}", ApiResponse<ProductResponse>.Success(response));
            })
            .WithName("CreateProduct")
            .WithOpenApi();

            return app;
        }
    }
}
