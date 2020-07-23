import { Deeplinks, DeeplinkMatch } from '@ionic-native/deeplinks/ngx';
import { Injectable, NgZone } from '@angular/core';
import { AppSettings } from '../app.settings';
import { IgniteNavController } from '../extensions/ignite-nav-controller';
import { AsyncSubject } from 'rxjs';
import { AuthService } from './auth.service';;

@Injectable({
    providedIn: 'root'
})
export class DeeplinksService  {

    private _emitter: AsyncSubject<string>
    
    /**
     * Format: /deeplink_route : angular_router_route
     */
    private readonly _routes = {
        '/products': 'products',
        '/complete-social-auth': 'complete-social-auth',
        '/authorize-access/:token': ''
    }

    constructor(
        private _deeplinks: Deeplinks,
        private _navCtrl: IgniteNavController,
        private _authService: AuthService,
        private _ngZone: NgZone
    ) { }

    setup() {
        this._deeplinks.route(this._routes).subscribe( 
            match => this.parse(match),
            nomatch => console.warn("No match for deeplink found", nomatch) 
        )
        this.setOrReset()
    }

    isDeeplink(url: string): boolean {
        return url.includes(AppSettings.DEEPLINKS.HOST)
    }

    isAuthorizeAccess(match: DeeplinkMatch) {
        return match.$link.path.includes("authorize-access")
    }

    /**
     * Extract the route conteined in deeplink
     * @param url String such that isDeeplink(url) === true
     */
    extractRoute(url: string) {
        return url.replace(`${AppSettings.DEEPLINKS.SCHEME}://${AppSettings.DEEPLINKS.HOST}`, "")
    }

    openFromUrl(url: string) {
        if ( this.isDeeplink(url) ) {
            let route = this.extractRoute(url)
            this.open(route);
        }
    }

    /**
     * Parse the deeplink match and if it is an authorization access, save the access
     * token and redirect to home page. Otherwise, open the associated page.
     * @param match The deeplink match
     * @param params Defined if and only if match.$link.queryString is undefined. Used for calls to action.
     */
    parse<T>(match: DeeplinkMatch, params?: T) {
        if ( this.isAuthorizeAccess(match) ) {
            let token = match.$args.token
            this._authService.unlockApp(token).subscribe()
        } else {
            if (match.$link.queryString) {
                params = this._navCtrl.parseQueryString<T>(match.$link.queryString)
            }
            this.open<T>(match.$route, params)
        }
    }

    open<T>(route: string, params?: T) {
        // Navigation should be done inside ngZone, otherwise there might occur template associated errors
        this._ngZone.run(() => this._navCtrl.navigateTo<T>(route, params))
    }

    openWhenUserIsLoggedId(route: string) {
        // Send the pending deeplink to the emitter
        this._emitter.next(route)
    }

    openAuthDeeplinks() {
        // Emmit the deeplink
        this._emitter.complete()
        // Delete subscription for avoid memory leak
        this._emitter.unsubscribe()
    }

    setOrReset() {
        // Set/Reset the emitter and delete previous trash values
        this._emitter = new AsyncSubject<string>()
        // Subscribe to the new emitter
        this._emitter.subscribe( route => this.open(route) )
    }

}