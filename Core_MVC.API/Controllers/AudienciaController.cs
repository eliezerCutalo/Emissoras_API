using Core_MVC.Domain.Filter;
using Core_MVC.Domain.Models;
using Core_MVC.Domain.Repository;
using Core_MVC.Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MVC.API.Controllers
{
    [Route("api/[Controller]")]
    public class AudienciaController : Controller
    {
        private readonly IAudienciaRepository _repository;

        public AudienciaController(IAudienciaRepository repository)
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

        [HttpGet("[action]", Name = "FindAudiencia")]
        public IActionResult Find([FromQuery]AudienciaFilter filter)
        {
            try
            {
                if (filter.Id == 0 && filter.Data_Hora_Audiencia == null && filter.Emissora_Audiencia == 0)
                    return BadRequest(new { mensagem = "É necessário inserir ao menos um filtro para pesquisa" });

                var audiencia = _repository.Find(filter);

                if (audiencia == null)
                    return NotFound(new { mensagem = "Audiencia não encontrada" });

                return new ObjectResult(audiencia);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = "Falha no servidor! Tente novamente mais tarde" });
            }
        }

        [HttpPost()]
        public IActionResult Add([FromBody]Audiencia audiencia)
        {
            try
            {

                string erro = AudienciaValidator.VerificaCampos(audiencia);
                if (erro != "")
                    return BadRequest(new { mensagem = erro });

                var emissora = _repository.FindEmissora(audiencia.Emissora_Audiencia);

                if (emissora == null)
                    return BadRequest(new { mensagem = "Emissora não encontrada" });

                var filter = new AudienciaFilter();
                filter.Emissora_Audiencia = audiencia.Emissora_Audiencia;
                filter.Data_Hora_Audiencia = audiencia.Data_Hora_Audiencia;

                var audienciaExistente = _repository.Find(filter);
                if (audienciaExistente != null)
                    return Unauthorized(new { mensagem = "Já existe uma pontuação definida para emissora essa hora" });

                _repository.Add(audiencia);

                return CreatedAtRoute("FindAudiencia", new { Id = audiencia.Id }, audiencia);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = "Falha no servidor! Tente novamente mais tarde" });
            }

        }
        [HttpPut("{Id}")]
        public IActionResult Update(int Id, [FromBody]Audiencia request)
        {
            try
            {

                string erro = AudienciaValidator.VerificaCampos(request);
                if (erro != "")
                    return BadRequest(new { mensagem = erro });


                var filter = new AudienciaFilter();
                filter.Id = request.Id;

                var audiencia = _repository.Find(filter);
                if (audiencia == null)
                    return NotFound(new { mensagem = "Audiencia não encontrada" });

                var emissora = _repository.FindEmissora(request.Emissora_Audiencia);

                if (emissora == null)
                    return BadRequest(new { mensagem = "Emissora não encontrada" });

                audiencia.Emissora_Audiencia = request.Emissora_Audiencia;
                audiencia.Data_Hora_Audiencia = request.Data_Hora_Audiencia;
                audiencia.Pontos_Audiencia = request.Pontos_Audiencia;

                _repository.Update(audiencia);
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
                var filter = new AudienciaFilter();
                filter.Id = Id;

                var emissora = _repository.Find(filter);

                if (emissora == null)
                    return NotFound(new { mensagem = "Emissora não encontrada" });

                _repository.Remove(emissora);

                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = "Falha no servidor! Tente novamente mais tarde" });
            }
        }
        [HttpGet("[action]")]
        public IActionResult PegarAudienciaEmissora([FromQuery]AudienciaEmissoraFilter filter)
        {
            try
            { 
                if (filter.DataAudiencia == new DateTime(0001, 01, 01))
                    return BadRequest(new { mensagem = "Data Inválida" });


               if(filter.VisaoAudiencia.ToUpper() == "SOMA")
                {
                    return new ObjectResult(_repository.PegarSomaAudienciaEmissora(filter.DataAudiencia));

                }else if(filter.VisaoAudiencia.ToUpper() == "MEDIA")
                {
                    return new ObjectResult(_repository.PegarMediaAudienciaEmissora(filter.DataAudiencia));
                }
                else
                {
                    return BadRequest(new { mensagem = "A visão de audiência deve ser 'soma' ou 'media'" });
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = "Falha no servidor! Tente novamente mais tarde" });
            }
        }
    }
}
