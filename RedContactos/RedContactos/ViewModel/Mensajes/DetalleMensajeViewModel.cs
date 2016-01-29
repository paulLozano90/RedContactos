using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactosModel.Model;
using MvvmLibrary.Factorias;
using RedContactos.Service;

namespace RedContactos.ViewModel.Mensajes
{
    public class DetalleMensajeViewModel:GeneralViewModel
    {
        private MensajeModel _mensaje;

        public MensajeModel Mensaje
        {
            get
            {
                return _mensaje;
            }

            set
            {
                SetProperty(ref _mensaje , value);
            }
        }

        public DetalleMensajeViewModel(INavigator navigator, IServicioMovil servicio, IPage page) :
                                       base(navigator, servicio, page)
        {
        }
    }
}
