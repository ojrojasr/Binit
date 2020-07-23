import { Component, OnInit } from '@angular/core';
import { ProductDTO } from 'src/app/entities/productDTO';
import { Observable  } from 'rxjs';
import { ActivatedRoute, Router, RouterStateSnapshot } from '@angular/router';
import { IgniteNavController } from 'src/app/extensions/ignite-nav-controller';

@Component({
  selector: 'detail',
  templateUrl: './detail.page.html',
  styleUrls: ['./detail.page.scss']
})
export class DetailPage implements OnInit {
  
  product$: Observable<ProductDTO>

  constructor( 
    private _navCtrl: IgniteNavController,
    private _route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.product$ = this._navCtrl.getData<ProductDTO>(this._route)
  }

  goBack() {
    this._navCtrl.back()
  }

}
