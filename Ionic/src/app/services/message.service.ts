import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class MessageService {

	isAuthenticated = new Subject<boolean>();

	constructor() { }

	setIsAuthenticated(value: boolean): void {
		this.isAuthenticated.next(value);
	}

}