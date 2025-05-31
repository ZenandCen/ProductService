using AutoMapper;
using ProductService.Api.Data.Models;
using ProductService.Application.Dtos;

namespace ProductService.Api.Data.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Ánh xạ từ ProductRequest (API) sang CreateProductCommand (Application)
            // Ánh xạ từ ProductDto (Application) sang ProductResponse (API)
            CreateMap<ProductDto, ProductResponse>();

        }
    }
}
