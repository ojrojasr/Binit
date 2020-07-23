import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';
import { RegisterDTO } from '../entities/registerDTO';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { RegisterValidationErrors } from '../error-handling/model-error/register-validation-errors';
import { AlertService } from '../services/alert.service';
import { AppSettings } from '../app.settings';
import { CustomValidators } from '../helpers/custom-validators';
import { LanguageService } from '../services/language.service';

@Component({
	selector: 'app-register',
	templateUrl: './register.page.html',
	styleUrls: ['./register.page.scss'],
})
export class RegisterPage implements OnInit {

	/* The following regex establish three requirements over the password: it must have at least one digit, at least one capital letter,
	 * at least one lower case letter and it's length must be greater or equal to 8 and less or equal to 16. */
	private readonly passwordRequirementsRegex = '(?=.*\\d)(?=.*[\\u0021-\\u002b\\u003c-\\u0040])(?=.*[A-Z])(?=.*[a-z])\\S{8,16}';

	registerForm: FormGroup;
	submit = false;

	constructor(
		private _navCtrl: NavController,
		private _formBuilder: FormBuilder,
		private _authService: AuthService,
		private _alertService: AlertService,
		private _languageService: LanguageService
	) { }

	ngOnInit() {
		this.registerForm = this._formBuilder.group({
			email: ['', [Validators.required, Validators.email]],
			name: ['', Validators.required],
			lastName: ['', Validators.required],
			password: ['', [Validators.required, Validators.pattern(this.passwordRequirementsRegex)]],
			confirmPassword: ['', [Validators.required, Validators.pattern(this.passwordRequirementsRegex)]]
		}, { validators: [CustomValidators.matchPasswords] });
	}

	goBack() {
		this._navCtrl.back();
	}

	register() {
		this.submit = true;
		if (this.registerForm.valid) {
			const registerDTO: RegisterDTO = {
				email: this.registerForm.get('email').value,
				name: this.registerForm.get('name').value,
				lastName: this.registerForm.get('lastName').value,
				password: this.registerForm.get('password').value,
				confirmPassword: this.registerForm.get('confirmPassword').value,
				confirmEmailCallback: AppSettings.REGISTER_CONFIRMATION_EMAIL_CALLBACK
			}
			return this._authService.register(registerDTO).subscribe(
				() => {
					// Show success message
					let title = this._languageService.getValue("SUCCESS.TITLE");
					let message = this._languageService.getValue("SUCCESS.REGISTER");
					this._alertService.showSuccess(title, '', message);
					// Redirect to login
					this._navCtrl.navigateRoot('login');
				},
				(errors: RegisterValidationErrors | string) => {
					if (errors instanceof RegisterValidationErrors) {
						CustomValidators.serverSide<RegisterValidationErrors>(this.registerForm, errors)
					} else {
						this._alertService.showError(errors)
					}
				});
		}
	}
}
