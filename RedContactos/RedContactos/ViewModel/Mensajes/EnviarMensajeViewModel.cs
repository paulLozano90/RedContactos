
using System;
using System.Windows.Input;
using ContactosModel.Model;
using MvvmLibrary.Factorias;
using RedContactos.Service;
using RedContactos.Util;
using Xamarin.Forms;

namespace RedContactos.ViewModel.Mensajes
{
    public class EnviarMensajeViewModel:GeneralViewModel
    {
        private ContactoModel _contacto;
        private MensajeModel _mensaje;
        public ICommand CmdEnviar { get; set; }

        public EnviarMensajeViewModel(INavigator navigator, IServicioMovil servicio, IPage page) :
                                      base(navigator, servicio, page)
        {
            CmdEnviar = new Command(RunEnviarMensaje);
        }

        public ContactoModel Contacto
        {
            get{ return _contacto;}
            set{ SetProperty(ref _contacto , value);}
        }

        public MensajeModel Mensaje
        {
            get{ return _mensaje;}
            set{ SetProperty(ref _mensaje, value);}
        }

        public async void RunEnviarMensaje()
        {
            try
            {
                IsBusy = true;
                Mensaje.idOrigen = Contacto.idOrigen;
                Mensaje.fecha = DateTime.Now;
                Mensaje.idDestino = Contacto.idDestino;
                Mensaje.leido = false;
                var r = await _servicio.AddMensaje(Mensaje);
                if (r != null)
                {
                    await _page.MostrarAlerta("Exito", "Mensaje enviado", "Aceptar");
                }
                else
                {
                    await _page.MostrarAlerta("Error", "No se pudo enviar", "Aceptar");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}