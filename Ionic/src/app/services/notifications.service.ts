import { OneSignal, OSNotificationOpenedResult } from '@ionic-native/onesignal/ngx';
import { DeeplinksService } from './deeplinks.service';
import { AppSettings } from '../app.settings';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class NotificationsService {

    constructor( 
        private _oneSignal: OneSignal,
        private _deeplinksService: DeeplinksService 
    ) { }

    setup() {
        this._oneSignal.startInit(AppSettings.ONE_SIGNAL.APP_ID, AppSettings.ONE_SIGNAL.GOOGLE_PROJECT_NUMBER);

        let displayOption = environment.production ? this._oneSignal.OSInFocusDisplayOption.None : this._oneSignal.OSInFocusDisplayOption.InAppAlert
		this._oneSignal.inFocusDisplaying(displayOption);
		
        this._oneSignal.iOSSettings({ kOSSettingsKeyAutoPrompt: true, kOSSettingsKeyInAppLaunchURL: false })

		this._oneSignal.handleNotificationOpened().subscribe((notification: OSNotificationOpenedResult) => {
            let url = notification.notification.payload.launchURL;
            this._deeplinksService.openFromUrl(url);
		});
		
		this._oneSignal.endInit();
    }
}