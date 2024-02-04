import { Component } from '@angular/core';
import { Product } from '../types';
import { ProductService } from '../product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent {
  products: Product[] = [];

  constructor(private productService: ProductService, private router: Router){}

  ngOnInit() {
    this.productService.getProducts();
    this.productService.productsToShow$.subscribe(products => {
      this.products = products;
    })
  }

  onProductClick(productId: string) {
    this.router.navigate(['/product', productId]);
  }
}
