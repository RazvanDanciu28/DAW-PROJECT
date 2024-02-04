import { Component } from '@angular/core';
import { Product } from '../types';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent {
  products: Product[] = [];

  constructor(private productService: ProductService){}

  ngOnInit() {
    this.productService.getProducts();
    this.productService.productsToShow$.subscribe(products => {
      this.products = products;
    })
  }
}
