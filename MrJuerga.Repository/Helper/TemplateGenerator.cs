using System.Collections.Generic;
using System.Text;
using MrJuerga.Entity;
using MrJuerga.Repository.dbcontext;

namespace MrJuerga.Repository.Helper
{
    public class TemplateGenerator
    {

        private ApplicationDbContext context;

        public TemplateGenerator(ApplicationDbContext context)
        {
            this.context = context;
        }
        public static string GetHTMLString(IEnumerable<Usuario> usuarios)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Usuarios registrados en la aplicación</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Nombre</th>
                                        <th>Apellido</th>
                                        <th>Telefono</th>
                                        <th>FechaNacimiento</th>
                                        <th>Genero</th>
                                        <th>Dni</th>
                                    </tr>");
            foreach (var emp in usuarios)
            {
                var year = emp.FechaNacimiento.Year.ToString();
                var month = emp.FechaNacimiento.Month.ToString();
                var day = emp.FechaNacimiento.Day.ToString();
                var fecha = day + "/" + month + "/" + year;
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                  </tr>", emp.Nombre, emp.Apellido, emp.Telefono, fecha, emp.Genero, emp.Dni);
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}