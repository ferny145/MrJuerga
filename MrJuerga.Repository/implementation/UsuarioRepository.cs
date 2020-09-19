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
using MrJuerga.Repository.Helper;

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
            /*try
            {
                context.Add(entity);
                context.SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }
            return true;*/
            throw new System.NotImplementedException();
        }

        public bool Update(Usuario entity)
        { /*
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
                usuariooriginal.Rol = entity.Rol;
                usuariooriginal.Dni = entity.Dni;
                usuariooriginal.Estado = entity.Estado;


                context.Update(usuariooriginal);
                context.SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }*/
           throw new System.NotImplementedException();
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

        public Usuario Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = context.Usuarios.SingleOrDefault(x => x.Correo == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public Usuario Register(UsuarioDTO user)
        {
            Usuario nuevousuario = new Usuario();

            nuevousuario.Id = user.Id;
            nuevousuario.Nombre = user.Nombre;
            nuevousuario.Apellido = user.Apellido;
            nuevousuario.Correo = user.Correo;
            nuevousuario.Telefono = user.Telefono;
            nuevousuario.FechaNacimiento = user.FechaNacimiento;
            nuevousuario.Genero = user.Genero;
            nuevousuario.Rol = "general";
            nuevousuario.Dni = user.Dni;
            nuevousuario.Estado = "activo";
            nuevousuario.PasswordHash = null;
            nuevousuario.PasswordSalt = null;


            // validation
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new AppException("Password is required");

            if (context.Usuarios.Any(x => x.Correo == user.Correo))
                throw new AppException("Username \"" + user.Correo + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            nuevousuario.PasswordHash = passwordHash;
            nuevousuario.PasswordSalt = passwordSalt;

            context.Usuarios.Add(nuevousuario);
            context.SaveChanges();

            return nuevousuario;
        }

        public Usuario GetById(int id)
        {
           return context.Usuarios.Find(id);
        }

        public Usuario updatejwt(UsuarioDTO user)
        {
            var usuariooriginal = context.Usuarios.Single(
                    x => x.Id == user.Id
                );

            if (usuariooriginal == null)
                throw new AppException("User not found");

            if (user.Correo != usuariooriginal.Correo)
            {
                // username has changed so check if the new username is already taken
                if (context.Usuarios.Any(x => x.Correo == user.Correo))
                    throw new AppException("Username " + user.Correo + " is already taken");
            }

            // update user properties
            usuariooriginal.Nombre = user.Nombre;
            usuariooriginal.Apellido = user.Apellido;
            usuariooriginal.Correo = user.Correo;
            usuariooriginal.Telefono = user.Telefono;
            usuariooriginal.FechaNacimiento = user.FechaNacimiento;
            usuariooriginal.Genero = user.Genero;   
            usuariooriginal.Dni = user.Dni;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

                usuariooriginal.PasswordHash = passwordHash;
                usuariooriginal.PasswordSalt = passwordSalt;
            }

            context.Usuarios.Update(usuariooriginal);
            context.SaveChanges();
            return usuariooriginal;
        }
    }
}