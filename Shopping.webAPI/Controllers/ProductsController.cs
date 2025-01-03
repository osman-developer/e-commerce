﻿using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.webAPI.DTOs;
using Shopping.webAPI.Errors;

namespace Shopping.webAPI.Controllers {

  public class ProductsController : BaseApiController {
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<ProductType> _productTypeRepo;

    public ProductsController (IGenericRepository<Product> productRepo,
      IGenericRepository<ProductBrand> productBrandRepo,
      IGenericRepository<ProductType> productTypeRepo,
      IMapper mapper) {
      _productRepo = productRepo;
      _productTypeRepo = productTypeRepo;
      _productBrandRepo = productBrandRepo;
      _mapper = mapper;

    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GetProductDTO>>> GetProducts (string? sort,int? brandId, int? typeId) {
      var spec = new ProductsWithDescendants (sort,brandId,typeId);
      var products = await _productRepo.ListAsync (spec);
      return Ok (_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<GetProductDTO>> (products));
    }

    [HttpGet ("{id}")]
    public async Task<ActionResult<GetProductDTO>> GetProduct (int id) {
      var spec = new ProductsWithDescendants (id);
      var product = await _productRepo.GetEntityWithSpec (spec);
      if (product == null) {
        return NotFound (new ApiResponse (404));
      }
      return _mapper.Map<Product, GetProductDTO> (product);

    }

    [HttpGet ("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductBrands () {
      var productBrands = await _productBrandRepo.ListAllAsync ();
      return Ok (productBrands);
    }

    [HttpGet ("types")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductTypes () {
      var productTypes = await _productTypeRepo.ListAllAsync ();
      return Ok (productTypes);
    }
  }
}