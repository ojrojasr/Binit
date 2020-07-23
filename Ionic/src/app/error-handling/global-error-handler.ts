import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { LoggingService } from '../services/logging.service';
import { ErrorService } from '../services/error.service';
import { AlertService } from '../services/alert.service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

	constructor(private injector: Injector) { }

	handleError(error: Error | HttpErrorResponse) {
		const errorService = this.injector.get(ErrorService);
		const logger = this.injector.get(LoggingService);
		const alerter = this.injector.get(AlertService);

		let message: string;
		if (error instanceof HttpErrorResponse) {
			// Server error. Parse it an get a string
			message = errorService.getServerMessage(error);
			alerter.showError(message);
		} else {
			// Client Error
			message = errorService.getClientMessage(error);
			alerter.showError(message);
		}

		// Always log errors
		logger.logError(message, error);
	}
}