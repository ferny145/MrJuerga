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

        public byte[] GetExcel()
        {
            return usuarioRepository.GetExcel();
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