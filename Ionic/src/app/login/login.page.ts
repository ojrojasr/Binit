import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';
import { LoginDTO } from '../entities/loginDTO';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  public loginDTO: LoginDTO = new LoginDTO();

  constructor(
    private _navCtrl: NavController,
    private _authService: AuthService
  ) { }

  ngOnInit() {
  }

  login() {
    this._authService.login(this.loginDTO).subscribe()
  }

  register() {
    this._navCtrl.navigateForward("register");
  }

  socialAuthentication(provider: string) {
    this._authService.socialAuthentication(provider).subscribe()
  }

}
