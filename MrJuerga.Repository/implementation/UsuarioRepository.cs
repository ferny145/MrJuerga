using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MrJuerga.Entity;
using MrJuerga.Repository.dbcontext;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;



namespace MrJuerga.Repository.implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ApplicationDbContext context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Usuario Get(int id)
        {
            var result = new Usuario();
            try
            {
                result = context.Usuarios.Single(x => x.Id == id);
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public IEnumerable<Usuario> GetAll()
        {

            var result = new List<Usuario>();
            try
            {
                result = context.Usuarios.ToList();
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public bool Save(Usuario entity)
        {
            try
            {
                context.Add(entity);
                context.SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }
            return true;
        }

        public bool Update(Usuario entity)
        {
            try
            {
                var usuariooriginal = context.Usuarios.Single(
                    x => x.Id == entity.Id
                );

                usuariooriginal.Id = entity.Id;
                usuariooriginal.Nombre = entity.Nombre;
                usuariooriginal.Apellido = entity.Apellido;
                usuariooriginal.Correo = entity.Correo;
                usuariooriginal.Telefono = entity.Telefono;
                usuariooriginal.FechaNacimiento = entity.FechaNacimiento;
                usuariooriginal.Genero = entity.Genero;
                usuariooriginal.Password = entity.Password;
                usuariooriginal.Rol = entity.Rol;
                usuariooriginal.Dni = entity.Dni;
                usuariooriginal.Estado = entity.Estado;


                context.Update(usuariooriginal);
                context.SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var usuariooriginal = context.Usuarios.Single(
                         x => x.Id == id
                     );
                usuariooriginal.Estado = "Inactivo";
                context.Update(usuariooriginal);
                context.SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }
            return true;
        }

        public IEnumerable<Usuario> FetchUsuariobyName(string name)
        {
            var result = new List<Usuario>();
            try
            {
                result = context.Usuarios.Where(m => m.Nombre.Contains(name)).ToList();
            }
            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public Byte[] GetExcel()
        {

            string[] col_names = new string[]{
            "nombre",
            "apellido",
            "Telefono",
            "FechaNacimiento",
            "Genero",
            "Dni"
            };
            byte[] result;

            using (var package = new ExcelPackage())
            {
                var woorksheet = package.Workbook.Worksheets.Add("usuarios");
                for (int i = 0; i < col_names.Length; i++)
                {
                    woorksheet.Cells[1, i + 1].Style.Font.Size = 14;
                    woorksheet.Cells[1, i + 1].Value = col_names[i];
                    woorksheet.Cells[1, i + 1].Style.Font.Bold = true;
                    //border the cell
                    woorksheet.Cells[1, i + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    //set background color for each cell
                    woorksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    woorksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 243, 214));
                }
                int row = 2;
                var Lusuarios = new List<Usuario>();
                Lusuarios = context.Usuarios.ToList();
                foreach (Usuario item in Lusuarios)
                {
                    for (int col = 1; col <= 6; col++)
                    {
                        woorksheet.Cells[row, col].Style.Font.Size = 12;
                        woorksheet.Cells[row, col].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }
                    var year = item.FechaNacimiento.Year.ToString();
                    var month = item.FechaNacimiento.Month.ToString();
                    var day = item.FechaNacimiento.Day.ToString();
                    var fecha = day + "/"+ month+ "/" + year;
                    

                    woorksheet.Cells[row, 1].Value = item.Nombre;
                    woorksheet.Cells[row, 2].Value = item.Apellido;
                    woorksheet.Cells[row, 3].Value = item.Telefono;
                    woorksheet.Cells[row, 4].Value = fecha;
                    woorksheet.Cells[row, 5].Value = item.Genero;
                    woorksheet.Cells[row, 6].Value = item.Dni;

                    if (row % 2 == 0)
                    {
                        woorksheet.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        woorksheet.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(102, 255, 255));

                        woorksheet.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        woorksheet.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(102, 255, 255));

                        woorksheet.Cells[row, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        woorksheet.Cells[row, 3].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(102, 255, 255));

                        woorksheet.Cells[row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        woorksheet.Cells[row, 4].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(102, 255, 255));

                        woorksheet.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        woorksheet.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(102, 255, 255));

                        woorksheet.Cells[row, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        woorksheet.Cells[row, 6].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(102, 255, 255));
                    }
                    row++;
                }
                woorksheet.Cells[woorksheet.Dimension.Address].AutoFitColumns();
                result = package.GetAsByteArray();
            }
            
            return result;
        }
    }
}