import { Injectable, Inject } from '@angular/core';
import { IgniteHttpClientService } from './http-client.service';
import { AppSettings } from '../app.settings';
import { from, of, Observable } from 'rxjs'
import { switchMap, catchError } from 'rxjs/operators'
import { Storage } from '@ionic/storage';
import { LoginDTO } from '../entities/loginDTO';
import { RegisterDTO } from '../entities/registerDTO';
import { ForgotPasswordDTO } from '../entities/forgotPasswordDTO';
import { ResetPasswordDTO } from '../entities/resetPasswordDTO';
import { ChangePasswordDTO } from '../entities/changePasswordDTO';
import { SetPasswordDTO } from '../entities/setPasswordDTO';
import { ConfirmEmailDTO } from '../entities/confirmEmailDTO';
import { AlertService } from './alert.service';
import { NavController } from '@ionic/angular';
import { MessageService } from './message.service';
import { RegisterValidationErrors } from '../error-handling/model-error/register-validation-errors';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorService } from './error.service';
import { DOCUMENT } from '@angular/common';
import { CompleteSocialAuthDTO } from '../entities/completeSocialAuthDTO';
import { LoginRes } from '../entities/loginRes';
import { LanguageService } from './language.service';

@Injectable()
export class AuthService {

   constructor(
      private _http: IgniteHttpClientService,
      private _storage: Storage,
      private _alertService: AlertService,
      private _navCtrl: NavController,
      private _messageService: MessageService,
      @Inject(DOCUMENT) private document: Document,
	  private _errorService: ErrorService,
	  private _languageService: LanguageService
   ) { }

   /**
    * Validates the user credentials and returns a valid JWT.
    */
   login(dto: LoginDTO): Observable<any> {
      let url = `${AppSettings.API_DOMAIN}/Account/login`

      return this._http.post<any>(url, dto, false, this._languageService.getValue("LOADING_STATES.LOGGING_IN")).pipe(
         switchMap((response: any) => {
            // Set auth token
            this._storage.set(AppSettings.AUTH_TOKEN, response.token)
            // Set the root page
            this._navCtrl.navigateRoot('home').then(
               () => {
                  // Update the logout button visibility and open deeplinks pending
                  // due to authentication need
                  this._messageService.setIsAuthenticated(true)
               })
         
            return of(response);
         }),
         // Handling error for specific API call
         catchError((err: any, caught: Observable<any>) => {
            this._alertService.showError(this._languageService.getValue("ERRORS.IN_LOGIN"))
            return of(null)
         })
      )
   }

   /**
    * Creates a new user with password and sends the Welcome email with a confirmation code.
    */
   register(dto: RegisterDTO): Observable<any | RegisterValidationErrors> {
      let url = `${AppSettings.API_DOMAIN}/Account/register`

      return this._http.post<any>(url, dto, false, this._languageService.getValue("LOADING_STATES.REGISTERING")).pipe(
         catchError((error: HttpErrorResponse) => {
			// Parse the error and throw it so the component could decide what to do with it 
			// based on the outcome
         let parsedError = this._errorService.getServerError(error);
			throw typeof parsedError === 'string' ? parsedError : new RegisterValidationErrors(parsedError)
		 })
      )
   }

   /**
    * Logs out the current user.
    */
   logout(): Observable<any> {
      let url = `${AppSettings.API_DOMAIN}/Account/logout`

      return this._http.post<any>(url, null, true, this._languageService.getValue("LOADING_STATES.LOGGING_OUT")).pipe(
         switchMap((response: any) => {
            // Remove auth token
            this._storage.remove(AppSettings.AUTH_TOKEN);
            // Redirect to login
            this._navCtrl.navigateRoot('login').then(
               () => {
                  // Update the logout button visibility and reset the deeplinks async emitter
                  this._messageService.setIsAuthenticated(false)
               })

            return of(response);
         })
      )
   }

   /**
    * Confirms the user's email.
    * This endpoint should be called when a user received the Welcome email and clicked on the link.
    */
   confirmEmail(dto: ConfirmEmailDTO): Observable<any> {
      let url = `${AppSettings.API_DOMAIN}/Account/confirm-email`

      return this._http.post<any>(url, dto, false).pipe(
         switchMap((response: any) => {
            // TODO redirect to login
            return of(response);
         })
      )
   }

   /**
    * Sends the Forgot password email with a recovery code.
    */
   forgotPassword(dto: ForgotPasswordDTO): Observable<any> {
      let url = `${AppSettings.API_DOMAIN}/Account/forgot-password`

      let body = {
         email: dto.email,
         forgotPasswordEmailCallback: dto.callback
      }

      return this._http.post<any>(url, body, false).pipe(
         switchMap((response: any) => {
            // TODO redirect to login?
            return of(response);
         })
      )
   }

   /**
    * Resets the user's password.
    * This endpoint should be called when a user received the Forgot password email and clicked on the link.
    */
   resetPassword(dto: ResetPasswordDTO): Observable<any> {
      let url = `${AppSettings.API_DOMAIN}/Account/reset-password`

      return this._http.post<any>(url, dto, false).pipe(
         switchMap((response: any) => {
            return of(response);
         })
      )
   }

   /**
    * Changes the user's password.
    * This endpoint should be called by an authorized user logged in with a manual account (user and password)
    * who wants to change their current password.
    */
   changePassword(dto: ChangePasswordDTO): Observable<any> {
      let url = `${AppSettings.API_DOMAIN}/Account/change-password`

      return this._http.put<any>(url, dto, true).pipe(
         switchMap((response: any) => {
            return of(response);
         })
      )
   }

   /**
    * Sets the user's first password.
    * This endpoint should be called by an authorized user that in with a social account
    * who wants to allow manual login (with user and password).
    */
   setPassword(dto: SetPasswordDTO): Observable<any> {
      let url = `${AppSettings.API_DOMAIN}/Account/set-password`

      return this._http.put<any>(url, dto, true).pipe(
         switchMap((response: any) => {
            return of(response);
         })
      )
   }

   /**
    * Checks if the auth token is setted
    */
   isAuthenticated(): Observable<boolean> {
      return from(this._storage.get(AppSettings.AUTH_TOKEN)).pipe(
         switchMap(token => {
            return of(token != null)
         })
      )
   }
   
   /**
    * Calls the server for doing a challenge of social auth
    * @param provider The social network for doing the challenge
    */
   socialAuthentication(provider: string): Observable<any> {
      let url = `${AppSettings.API_DOMAIN}/Account/social-authentication?provider=${provider}`
      this.document.location.href = url
      return of(null)
   }

   /**
    * Sends to the server the information associated with the social provider (name and key)
    * and the information of the user necessary for register: name, lastname and email
    * @param dto Conteins the user and social provider data
    */
   completeSocialAuth(dto: CompleteSocialAuthDTO): Observable<null> {
      let url = `${AppSettings.API_DOMAIN}/Account/complete-social-auth`

      return this._http.post(url, dto, false, this._languageService.getValue("SOCIAL_AUTH.COMPLETING")).pipe(
         switchMap((loginRes: LoginRes) => {
            return this.unlockApp(loginRes.token)
         })
      )
   }

   /**
    * Sets the auth token and navigates to the root page (default is home)
    * @param token The auth token received from the server
    */
   unlockApp(token: string): Observable<any> {
      // Set auth token
      this._storage.set(AppSettings.AUTH_TOKEN, token)
      // Set the root page
      return of(
         this._navCtrl.navigateRoot('home').then(
         () => {
            // Update the logout button visibility and open deeplinks pending
            // due to authentication need
            this._messageService.setIsAuthenticated(true)
         })
      )
   }    

}