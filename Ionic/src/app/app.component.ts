import { Component, OnInit } from '@angular/core';
import { Platform } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { GoogleAnalytics } from '@ionic-native/google-analytics/ngx';
import { AppSettings } from './app.settings';
import { AuthService } from './services/auth.service';
import { MessageService } from './services/message.service';
import { DeeplinksService } from './services/deeplinks.service';
import { NotificationsService } from './services/notifications.service';
import { isCordovaAvailable } from './helpers/is-cordova-available';
import { LanguageService } from './services/language.service';

@Component({
	selector: 'app-root',
	templateUrl: 'app.component.html'
})
export class AppComponent implements OnInit {

	isAuthenticated: boolean = false

	public appPages = [
		{
			title: 'Home',
			url: '/home',
			icon: 'home'
		},
		{
			title: 'Products',
			url: '/products',
			icon: 'list'
		}
	];

	constructor(
		private _platform: Platform,
		private _splashScreen: SplashScreen,
		private _statusBar: StatusBar,
		private _ga: GoogleAnalytics,
		private _authService: AuthService,
		private _messageService: MessageService,
		private _deeplinksService: DeeplinksService,
		private _notificationsService: NotificationsService,
		private _languageService: LanguageService
	) { }

	ngOnInit() {
		this._platform.ready().then(() => {

			this.listenToSessionStateChanges()

			// Hide the splash screen and set the status bar style
			this._splashScreen.hide();
			this._statusBar.styleDefault();

			// Setup deeplinks, notifications and analytics
			if (isCordovaAvailable()) {
				this._deeplinksService.setup();
				this._notificationsService.setup();
				this.initGoogleAnalytics();
			}

			// Setup user language
			this._languageService.setInitialAppLanguage();
		});
	}

	initGoogleAnalytics() {
		this._ga.startTrackerWithId(AppSettings.GOOGLE_ANALYTICS_ID)
			.then(() => {
				console.log('Google analytics is ready now');
			})
			.catch(e => console.log('Error starting GoogleAnalytics', e));
	}

	listenToSessionStateChanges() {
		// Receives a message indicating if the user is authenticated to show/hide elements
		// and to open saved deeplinks that requires authentication or reset the deeplinks
		// internal emitter for deleting previous values
		this._messageService.isAuthenticated.subscribe(
			(isAuthenticated: boolean) => {
				// Flag for show/hide elements
				this.isAuthenticated = isAuthenticated
				if (isAuthenticated) {
					// Open pending deeplinks
					this._deeplinksService.openAuthDeeplinks()
				} else {
					// Reset deeplinks emitter
					this._deeplinksService.setOrReset()
				}
			});
	}

	logout() {
		this._authService.logout().subscribe();
	}
}
