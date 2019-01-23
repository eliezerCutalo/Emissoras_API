using Core_MVC.Domain.Filter;
using Core_MVC.Domain.Models;
using Core_MVC.Domain.Repository;
using Core_MVC.Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Core_MVC.API.Controllers
{
    [Route("api/[Controller]")]
    public class EmissoraController : Controller
    {
        private readonly IEmissoraRepository _repository;

        public EmissoraController(IEmissoraRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                var lista = _repository.Get();

                return new ObjectResult(lista);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = "Falha no servidor! Tente novamente mais tarde" });
            }
        }

        [HttpGet("[action]", Name ="FindEmissora")]
        public IActionResult Find([FromQuery]EmissoraFilter filter)
        {
            try
            {
                if (filter.Id == 0 && filter.Nome.Trim() == "")
                    return BadRequest(new { mensagem = "É necessário inserir ao menos um filtro para pesquisa" });

                var emissora = _repository.Find(filter);

                if (emissora == null)
                    return NotFound(new { mensagem = "Emissora não encontrada" });

                return new ObjectResult(emissora);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = "Falha no servidor! Tente novamente mais tarde" });
            }
        }

        [HttpPost()]
        public IActionResult Add([FromBody]Emissora emissora)
        {
            try
            { 

                string erro = EmissoraValidator.VerificaCampos(emissora);
                if (erro != "")
                    return BadRequest(new { mensagem = erro });

                var filter = new EmissoraFilter();
                filter.Nome = emissora.Nome;

                var emissoraExistente = _repository.Find(filter);
                if (emissoraExistente != null)
                    return Unauthorized(new { mensagem = "Já existe uma emissora cadastrada com esse nome" });

                _repository.Add(emissora);

                return CreatedAtRoute("FindEmissora", new { Id = emissora.Id }, emissora);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = "Falha no servidor! Tente novamente mais tarde" });
            }
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int Id, [FromBody]Emissora request)
        {
            try
            {
                var filter = new EmissoraFilter();
                filter.Id = Id;

                var emissora = _repository.Find(filter);
                if (emissora == null)
                    return NotFound(new { mensagem = "Emissora não encontrada" });

                string erro = EmissoraValidator.VerificaCampos(request);
                if (erro != "")
                    return BadRequest(new { mensagem = erro });

                emissora.Nome = request.Nome;
                _repository.Update(emissora);

                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = "Falha no servidor! Tente novamente mais tarde" });
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var filter = new EmissoraFilter();
                filter.Id = Id;

                var emissora = _repository.Find(filter);

                if (emissora == null)
                    return NotFound(new { mensagem = "Emissora não encontrada" });
                if (_repository.VerificaRegistroAudienciaPorEmissora(Id))
                    return Unauthorized(new { mensagem = "Existem registros de audiência vinculados a esta emissora!" });

                _repository.Remove(emissora);

                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = "Falha no servidor! Tente novamente mais tarde" });
            }
        }
    }
}
