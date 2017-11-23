using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.AplicativoFIAP.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfessorListView : ContentPage {
        public ProfessorListView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
        
        }
    }
}