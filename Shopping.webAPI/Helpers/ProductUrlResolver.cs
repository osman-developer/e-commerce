using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Shopping.webAPI.DTOs;

namespace Shopping.webAPI.Helpers {
  public class ProductUrlResolver : IValueResolver<Product, GetProductDTO, string> {
    private readonly IConfiguration _config;

    public ProductUrlResolver (IConfiguration config) {
      _config = config;
    }
    public string Resolve (Product source, GetProductDTO destination, string destMember, ResolutionContext context) {
      if (!string.IsNullOrEmpty (source.PictureUrl)) {
        return _config["ApiUrl"] + source.PictureUrl;
      }
      return null;
    }
  }
}