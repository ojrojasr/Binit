import { NgModule } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { RefresherComponent } from './refresher.component';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [IonicModule],
  declarations: [RefresherComponent],
  exports: [RefresherComponent]
})
export class RefresherModule {}
