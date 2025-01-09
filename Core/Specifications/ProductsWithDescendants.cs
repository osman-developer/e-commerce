using Core.Entities;

namespace Core.Specifications {
  public class ProductsWithDescendants : BaseSpecification<Product> {
    public ProductsWithDescendants (int id) : base (x => x.Id == id) {
      AddInclude (x => x.ProductBrand);
      AddInclude (x => x.ProductType);
    }
    public ProductsWithDescendants (ProductSpecParams productParams) : base (x =>
     (string.IsNullOrEmpty (productParams.Search) || x.Name.ToLower ().Contains (productParams.Search)) &&
      (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
      (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
    ) {
      AddInclude (x => x.ProductBrand);
      AddInclude (x => x.ProductType);
      AddOrderBy (x => x.Name);
      ApplyPaging (productParams.PageSize * (productParams.PageIndex - 1),
        productParams.PageSize);

      if (!string.IsNullOrEmpty(productParams.Sort)) {
        switch (productParams.Sort) {
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