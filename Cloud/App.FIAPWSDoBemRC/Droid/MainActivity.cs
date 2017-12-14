using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Threading.Tasks;
using Gcm.Client;

namespace FIAPWSDoBemRC.Droid {
    [Activity(Label = "FIAPWSDoBemRC.Droid",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        Theme = "@android:style/Theme.Holo.Light")]
    public class MainActivity : FormsApplicationActivity, IAuthenticate {
        // Cria uma instancia da classe activity.
        // Push Notification
        static MainActivity instance = null;
        public static MainActivity CurrentActivity {
            get { return instance; }
        }

        protected override void OnCreate(Bundle bundle) {
            // Push Notification: inicializando a instancia do MainActivity.
            instance = this;

            base.OnCreate(bundle);

            // Initialize Azure Mobile Apps
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            // Initialize Xamarin Forms
            global::Xamarin.Forms.Forms.Init(this, bundle);

            // Load the main application
            LoadApplication(new App());

            // Push Notification
            try {
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);

                // Registra a mensagem no push notifications
                System.Diagnostics.Debug.WriteLine("Enviando...");
                GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
            }
            catch (Java.Net.MalformedURLException) {
                CreateAndShowDialog("Ocorreu um erro inesperado. Verifique a URL", "Falha");
            }
            catch (Exception e) {
                CreateAndShowDialog(e.Message, "Falha");
            }

            App.Init((IAuthenticate)this);
        }

        private void CreateAndShowDialog(String message, String title) {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

        private MobileServiceUser user;
        public async Task<bool> Authenticate() {
            var success = false;
            var message = string.Empty;
            try {

                user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this,
                    MobileServiceAuthenticationProvider.Twitter);
                if (user != null) {
                    message = string.Format("você está autenticado como {0}.",
                        user.UserId);
                    success = true;
                }
            }
            catch (Exception ex) {
                message = ex.Message;
            }

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Resultado Autenticação");
            builder.Create().Show();

            return success;
        }
    }
}

