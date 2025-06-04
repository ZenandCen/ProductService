using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Data.Models;
using ProductService.Application.Commands;
using ProductService.Application.Dtos;
using ProductService.Application.Queries;

namespace ProductService.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")] // This defines the route for getting a product by ID: /api/products/{id}
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var productDto = await _mediator.Send(query); // Explicitly specify the type of the response

            if (productDto == null)
            {
                return NotFound(ApiResponse<ProductResponse>.Error(
                    message: "Product not found",
                    code: ErrorCodes.ProductNotFound,
                    errors: null
                ));
            }

            var response = _mapper.Map<ProductResponse>(productDto);
            return Ok(ApiResponse<ProductResponse>.Success(response));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductResponse>>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProductsAsync(
            [FromQuery] string? search,        // e.g., ?search=apple
            [FromQuery] int page = 1,          // default page = 1
            [FromQuery] int pageSize = 10)      // default pageSize = 10
        {
            var query = new GetAllProductsQuery(search, page, pageSize);
            var productsDto = await _mediator.Send(query); // Explicitly specify the type of the response
            if (productsDto == null || !productsDto.Any())
            {
                return NotFound(ApiResponse<ProductResponse>.Error(
                    message: "Products not found",
                    code: ErrorCodes.ProductNotFound,
                    errors: null
                ));
            }

            var response = _mapper.Map<IEnumerable<ProductResponse>>(productsDto);
            return Ok(ApiResponse<IEnumerable<ProductResponse>>.Success(response));
        }



        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(ApiResponse<ProductResponse>.Error(
                    message: "Product name cannot be empty",
                    code: ErrorCodes.ProductNameEmpty,
                    errors: new { name = "Product name is required" }
                ));
            }
            if (request.Price < 0)
            {
                return BadRequest(ApiResponse<ProductResponse>.Error(
                    message: "Price cannot be negative",
                    code: ErrorCodes.PriceNegative,
                    errors: new { price = "Price must be greater than or equal to 0" }
                ));
            }
            var command = new CreateProductCommand(
                Name: request.Name,
                Price: request.Price
            );
            var productId = await _mediator.Send(command);
            var response = new ProductResponse
            {
                Id = productId,
                Name = request.Name,
                Price = request.Price
            };
            return Created($"/products/{productId}", ApiResponse<ProductResponse>.Success(response));
        }
    }
}
