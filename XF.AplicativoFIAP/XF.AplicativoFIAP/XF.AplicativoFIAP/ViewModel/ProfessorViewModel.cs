using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.AplicativoFIAP.Model;

namespace XF.AplicativoFIAP.ViewModel {
    public class ProfessorViewModel : INotifyPropertyChanged {
        #region Propriedades

        public Professor Professor { get; set; }

        public List<Professor> ProfessorList;
        public ObservableCollection<Professor> Professores { get; set; } = new ObservableCollection<Professor>();

        // UI Events
        public OnAdicionarProfessorCMD OnAdicionarProfessorCMD { get; }
        public OnEditarProfessorCMD OnEditarProfessorCMD { get; }
        public OnDeleteProfessorCMD OnDeleteProfessorCMD { get; }
        public ICommand OnSairCMD { get; private set; }
        public ICommand OnNovoCMD { get; private set; }

        #endregion

        public ProfessorViewModel() {
            OnSairCMD = new Command(OnSair);
            OnNovoCMD = new Command(OnNovo);
            OnAdicionarProfessorCMD = new OnAdicionarProfessorCMD(this);
            OnEditarProfessorCMD = new OnEditarProfessorCMD(this);
            OnDeleteProfessorCMD = new OnDeleteProfessorCMD(this);

            ProfessorList = ProfessorRepository.GetProfessoresSqlAzureAsync().Result;
        }

        public void Adicionar(Professor paramProfessor) {
            if (paramProfessor == null || string.IsNullOrWhiteSpace(paramProfessor.Nome)) {
                App.Current.MainPage.DisplayAlert("Atenção", "O campo nome é obrigatório", "OK");
            }
            else if (ProfessorRepository.PostProfessorSqlAzureAsync(paramProfessor).Result)
                App.Current.MainPage.Navigation.PopAsync();
            else
                App.Current.MainPage.DisplayAlert("Falhou", "Desculpe, ocorreu um erro inesperado =(", "OK");
        }

        public async void Editar() {
            await App.Current.MainPage.Navigation.PushAsync(
                new View.ProfessorCadastroView() { BindingContext = App.ProfessorVM });
        }

        public async void Remover() {
            if (await App.Current.MainPage.DisplayAlert("Atenção",
                string.Format("Tem certeza que deseja remover o {0}?", Professor.Nome), "Sim", "Não")) {
                if (ProfessorRepository.DeleteProfessorSqlAzureAsync(Professor.Id.ToString()).Result) {
                    ProfessorList.Remove(Professor);
                }
                else {
                    await App.Current.MainPage.DisplayAlert(
                            "Falhou", "Desculpe, ocorreu um erro inesperado =(", "OK");
                }
            }
        }

        private async void OnSair() {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private void OnNovo() {
            App.ProfessorVM.Professor= new Model.Professor();
            App.Current.MainPage.Navigation.PushAsync(
                new View.ProfessorCadastroView() { BindingContext = App.ProfessorVM });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void EventPropertyChanged([CallerMemberName] string propertyName = null) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    public class OnAdicionarProfessorCMD : ICommand {
        private ProfessorViewModel professorVM;
        public OnAdicionarProfessorCMD(ProfessorViewModel paramVM) {
            professorVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void AdicionarCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) {
            professorVM.Adicionar(parameter as Professor);
        }
    }

    public class OnEditarProfessorCMD : ICommand {
        private ProfessorViewModel professorVM;
        public OnEditarProfessorCMD(ProfessorViewModel paramVM) {
            professorVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void EditarCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) => (parameter != null);
        public void Execute(object parameter) {
            App.ProfessorVM.Professor = parameter as Professor;
            professorVM.Editar();
        }
    }

    public class OnDeleteProfessorCMD : ICommand {
        private ProfessorViewModel professorVM;
        public OnDeleteProfessorCMD(ProfessorViewModel paramVM) {
            professorVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void DeleteCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) => (parameter != null);
        public void Execute(object parameter) {
            App.ProfessorVM.Professor = parameter as Professor;
            professorVM.Remover();
        }
    }
}
