import { Component, OnInit } from '@angular/core';
import { IgniteNavController } from '../extensions/ignite-nav-controller';
import { ActivatedRoute } from '@angular/router';
import { CompleteSocialAuthDTO } from '../entities/completeSocialAuthDTO';
import { AuthService } from '../services/auth.service';

@Component({
	selector: 'app-complete-social-auth',
	templateUrl: './complete-social-auth.page.html',
	styleUrls: ['./complete-social-auth.page.scss'],
})
export class CompleteSocialAuthPage implements OnInit {

	data: CompleteSocialAuthDTO

	constructor(
		private _navCtrl: IgniteNavController,
		private _route: ActivatedRoute,
		private _authService: AuthService
	) { }

	ngOnInit() {
		this._navCtrl.getData<CompleteSocialAuthDTO>(this._route).subscribe(
			data => this.data = data
		)
	}

	finish() {
		this._authService.completeSocialAuth(this.data).subscribe()
	}

}
