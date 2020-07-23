import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [
	{
		path: 'login',
		loadChildren: './login/login.module#LoginPageModule'
	},
	{
		path: 'register',
		loadChildren: './register/register.module#RegisterPageModule'
	},
	{
		path: '',
		redirectTo: 'home',
		pathMatch: 'full'
	},
	{
		path: 'home',
		loadChildren: './home/home.module#HomePageModule',
		canActivate: [AuthGuardService]
	},
	{
		path: 'list',
		loadChildren: './list/list.module#ListPageModule'
	},
  	{ 
		path: 'products', 
		loadChildren: './products/products.module#ProductsPageModule',
		canActivate: [AuthGuardService]
	},
  	{ 
		path: 'product', 
		loadChildren: './products/detail/detail.module#DetailPageModule',
		canActivate: [AuthGuardService]
	},
  	{ 
		path: 'product/:id', 
		loadChildren: './products/detail-async/detail-async.module#DetailAsyncPageModule',
		canActivate: [AuthGuardService]
	},
	{ 	
		path: 'complete-social-auth', 
		loadChildren: './complete-social-auth/complete-social-auth.module#CompleteSocialAuthPageModule' 
	},
	{
		path: 'language-popover',
		loadChildren: './language-popover/language-popover.module#LanguagePopoverPageModule'
	}
];

@NgModule({
	imports: [
		RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
	],
	exports: [RouterModule]
})

export class AppRoutingModule { }
