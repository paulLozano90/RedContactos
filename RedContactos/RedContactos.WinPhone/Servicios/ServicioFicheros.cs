﻿
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using RedContactos.Service;
using RedContactos.WinPhone.Servicios;
using Xamarin.Forms;

[assembly: Dependency(typeof(ServicioFicheros))]
namespace RedContactos.WinPhone.Servicios
{
    public class ServicioFicheros:IServicioFichero
    {
        public async void GuardarTexto(string texto, string fichero)
        {
            var carpeta = ApplicationData.Current.LocalFolder;
            var f = await carpeta.CreateFileAsync(fichero, CreationCollisionOption.ReplaceExisting);
            using (var stream = new StreamWriter(await f.OpenStreamForWriteAsync()))
            {
                stream.Write(texto);
            }
        }

        public async Task<String> CargarTexto(String fichero)
        {
            try
            {
                var carpeta = ApplicationData.Current.LocalFolder;
                var f = await carpeta.GetItemAsync(fichero);
                using (var stream = new StreamReader(f.Path))
                {
                    var txt = stream.ReadToEnd();
                    return txt;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string RecuperarTexto(string fichero)
        {
            var data = CargarTexto(fichero);
            data.Wait();
            return data.Result;
        }
    }
}