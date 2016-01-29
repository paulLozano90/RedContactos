
using System;
using System.IO;
using RedContactos.Droid.Servicios;
using RedContactos.Service;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(ServicioFicheros))]
namespace RedContactos.Droid.Servicios
{
    public class ServicioFicheros:IServicioFichero
    {
        public void GuardarTexto(string texto, string fichero)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var rutafinal = Path.Combine(path, fichero);
            File.WriteAllText(rutafinal, fichero);
        }

        public string RecuperarTexto(string fichero)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var rutafinal = Path.Combine(path, fichero);
            try
            {
                return File.ReadAllText(rutafinal);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}