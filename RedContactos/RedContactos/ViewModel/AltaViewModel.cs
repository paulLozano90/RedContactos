using System.Windows.Input;
using ContactosModel.Model;
using MvvmLibrary.Factorias;
using RedContactos.Service;
using RedContactos.ViewModel.Contactos;
using Xamarin.Forms;

namespace RedContactos.ViewModel
{
    public class AltaViewModel : GeneralViewModel
    {
        private UsuarioModel _usuario;
        public UsuarioModel Usuario
        {
            get { return _usuario; }

            set { SetProperty(ref _usuario, value); }
        }

        public ICommand cmdAlta { get; set; }

        public AltaViewModel(INavigator navigator, IServicioMovil servicio, IPage page) : 
                             base(navigator, servicio,page)
        {
            Usuario = new UsuarioModel();
            _usuario = new UsuarioModel();
            cmdAlta = new Command(RunAlta);
        }

        private async void RunAlta()
        {
            var a = Usuario.login;
            var f = _usuario;
            try
            {
                IsBusy = true;
                var noesta = await _servicio.UsuarioNuevo(Usuario.login);
                if (noesta)
                {
                    var r = await _servicio.AddUsuario(_usuario);
                    if (r != null)
                    {
                        await _navigator.PushAsync<PrincipalViewModel>(viewModel =>
                        {
                            viewModel.Titulo = "Registrate";
                        });
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}