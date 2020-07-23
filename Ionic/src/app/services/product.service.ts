import { Injectable } from '@angular/core';
import { IgniteHttpClientService } from './http-client.service';
import { AppSettings } from '../app.settings';
import { Observable } from 'rxjs'
import { ProductDTO } from '../entities/productDTO';

@Injectable()
export class ProductService {

   constructor(
      private _http: IgniteHttpClientService
   ) { }

   getAll(): Observable<ProductDTO[]> {
      let url = `${AppSettings.API_DOMAIN}/Product`

      return this._http.get<ProductDTO[]>(url, true);
   }

   get(id: string): Observable<ProductDTO> {
      let url = `${AppSettings.API_DOMAIN}/Product/${id}`

      return this._http.get<ProductDTO>(url, true);
   }

}