using AutoMapper;
using Core.Entities;
using Shopping.webAPI.DTOs;

namespace Shopping.webAPI.Helpers {
  public class MappingProfiles : Profile {
    public MappingProfiles () {
      CreateMap<Product, GetProductDTO> ()
        .ForMember (x => x.ProductBrand, o => o.MapFrom (s => s.ProductBrand.Name))
        .ForMember (x => x.ProductType, o => o.MapFrom (s => s.ProductType.Name))
        .ForMember (x => x.PictureUrl, o => o.MapFrom<ProductUrlResolver> ());
    }
  }
}