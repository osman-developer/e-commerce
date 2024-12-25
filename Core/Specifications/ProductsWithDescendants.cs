using Core.Entities;

namespace Core.Specifications {
  public class ProductsWithDescendants : BaseSpecification<Product> {
    public ProductsWithDescendants (int id) : base (x => x.Id == id) {
      AddInclude (x => x.ProductBrand);
      AddInclude (x => x.ProductType);
    }
    public ProductsWithDescendants (string ? sort, int? brandId, int? typeId) : base (x =>
      (!brandId.HasValue || x.ProductBrandId == brandId) &&
      (!typeId.HasValue || x.ProductTypeId == typeId)
    ) {
      AddInclude (x => x.ProductBrand);
      AddInclude (x => x.ProductType);
      AddOrderBy (x => x.Name);

      if (!string.IsNullOrEmpty (sort)) {
        switch (sort) {
          case "priceAsc":
            AddOrderBy (p => p.Price);
            break;
          case "priceDesc":
            AddOrderByDesc (p => p.Price);
            break;
          default:
            AddOrderBy (n => n.Name);
            break;
        }
      }
    }
  }
}