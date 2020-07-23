import { TranslateService } from '@ngx-translate/core';
import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';

const LANGUAGE_KEY = 'SELECTED_LANGUAGE';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  selected = '';

  constructor(
    private _translateService: TranslateService, 
    private _storage: Storage 
  ) { }

  /*
   * Get the app language. By default it will be browser language, but if a custom language was previously setted and saved into
   * local storage, the app will retrive it and use the custom language.
   */
  setInitialAppLanguage() {
    let language = this._translateService.getBrowserLang();
    this._translateService.setDefaultLang(language);

    this._storage.get(LANGUAGE_KEY).then(val => {
      if (val) {
        this.setLanguage(val);
        this.selected = val;
      }
    });
  }

  /*
   * Set an especific language in TranslateService and save it into local storage.
   */
  setLanguage(language: string) {
    this._translateService.use(language);
    this.selected = language;
    this._storage.set(LANGUAGE_KEY, language);
  }
  
  /*
   * Get the value of a given key in the dictionary of the language previously setted in TranslateService.
   */
  getValue(key: string): string {
    return this._translateService.instant(key);
  }
}