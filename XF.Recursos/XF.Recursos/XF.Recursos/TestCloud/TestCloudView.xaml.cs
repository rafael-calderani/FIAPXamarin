using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Recursos.TestCloud {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestCloudView : ContentPage {
        public TestCloudView() {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e) {
            try {
                throw new Exception("Ocorreu um erro inesperado");
            }
            catch (Exception error) {
                DisplayAlert("Exceção", error.Message, "Ok");
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e) {
            try {
                throw new IndexOutOfRangeException("Index fora do intervalo");
            }
            catch (Exception error) {
                DisplayAlert("Exceção", error.Message, "Ok");
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e) {
            try {
                throw new OutOfMemoryException("Estouro de memória");
            }
            catch (Exception error) {
                DisplayAlert("Exceção", error.Message, "Ok");
            }
        }

        private void OnTested_Clicked(object sender, EventArgs e) {
            throw new OutOfMemoryException("Estouro de memória");
        }
    }
}