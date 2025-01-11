import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Type } from '../shared/models/productType';
import { Brand } from '../shared/models/brand';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  standalone: false,

  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  types: Type[] = [];
  brands: Brand[] = [];
  totalCount!: number;
  shopParams = new ShopParams();
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];

  @ViewChild('search', { static: true }) searchTerm?: ElementRef;

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getTypes();
    this.getBrands();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response: any) => (
        (this.products = response.data),
        (this.shopParams.pageNumber = response.pageIndex),
        (this.shopParams.pageSize = response.pageSize),
        (this.totalCount = response.count)
      ),
      error: (error) => console.log(error),
    });
  }
  getTypes() {
    this.shopService.getTypes().subscribe({
      next: (response: any) =>
        (this.types = [{ id: 0, name: 'All' }, ...response]),
      error: (error) => console.log(error),
    });
  }
  getBrands() {
    this.shopService.getBrands().subscribe({
      next: (response: any) =>
        (this.brands = [{ id: 0, name: 'All' }, ...response]),
      error: (error) => console.log(error),
    });
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onSortSelected(event: any) {
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }
  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }
  onSearch() {
    this.shopParams.search = this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onReset() {
    this.searchTerm = undefined;
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
