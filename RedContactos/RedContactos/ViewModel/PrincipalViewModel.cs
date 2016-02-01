﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Autofac;
using ContactosModel.Model;
using MvvmLibrary.Factorias;
using RedContactos.Model;
using RedContactos.Service;
using RedContactos.Util;
using RedContactos.ViewModel.Contactos;
using RedContactos.ViewModel.Mensajes;
using Xamarin.Forms;

namespace RedContactos.ViewModel
{
    public class PrincipalViewModel:GeneralViewModel
    {
        public ICommand CmdContactos { get; set; }
        public ICommand CmdMensajes { get; set; }
        public ICommand CmdSalir { get; set; }
        public IComponentContext Context { get; set; }

        public PrincipalViewModel(INavigator navigator, IServicioMovil servicio, IPage page,
                                  IComponentContext ctx) : base(navigator, servicio, page)
        {
            Context = ctx;
            CmdContactos = new Command(RunContactos);
            CmdMensajes = new Command(RunMensajes);
            CmdSalir = new Command(RunSalir);
        }

        public async void RunMensajes()
        {
            IsBusy = true;
            var yo = Cadenas.Session["usuario"] as UsuarioModel;
            var data = await _servicio.GetMensajes(yo.idUsuario);
            var a = 2;
            await _navigator.PushAsync<MisMensajesViewModel>(viewModel =>
            {
                viewModel.Mensajes = new ObservableCollection<MensajeModel>(data);
                viewModel.Titulo = "Mis mensajes";
            });
        }

        public async void RunContactos()
        {
            IsBusy = true;
            var yo = Cadenas.Session["usuario"] as UsuarioModel;
            var amigos = await _servicio.GetContactos(true, yo.idUsuario);
            var noamigos = await _servicio.GetContactos(false, yo.idUsuario);
            var oc = new ObservableCollection<NoAmigosModel>();
            foreach (var contactoModel in noamigos)
            {
                oc.Add(new NoAmigosModel()
                {
                    ComponentContext = Context,
                    ContactoModel = contactoModel
                });
            }
            await _navigator.PushAsync<ContactosViewModel>(viewModel =>
            {
                viewModel.Amigos = new ObservableCollection<ContactoModel>(amigos);
                viewModel.NoAmigos = oc;
                viewModel.Titulo = "Mis contactos";
            });
        }

        public async void RunSalir()
        {
            Cadenas.Session["usuarios"] = null;
            DependencyService.Get<IServicioFichero>().GuardarTexto("",Cadenas.FicheroSettings);
            await _navigator.PopToRootAsync();
        }
    }
}