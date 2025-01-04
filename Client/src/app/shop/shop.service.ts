import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../shared/models/product';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/productType';
import { map } from 'rxjs';
import { response } from 'express';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'http://localhost:5000/api/';

  constructor(private http: HttpClient) {}

  getProducts(brandId?: number, typeId?: number) {
    let params = new HttpParams();
    if (brandId) {
      params = params.append('brandId', brandId.toString());
    }
    if (typeId) {
      params = params.append('typeId', typeId.toString());
    }
    return this.http
      .get<Product>(this.baseUrl + 'products', { observe: 'response', params })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }
  getBrands() {
    return this.http.get<Brand>(this.baseUrl + 'products/brands');
  }
  getTypes() {
    return this.http.get<Type>(this.baseUrl + 'products/types');
  }
}
