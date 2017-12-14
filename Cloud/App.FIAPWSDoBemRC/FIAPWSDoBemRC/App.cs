using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FIAPWSDoBemRC {
    public interface IAuthenticate {
        Task<bool> Authenticate();
    }

    public class App : Application {
        public App() { // The root page of your application
            MainPage = new TodoList();
        }

        protected override void OnStart() { // Handle app starts
        }

        protected override void OnSleep() { // Handle app sleeps
        }

        protected override void OnResume() { // Handle app resumes
        }

        public static IAuthenticate Authenticator { get; private set; }
        public static void Init(IAuthenticate authenticator) {
            Authenticator = authenticator;
        }
    }
}

