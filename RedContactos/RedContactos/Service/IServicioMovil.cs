using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactosModel.Model;

namespace RedContactos.Service
{
    public interface IServicioMovil
    {
        Task<UsuarioModel> ValidarUsuario(UsuarioModel usuario);
        Task<bool> UsuarioNuevo(String login);
        Task<UsuarioModel> AddUsuario(UsuarioModel usuario);

        #region Contactos

        Task<List<ContactoModel>> GetContactos(bool actuales, int id);
        Task<ContactoModel> AddContacto(ContactoModel contacto);
        Task DelContacto(ContactoModel contacto);

        #endregion

        #region Mensajes

        Task<List<MensajeModel>> GetMensajes(int id);
        Task<MensajeModel> AddMensaje(MensajeModel mensaje);
        Task UpdateMensaje(MensajeModel mensaje);

        #endregion
    }
}