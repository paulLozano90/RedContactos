using System;
using System.Windows.Input;
using ContactosModel.Model;
using MvvmLibrary.Factorias;
using RedContactos.Service;
using RedContactos.Util;
using RedContactos.ViewModel.Contactos;
using Xamarin.Forms;

namespace RedContactos.ViewModel
{
    public class LoginViewModel : GeneralViewModel
    {
        private UsuarioModel _usuario;
        public ICommand cmdLogin { get; set; }
        public ICommand cmdAlta { get; set; }

        public LoginViewModel(INavigator navigator, IServicioMovil servicio, IPage page) :
                              base(navigator, servicio, page)
        {
            Usuario = new UsuarioModel();
            cmdLogin = new Command(RunLogin);
            cmdAlta = new Command(RunAlta);
        }

        public UsuarioModel Usuario
        {
            get { return _usuario; }
            set { SetProperty(ref _usuario, value); }
        }

        private async void RunLogin()
        {
            try
            {
                IsBusy = true;
                var us = await _servicio.ValidarUsuario(Usuario);
                if (us != null)
                {
                    Cadenas.Session["usuario"] = us;
                    await _navigator.PushAsync<PrincipalViewModel>(viewModel =>
                    {
                        viewModel.Titulo = "Bienvenido "+ us.nombre;
                    });
                }
            }
            catch (Exception e)
            {
                await _page.MostrarAlerta("Error", "Error en el login", "Aceptar");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void RunAlta()
        {
            await _navigator.PushAsync<AltaViewModel>(viewModel =>
            {
                viewModel.Titulo = "Alta nueva";
            });
        }
    }
}