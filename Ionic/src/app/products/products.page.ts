import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { ProductDTO } from '../entities/productDTO';
import { Observable } from 'rxjs';
import { IgniteNavController } from '../extensions/ignite-nav-controller';

@Component({
  selector: 'app-products',
  templateUrl: './products.page.html',
  styleUrls: ['./products.page.scss'],
})
export class ProductsPage implements OnInit {

  products$: Observable<ProductDTO[]>;
  
  constructor(
    private _navCtrl: IgniteNavController,
    private _productService: ProductService
  ) { }

  ngOnInit() {
    this.products$ = this._productService.getAll()
  }

  seeDetail(product: ProductDTO) {
    this._navCtrl.navigateForward<ProductDTO>('product', product);
  }

  seeDetailAsync(productId: string) {
    this._navCtrl.navigateForward('product/' + productId)
  }

  goBack() {
    this._navCtrl.back()
  }
}
