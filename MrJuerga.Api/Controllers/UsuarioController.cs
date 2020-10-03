using MrJuerga.Entity;
using MrJuerga.Service;
using Microsoft.AspNetCore.Mvc;
using MrJuerga.Repository.dbcontext;
using Microsoft.AspNetCore.Authorization;
using MrJuerga.Repository.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.Extensions.Options;
using DinkToPdf;
using System.IO;
using DinkToPdf.Contracts;
using System.Collections.Generic;

namespace MrJuerga.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private IUsuarioService usuarioService;

        private ApplicationDbContext context;
        private IConverter _converter;

        private readonly AppSettings _appSettings;
        public UsuarioController(IUsuarioService usuarioService, IOptions<AppSettings> appSettings, IConverter converter)
        {
            this.usuarioService = usuarioService;
            _appSettings = appSettings.Value;
            _converter = converter;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(
                usuarioService.GetAll()
            );
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(
                usuarioService.Get(id)
            );
        }

        [HttpGet("fetchbyname/{name}")]
        public ActionResult FetchUsuariobyName(string name)
        {
            return Ok(
                usuarioService.FetchUsuariobyName(name)
            );
        }


        [HttpGet("GetExcel")]
        public FileContentResult GetExcel()
        {
            byte[] datos = usuarioService.GetExcel();

            return File(datos, "application/vnd.ms-excel", "usuarios.xlsx");
        }


        [AllowAnonymous]
        [HttpPost("LoadUsers/{name}")]
        public ActionResult LoadUsers(string name)
        {
           return Ok(
                usuarioService.loadUsers(name)
            );
        }

        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            return Ok(
                usuarioService.Save(usuario)
            );
        }


        [HttpGet("getpdf")]
        public IActionResult CreatePDF()
        {
            var usuarios = usuarioService.GetAll();

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(usuarios),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "EmployeeReport.pdf");
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UsuarioDTO usuarioDTO)
        {
            var user = usuarioService.Authenticate(usuarioDTO.Correo, usuarioDTO.Password);

            if (user == null)
                return BadRequest(new { message = "email or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Correo = user.Correo,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UsuarioDTO userDto)
        {

            try
            {
                // save 
                usuarioService.Register(userDto);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updatepsw")]
        public ActionResult updatepsw([FromBody] UsuarioDTO usuario)
        {
            try
            {
                // save 
                usuarioService.updatepsw(usuario);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody] UsuarioDTO usuario)
        {
            return Ok(
                usuarioService.updatejwt(usuario)
            );
        }

        [HttpPut("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(
                usuarioService.Delete(id)
            );
        }
    }
}