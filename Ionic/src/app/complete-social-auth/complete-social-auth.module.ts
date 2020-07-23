import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';

import { CompleteSocialAuthPage } from './complete-social-auth.page';

const routes: Routes = [
  {
    path: '',
    component: CompleteSocialAuthPage
  }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [CompleteSocialAuthPage]
})
export class CompleteSocialAuthPageModule {}
