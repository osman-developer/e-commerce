using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shopping.webAPI.Controllers {
  [Route ("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase {
    public readonly IProductRepository _repo;
    public ProductsController (IProductRepository repo) {

      _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts () {
      var products = await _repo.GetProductsAsync ();
      return Ok (products);
    }

    [HttpGet ("{id}")]
    public async Task<ActionResult<Product>> GetProduct (int id) {
      return await _repo.GetProductByIdAsync (id);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductBrands () {
      var productBrands = await _repo.GetProductBrandsAsync ();
      return Ok (productBrands);
    }
    [HttpGet("types")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductTypes () {
      var productTypes = await _repo.GetProductTypesAsync ();
      return Ok (productTypes);
    }
  }
}