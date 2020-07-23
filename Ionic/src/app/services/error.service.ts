import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { BadRequestErrorResponse } from '../error-handling/bad-request-error-response';
import { LanguageService } from './language.service';

@Injectable({
	providedIn: 'root'
})
export class ErrorService {

	constructor(private _languageService: LanguageService){ }

	getClientMessage(error: Error): string {
		if (!navigator.onLine) {
			return this._languageService.getValue("ERRORS.NO_INTERNET_CONNECTION");
		}
		return error.message ? error.message : error.toString();
	}

	getClientStack(error: Error): string {
		return error.stack;
	}

	getServerStack(error: HttpErrorResponse): string {
		// handle stack trace
		return 'stack';
	}

	/**
	 * This will be used when the error is handled manually (e.g: to show multiple model errors accordingly)
	 * @param response HttpError response from Ignite API
	 */
	getServerError(response: HttpErrorResponse): any | string {
		switch (response.status) {
			case 400:
				let badRequestErrorResponse = new BadRequestErrorResponse(response)
				return badRequestErrorResponse.getError()
			default:
				return response.error.message || response.message || this._languageService.getValue("ERRORS.GENERIC");
		}
	}
	
	/**
	 * This will be used by the global error handler to show a friendly message when something goes wrong
	 * @param error HttpError response from Ignite API
	 */
	getServerMessage(error: HttpErrorResponse): string {
		switch (error.status) {
			case 401:
				return this._languageService.getValue("ERRORS.UNAUTHORIZED");
			case 400:
				let badRequestErrorResponse = new BadRequestErrorResponse(error);
				return badRequestErrorResponse.getErrorMessage()
			default:
				return error.error.message || error.message || this._languageService.getValue("ERRORS.GENERIC");
		}
	}
}