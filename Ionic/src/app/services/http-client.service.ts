import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { AppSettings } from '../app.settings';
import { Storage } from '@ionic/storage';
import { from, Observable, forkJoin } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { LoadingController } from '@ionic/angular';
import { finalize } from 'rxjs/operators';
import { of } from 'rxjs';

enum Verb {
   GET = 'GET',
   POST = 'POST',
   DELETE = 'DELETE',
   PUT = 'PUT'
};

@Injectable()
export class IgniteHttpClientService {

   constructor(
      private _http: HttpClient,
      private _storage: Storage,
      private _loadingCtrl: LoadingController
   ) { }

   get<T>(url: string, requiresAuthorization: boolean, loadingMessage?: string, additionalHeaders?: any) {
      return this.generateRequest<T>(Verb.GET, url, null, requiresAuthorization, additionalHeaders, loadingMessage);
   }

   delete<T>(url: string, requiresAuthorization: boolean, loadingMessage?: string, additionalHeaders?: any) {
      return this.generateRequest<T>(Verb.DELETE, url, null, requiresAuthorization, additionalHeaders, loadingMessage);
   }

   post<T>(url: string, data: any, requiresAuthorization: boolean, loadingMessage?: string, additionalHeaders?: any) {
      return this.generateRequest<T>(Verb.POST, url, data, requiresAuthorization, additionalHeaders, loadingMessage);
   }

   put<T>(url: string, data: any, requiresAuthorization: boolean, loadingMessage?: string, additionalHeaders?: any) {
      return this.generateRequest<T>(Verb.PUT, url, data, requiresAuthorization, additionalHeaders, loadingMessage);
   }

   private getHeaders(additionalHeaders?: any) {
      let headers = new HttpHeaders({ "Content-Type": "application/json" });
      for (var prop in additionalHeaders) {
         headers = headers.set(prop, additionalHeaders[prop])
      }
      return headers;
   }

   private generateRequest<T>(verb: Verb, url: string, data: any = null, requiresAuthorization: boolean, additionalHeaders?: any, loadingMessage?: string): Observable<T> {

      let loading$: Observable<HTMLIonLoadingElement> = loadingMessage ? from(this._loadingCtrl.create({ message: loadingMessage })) : of(null);
      let storage$: Observable<any> = requiresAuthorization ? from(this._storage.get(AppSettings.AUTH_TOKEN)) : of(null);

      return forkJoin(loading$, storage$).pipe(
         switchMap(results => {

            let loading = null
            let loadingTimeout = setTimeout(() => {
               loading = results[0]
               if (loading) {
                  loading.present()
               }
            }, AppSettings.SHOW_SPINNER_AFTER)

            let headers = this.getHeaders(additionalHeaders);

            if (results[1]) {
               headers = headers.set('Authorization', `Bearer ${results[1]}`)
            }

            let options = {
               body: data,
               headers: headers
            };

            return this._http.request<T>(verb, url, options).pipe(
               finalize(() => {
                  clearTimeout(loadingTimeout)
                  if (loading) {
                     loading.dismiss()
                  }
               })
            );
         })
      )
   }
}