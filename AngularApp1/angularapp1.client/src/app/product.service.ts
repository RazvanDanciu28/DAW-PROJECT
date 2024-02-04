import { Injectable } from '@angular/core';
import Constants from './types';
import { HttpClient } from '@angular/common/http';
import { Product } from './types';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private productsToShow = new Subject<Product[]>();
  productsToShow$ = this.productsToShow.asObservable();


  private readonly apiUrl = Constants.API;
  constructor(private http: HttpClient) { }

  getProducts() {
    const request = this.http.get<Product[]>(`${this.apiUrl}/Product`);

    request.subscribe(products => {
      this.productsToShow.next(products);
    });

    return request;
  }
}
