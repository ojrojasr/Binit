import { Injectable } from '@angular/core';
import { IgniteHttpClientService } from './http-client.service';
import { DeeplinksService } from './deeplinks.service';
import { Observable, of } from 'rxjs';
import { CallToActionDTO } from '../entities/callToActionDTO';
import { switchMap } from 'rxjs/operators';
import { AppSettings } from '../app.settings';

@Injectable()
export class CallToActionService {

   constructor( 
       private _http: IgniteHttpClientService,
       private _deeplinksService: DeeplinksService
    ) { } 

    do<T>(action: string, requiresAuthorization: boolean, loadingMessage?: string, additionalHeaders?: Observable<CallToActionDTO<T>>): Observable<CallToActionDTO<T>> {
        let url = `${AppSettings.API_DOMAIN}/CallToAction/${action}`
        let response$ = this._http.get<CallToActionDTO<T>>(url, requiresAuthorization, loadingMessage, additionalHeaders)
        return response$.pipe(
            switchMap(data => {
                this._deeplinksService.open(data.route, data.params)
                return of(null)
            })
        )
    }

}