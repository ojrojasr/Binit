import { Injectable } from '@angular/core';
import * as Sentry from 'sentry-cordova';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
   providedIn: 'root'
})

export class LoggingService {

   constructor() { }

   logError(message: string, error: Error | HttpErrorResponse) {
      let eventId = Sentry.captureException(error);
      console.log(`LoggingService: ${message}. See Sentry event with id: ${eventId}`);
    }
}