import { Injectable } from "@angular/core";
import { AlertController, LoadingController } from '@ionic/angular';
import { LoadingOptions } from '@ionic/core/dist/types/components/loading/loading-interface';
import { Observable } from 'rxjs';

@Injectable({
   providedIn: 'root'
})

export class NotificationService {

   constructor(private _alertController: AlertController, private _loadingController: LoadingController) { }

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