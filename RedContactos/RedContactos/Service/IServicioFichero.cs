using System;

namespace RedContactos.Service
{
    public interface IServicioFichero
    {
        void GuardarTexto(String texto, String fichero);
        String RecuperarTexto(String fichero);
    }
}