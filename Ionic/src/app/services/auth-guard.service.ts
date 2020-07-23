import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { NavController } from '@ionic/angular';
import { DeeplinksService } from './deeplinks.service';

@Injectable()

export class AuthGuardService implements CanActivate {

    constructor(
        private _auth: AuthService,
        private _navCtrl: NavController,
        private _deeplinksService: DeeplinksService) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
        return this._auth.isAuthenticated().pipe(
            switchMap((isAuth: boolean) => {
                if (!isAuth) {
                    // Send deeplink to the internal DeeplinksService emitter
                    this._deeplinksService.openWhenUserIsLoggedId(state.url)
                    // Go to login
                    this._navCtrl.navigateRoot('login')
                }
                return of(isAuth);
            })
        )
    }
}