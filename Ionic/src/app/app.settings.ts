export class AppSettings {
   // public static API_DOMAIN = "http://localhost:5151/api"
   // public static API_DOMAIN = "http://192.168.10.159:5000/api"
   public static API_DOMAIN = "http://api-ignite.binit.cloud/api"

   public static GOOGLE_ANALYTICS_ID = "UA-101608081-3" 
   
   public static AUTH_TOKEN = "AuthorizationToken"

   // Determines the amount of miliseconds that must pass to show the spinner while a request is processing
   public static SHOW_SPINNER_AFTER = 1000

   // The deeplink that will be opened after the user confirms his account
   public static REGISTER_CONFIRMATION_EMAIL_CALLBACK = 'https://ignite.binit.cloud/'

   // Deeplinks configuration
   public static DEEPLINKS = {
      SCHEME: "https",
      HOST: "ignite.binit.cloud"
   }
   
   // OneSignal configuration
   public static ONE_SIGNAL = {
      APP_ID: "c49b6f71-80ac-4ef3-bf3f-d0b545095e48",
      GOOGLE_PROJECT_NUMBER: "941054936435"
   }
};