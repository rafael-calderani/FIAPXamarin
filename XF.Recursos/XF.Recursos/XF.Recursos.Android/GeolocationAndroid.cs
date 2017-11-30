using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XF.Recursos.Droid;
using XF.Recursos.GPS;
using Xamarin.Geolocation;
using System.Threading.Tasks;
using Android.App;

[assembly: Dependency(typeof(GeoLocationAndroid))]
namespace XF.Recursos.Droid {
    public class GeoLocationAndroid : ILocalizacao {
        public void GetCoordenada() {
            var context = Forms.Context as Activity;
            var locator = new Geolocator(context) { DesiredAccuracy = 50 };

            locator.GetPositionAsync(timeout: 10000).ContinueWith(t => {
                SetCoordenada(t.Result.Latitude, t.Result.Longitude);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        void SetCoordenada(double paramLatitude, double paramLongitude) {
            var coordenada = new Coordenada() {
                Latitude = paramLatitude.ToString(),
                Longitude = paramLongitude.ToString()
            };

            MessagingCenter.Send<ILocalizacao, Coordenada>
                (this, "coordenada", coordenada);
        }
    }
}