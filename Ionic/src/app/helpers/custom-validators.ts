import { FormGroup } from '@angular/forms';

export class CustomValidators {

    static matchPasswords(registerForm: FormGroup): object {
		let result = null;
		if (registerForm) {
			const password: string = registerForm.get('password').value;
			const confirmPassword: string = registerForm.get('confirmPassword').value;
			result = password === confirmPassword ? null : { differentPasswords: true };
		}
		return result;
    }

    static serverSide<T>(form: FormGroup, errors: T): void {
        for (let errorName in errors) {
            if ( form.get(errorName) ) {
                form.get(errorName).setErrors({ serverSide: errors[errorName] });
            }
        }
    }
}