import { Injectable } from "@angular/core";
import { AlertController } from '@ionic/angular';

@Injectable({
   providedIn: 'root'
})

export class AlertService {

   constructor( private _alertController: AlertController ) { }

   async showSuccess(title?: string, subtitle?: string, message?: string) {
      const alert = await this._alertController.create({
         header: title,
         subHeader: subtitle,
         message: message,
         buttons: ['OK']
      });

      await alert.present();
   }

   async showError(subtitle?: string, message?: string) {
      const alert = await this._alertController.create({
         header: "Error",
         subHeader: subtitle,
         message: message,
         buttons: ['OK']
      });

      await alert.present();
   }
}