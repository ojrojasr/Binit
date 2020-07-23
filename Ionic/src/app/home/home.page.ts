import { Component } from '@angular/core';
import { LoginDTO } from '../entities/loginDTO';
import { CallToActionService } from '../services/call-to-action.service';
import { ProductDTO } from '../entities/productDTO';
import { LanguageService } from '../services/language.service';

@Component({
	selector: 'app-home',
	templateUrl: 'home.page.html',
	styleUrls: ['home.page.scss'],
	providers: [CallToActionService]
})
export class HomePage {

	constructor(
		private _callToActionService: CallToActionService,
		private _languageService: LanguageService
	) { }

	ionViewDidEnter() {
		var dto: LoginDTO = {
			email: "administrador1@binit.com.ar",
			password: "qweQWE123!#",
			rememberMe: true
		}
	}

	callTo(action: string) {
		this._callToActionService.do<ProductDTO>(action, true, this._languageService.getValue("LOADING_STATES.LOADING")).subscribe();
	}
}
