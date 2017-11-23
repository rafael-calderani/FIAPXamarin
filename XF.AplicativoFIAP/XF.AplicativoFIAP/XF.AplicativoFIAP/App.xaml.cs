using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XF.AplicativoFIAP.ViewModel;

namespace XF.AplicativoFIAP {
    public partial class App : Application {
        #region ViewModels
        public static ProfessorViewModel ProfessorVM { get; set; }

        #endregion
        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new XF.AplicativoFIAP.View.ProfessorListView());
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
