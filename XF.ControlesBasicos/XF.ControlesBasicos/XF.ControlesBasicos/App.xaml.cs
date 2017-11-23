using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XF.ControlesBasicos {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new XF.ControlesBasicos.MainPage());
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
