using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactosModel.Model;
using MvvmLibrary.Factorias;
using RedContactos.Service;

namespace RedContactos.ViewModel.Mensajes
{
    public class MisMensajesViewModel:GeneralViewModel
    {
        private ObservableCollection<MensajeModel> _mensajes;
        private MensajeModel _mensajeSeleccionado;

        public ObservableCollection<MensajeModel> Mensajes
        {
            get { return _mensajes; }
            set { SetProperty(ref _mensajes, value); }
        }

        public MensajeModel MensajeSeleccionado
        {
            get { return _mensajeSeleccionado; }
            set
            {
                SetProperty(ref _mensajeSeleccionado, value);
                if (value!=null)
                    VerDetailMensaje();
                
            }
        }

        public MisMensajesViewModel(INavigator navigator, IServicioMovil servicio, IPage page) :
                                    base(navigator, servicio, page)
        {
        }

        private async void VerDetailMensaje()
        {
            if (_mensajeSeleccionado != null)
            {
                if (!_mensajeSeleccionado.leido)
                {
                    _mensajeSeleccionado.leido = true;
                    await _servicio.UpdateMensaje(_mensajeSeleccionado);
                }
                await _navigator.PushAsync<DetalleMensajeViewModel>(viewModel =>
                {
                    viewModel.Mensaje = _mensajeSeleccionado;
                    viewModel.Titulo = _mensajeSeleccionado.asunto;
                });
                MensajeSeleccionado = null;
            }
        }
    }
}
