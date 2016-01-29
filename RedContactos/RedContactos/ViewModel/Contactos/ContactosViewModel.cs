﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using ContactosModel.Model;
using MvvmLibrary.Factorias;
using RedContactos.Model;
using RedContactos.Service;
using RedContactos.ViewModel.Mensajes;
using Xamarin.Forms;

namespace RedContactos.ViewModel.Contactos
{
    public class ContactosViewModel:GeneralViewModel
    {
        private ObservableCollection<ContactoModel> _amigos;
        private ObservableCollection<NoAmigosModel> _noAmigos;
        private ContactoModel _contactoSeleccionado;

        public ICommand CmdNuevo { get; set; }

        public ObservableCollection<ContactoModel> Amigos
        {
            get { return _amigos; }
            set { SetProperty(ref _amigos, value);}
        }

        public ObservableCollection<NoAmigosModel> NoAmigos
        {
            get { return _noAmigos; }
            set {SetProperty(ref _noAmigos, value);}
        }

        public ContactoModel ContactoSeleccionado
        {
            get { return _contactoSeleccionado; }
            set
            {
                SetProperty(ref _contactoSeleccionado, value);
                if(value != null)
                    RunAddMensaje();
            }
        }

        public ContactosViewModel(INavigator navigator, IServicioMovil servicio, IPage page) :
                                 base(navigator, servicio, page)
        {
            CmdNuevo = new Command(RunNuevoContacto);
        }

        public async void RunNuevoContacto()
        {
            await _navigator.PushAsync<AddContactoViewModel>(viewModel =>
            {
                viewModel.Amigos = Amigos;
                viewModel.NoAmigos = NoAmigos;
            });
        }

        public async void RunAddMensaje()
        {
            await _navigator.PushAsync<EnviarMensajeViewModel>(viewModel =>
            {
                viewModel.Contacto = ContactoSeleccionado;
                viewModel.Mensaje = new MensajeModel();
            });
            ContactoSeleccionado = null; 
        }
    }
}