import { HttpErrorResponse } from '@angular/common/http';

export class BadRequestErrorResponse extends HttpErrorResponse {
	
	constructor(response: HttpErrorResponse) {
		super(response)
	}
	
	/**
	 * This method will return an object with all the errors or a message.
	 */
	getError(): any | string {
		// Model Errors (validations)
		if (typeof this.error.errors === "object" && this.error.errors.validationError == undefined) {
			return this.error.errors
		} else {
			// Non Model Errors
			return this.getErrorMessage()
		}
	}
	
	// Used for global error handler, who always needs a message to display.
	getErrorMessage(): string {
		return this.error.errors.validationError || this.message
	}
}
