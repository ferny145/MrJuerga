using System.Collections.Generic;
using MrJuerga.Entity;
using MrJuerga.Repository;
using OfficeOpenXml;

namespace MrJuerga.Service.implementation
{
    public class UsuarioService: IUsuarioService
    {
        private IUsuarioRepository usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository=usuarioRepository;
        }

        public Usuario Authenticate(string username, string password)
        {
           return this.usuarioRepository.Authenticate(username,password);
        }

        public bool Delete(int id)
        {
            return usuarioRepository.Delete(id);
        }

        public IEnumerable<Usuario> FetchUsuariobyName(string name)
        {
            return usuarioRepository.FetchUsuariobyName(name);
        }

        public Usuario Get(int id)
        {
            return usuarioRepository.Get(id);
        }

        public IEnumerable<Usuario> GetAll()
        {
           return usuarioRepository.GetAll();
        }

        public Usuario GetById(int id)
        {
            return usuarioRepository.GetById(id);
        }

        public byte[] GetExcel()
        {
            return usuarioRepository.GetExcel();
        }

        public Usuario Register(UsuarioDTO user)
        {
            return usuarioRepository.Register(user);
        }

        public bool Save(Usuario entity)
        {
            return usuarioRepository.Save(entity);
        }

        public bool Update(Usuario entity)
        {
            return usuarioRepository.Update(entity);
        }
    }
}