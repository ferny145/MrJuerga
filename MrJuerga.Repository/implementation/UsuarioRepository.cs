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
using System.Text;
using ExcelDataReader;

namespace MrJuerga.Repository.implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ApplicationDbContextDTO context;

        public UsuarioRepository(ApplicationDbContextDTO context)
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
            throw new System.NotImplementedException();
        }

        public bool Update(Usuario entity)
        {
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
                    var fecha = day + "/" + month + "/" + year;


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

        public Usuario updatepsw(UsuarioDTO user)
        {
            if (string.IsNullOrEmpty(user.Correo) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.NewPassword))
                throw new AppException("insert a password, new password or an email");

            var usuariooriginal = context.Usuarios.Single(
                    x => x.Id == user.Id
                );
            if (usuariooriginal == null)
                throw new AppException("User not found");

            // check if password is correct
            if (!VerifyPasswordHash(user.Password, usuariooriginal.PasswordHash, usuariooriginal.PasswordSalt))
                throw new AppException("password incorrect");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.NewPassword, out passwordHash, out passwordSalt);

            usuariooriginal.PasswordHash = passwordHash;
            usuariooriginal.PasswordSalt = passwordSalt;

            context.Usuarios.Update(usuariooriginal);
            context.SaveChanges();
            return usuariooriginal;
        }

        public bool loadUsers(string name)
        {
            string path = @"C:\Users\foi12\Music\MrJuerga\files\" + name;
            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] words = lines[i].Split(',');

                UsuarioDTO usuario = new UsuarioDTO();
                usuario.Nombre = words[0];
                usuario.Apellido = words[1];
                usuario.Correo = words[2];
                usuario.Telefono = words[3];
                usuario.FechaNacimiento = Convert.ToDateTime(words[4]);
                usuario.Genero = Convert.ToInt32(words[5]);
                usuario.Dni = words[6];
                usuario.Password = words[7];

                Register(usuario);
            }
            return true;
        }

        public bool loadUsersExcel(string name)
        {           
            var fileName = @"C:\Users\foi12\Music\MrJuerga\files\" + name + ".xlsx";
            bool flag = false;
            // For .net core, the next line requires the NuGet package, 
            // System.Text.Encoding.CodePages
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read()) //Each row of the file
                    {
                        if(flag){
                        UsuarioDTO usuario = new UsuarioDTO();

                        usuario.Nombre = reader.GetValue(0).ToString();
                        usuario.Apellido = reader.GetValue(1).ToString();
                        usuario.Correo = reader.GetValue(2).ToString();
                        usuario.Telefono = reader.GetValue(3).ToString();
                        usuario.FechaNacimiento = Convert.ToDateTime(reader.GetValue(4).ToString());
                        usuario.Genero = Convert.ToInt32(reader.GetValue(5).ToString());
                        usuario.Dni = reader.GetValue(6).ToString();
                        usuario.Password = reader.GetValue(7).ToString();

                        Register(usuario);
                        }
                        else{
                            flag = true;
                        }
                    };
                }
            }
            return true;
        }

    }
}

