import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { IonicStorageModule } from '@ionic/storage';
import { AuthService } from './services/auth.service';
import { IgniteHttpClientService } from './services/http-client.service';
import { GoogleAnalytics } from '@ionic-native/google-analytics/ngx';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { ProductService } from './services/product.service';
import { AuthGuardService } from './services/auth-guard.service';
import { GlobalErrorHandler } from './error-handling/global-error-handler';
import { ServerErrorInterceptor } from './error-handling/server-error.interceptor';

import { Deeplinks } from '@ionic-native/deeplinks/ngx';
import { OneSignal } from '@ionic-native/onesignal/ngx';

import * as Sentry from 'sentry-cordova';
Sentry.init({ dsn: 'https://26f1d5df8a6546259ccab79c4b04d3df@sentry.io/1524197' });

import { TranslateModule, TranslateLoader} from '@ngx-translate/core';
import { TranslateHttpLoader} from '@ngx-translate/http-loader';
import { LanguagePopoverPageModule } from './language-popover/language-popover.module';
import { MessageService } from './services/message.service';

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, 'assets/i18n/', `.json?cb=${new Date().getTime()}`);
}

@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    IonicStorageModule.forRoot(),
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (createTranslateLoader),
        deps: [HttpClient]
      }
    }),
    LanguagePopoverPageModule
  ],
  providers: [
    StatusBar,
    SplashScreen,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    AuthService,
    IgniteHttpClientService,
    GoogleAnalytics,
    ProductService,
    AuthGuardService,
    MessageService,
    { provide: ErrorHandler, useClass: GlobalErrorHandler },
    { provide: HTTP_INTERCEPTORS, useClass: ServerErrorInterceptor, multi: true },
    Deeplinks,
    OneSignal
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
