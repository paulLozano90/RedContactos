using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using ContactosModel.Model;
using RedContactos.ViewModel.Contactos;
using Xamarin.Forms;

namespace RedContactos.Model
{
    public class NoAmigosModel
    {
        public ICommand CmdAdd { get; set; }

        public ContactoModel ContactoModel { get; set; }

        public IComponentContext ComponentContext { get; set; }
        
        public NoAmigosModel()
        {
            CmdAdd = new Command(ComandoAdd);
        }

        private async void ComandoAdd()
        {
            var vm = ComponentContext.Resolve<AddContactoViewModel>();

            var d = await vm._servicio.AddContacto(ContactoModel);
            if (d != null)
            {
                await vm._page.MostrarAlerta("Exito", "Contacto añadido", "Ok");
                vm.Amigos.Add(ContactoModel);
                vm.NoAmigos.Remove(this);
            }
            else
                await vm._page.MostrarAlerta("Error", "Contacto no añadido", "Ok");
        }
    }
}
