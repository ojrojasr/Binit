import { Injectable } from '@angular/core';
import { NavController, Platform } from '@ionic/angular';
import { MessageService } from './message.service';
import { UrlSerializer, Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class IgniteNavController extends NavController {

    constructor( 
        private _messageService: MessageService,
        private _platform: Platform,
        private _location: Location,
        private _urlSerializer: UrlSerializer,
        private _router: Router
    ) {
        super(_platform, _location, _urlSerializer, _router)
    }

    navigateForward<T>(route: string, params?: T) {
        return super.navigateForward(route, { state: params })
    }

    navigateTo<T>(route: string, params?: T) {
        this._router.navigateByUrl(route, { state: params })
    }

    getData<T>(route: ActivatedRoute): Observable<T> {
        return route.paramMap.pipe(map(() => window.history.state))
    }
    
    getQueryParam(route: ActivatedRoute, paramName: string): string {
        return route.snapshot.params[paramName]
    }
    
}