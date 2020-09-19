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

namespace MrJuerga.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private IUsuarioService usuarioService;

        private ApplicationDbContext context;

        private readonly AppSettings _appSettings;  
        public UsuarioController(IUsuarioService usuarioService,  IOptions<AppSettings> appSettings)
        {
            this.usuarioService = usuarioService;
            _appSettings = appSettings.Value;
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

        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            return Ok(
                usuarioService.Save(usuario)
            );
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