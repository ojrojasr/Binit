export class RegisterValidationErrors {
    public email?: string[];
    public password?: string[];
    public name?: string[];
    public lastName?: string[];
	public confirmEmailCallback?: string[];
	
	constructor(errors: any) {
		this.email = errors.Email
		this.password = errors.Password
		this.name = errors.Name
		this.lastName = errors.LastName
		this.confirmEmailCallback = errors.ConfirmEmailCallback
	}
}