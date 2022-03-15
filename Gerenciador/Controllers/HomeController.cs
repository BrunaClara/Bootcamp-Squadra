using Gerenciador.Data;
using Gerenciador.Data.Responses;
using Gerenciador.Model;
using Gerenciador.Repositorio;
using Gerenciador.ServiceToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static Gerenciador.Model.Curso;

namespace Gerenciador.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<dynamic> Authenticate([FromBody] UserResponse model)
        {
            if (!model.Senha.Any() || !model.Usuario.Any())
                return BadRequest("Usuário ou senha vazio");
            var user = UserRepositorio.Get(model.Usuario, model.Senha);
            if (user is null)
                return BadRequest("Usuário ou senha inválidos");
            var token = TokenService.GenerateToken(user);
            user.Senha = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("cursos")]
        [AllowAnonymous]
        public ActionResult<List<CursoResponse>>GetCursos()
        {
            try
            {
                var listaCursos = _context.Cursos.ToList();
                if (!listaCursos.Any())
                    return NoContent();
                return Ok (listaCursos.Select(p=>p.AsResponse()).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("cursos/status")]
        [AllowAnonymous]
        public ActionResult<List<CursoResponse>> GetStatus([FromQuery] StatusTipo status)
        {
            try
            {
                var consultaStatus = _context.Cursos.Where(P => P.Status == status).ToList();
                if (!consultaStatus.Any())
                    return NoContent();
                return Ok(consultaStatus.Select(p => p.AsResponse()).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("incluir")]
        [Authorize(Roles = "gerente")]
        public ActionResult PostIncluir([FromBody] Curso model, StatusTipo status)
        {
            try
            {
                _context.Cursos.Add(model);
                _context.SaveChanges();
                return Ok($"Curso {model.Titulo} criado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }                  
        }

        [HttpPatch]
        [Route("atualizar")]
        [Authorize(Roles = "secretaria,gerente")]
        public ActionResult PatchAtualizar([FromForm] StatusTipo Status, int Id)
        {
            try
            {
                var cursoId = _context.Cursos.Find(Id);
                if (cursoId is null)
                    return NotFound();
                cursoId.Status = Status;
                _context.SaveChanges();
                return Ok ($"Curso {Id} atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("excluir")]
        [Authorize(Roles = "gerente")]
        public ActionResult<dynamic> Exluir([FromQuery] int Id)
        {
            try
            {
                var cursoId = _context.Cursos.Find(Id);
                if (cursoId is null)
                    return NotFound();
                _context.Cursos.Remove(cursoId);
                _context.SaveChanges();
                return Ok($"Curso {Id} removido com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
