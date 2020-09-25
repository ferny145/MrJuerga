using MrJuerga.Entity;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Text;

namespace MrJuerga.Repository
{
    public interface IUsuarioRepository: IRepository<Usuario>
    {
          IEnumerable<Usuario> FetchUsuariobyName (string name);
          byte[]  GetExcel();
           Usuario Authenticate(string username, string password);
           Usuario Register(UsuarioDTO user);
           Usuario GetById(int id);
           Usuario updatejwt(UsuarioDTO user);
           Usuario updatepsw(UsuarioDTO user);
         
    }
}