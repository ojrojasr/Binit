import { Component, OnInit } from '@angular/core';
import { ProductDTO } from 'src/app/entities/productDTO';
import { ProductService } from 'src/app/services/product.service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { IgniteNavController } from 'src/app/extensions/ignite-nav-controller';

@Component({
  selector: 'detail-async',
  templateUrl: './detail-async.page.html',
  styleUrls: ['./detail-async.page.scss'],
})
export class DetailAsyncPage implements OnInit {

  product$: Observable<ProductDTO>

  constructor( 
    private _navCtrl: IgniteNavController,
    private _productService: ProductService,
    private _route: ActivatedRoute
  ) { }

  ngOnInit() {
    let productId = this._navCtrl.getQueryParam(this._route, "id");
    this.product$ = this._productService.get(productId)
  }

  goBack() {
    this._navCtrl.back()
  }

}
