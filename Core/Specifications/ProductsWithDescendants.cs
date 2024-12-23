using Core.Entities;

namespace Core.Specifications {
  public class ProductsWithDescendants : BaseSpecification<Product> {
    public ProductsWithDescendants (int id) : base (x => x.Id == id) {
      AddInclude (x => x.ProductBrand);
      AddInclude (x => x.ProductType);
    }
    public ProductsWithDescendants () {
      AddInclude (x => x.ProductBrand);
      AddInclude (x => x.ProductType);
    }
  }
}