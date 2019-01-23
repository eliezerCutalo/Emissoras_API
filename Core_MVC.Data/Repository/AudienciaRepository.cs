using Core_MVC.Domain.CustomerResponses;
using Core_MVC.Domain.Filter;
using Core_MVC.Domain.Models;
using Core_MVC.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core_MVC.Data.Repository
{
    public class AudienciaRepository : IAudienciaRepository
    {
        private readonly Context _contexto;
        public AudienciaRepository(Context ctx)
        {
            _contexto = ctx;
        }
        public void Add(Audiencia audiencia)
        {
            _contexto.Audiencia.Add(audiencia);
            _contexto.SaveChanges();
        }

        public Audiencia Find(AudienciaFilter filter)
        {
            var query = _contexto.Audiencia.AsQueryable();

            if (filter.Id != 0)
                query = query.Where(e => e.Id == filter.Id);

            if (filter.Data_Hora_Audiencia.Date != new DateTime(0001, 01, 01))
                query = query.Where(e => e.Data_Hora_Audiencia == filter.Data_Hora_Audiencia);

            if(filter.Pontos_Audiencia != 0)
                query = query.Where(e => e.Pontos_Audiencia == filter.Pontos_Audiencia);

            if (filter.Emissora_Audiencia != 0)
                query = query.Where(e => e.Emissora_Audiencia == filter.Emissora_Audiencia);

            return query.FirstOrDefault();
        }

        public Emissora FindEmissora(int codigo_emissora)
        {
            return _contexto.Emissora.FirstOrDefault(x=> x.Id ==codigo_emissora);
        }

        public IEnumerable<Audiencia> Get()
        {
            return _contexto.Audiencia.ToList();
        }

        public void Remove(Audiencia audiencia)
        {
            _contexto.Audiencia.Remove(audiencia);
            _contexto.SaveChanges();
        }

        public void Update(Audiencia audiencia)
        {
            _contexto.Entry(audiencia).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
        }

        public IEnumerable<MediaAudienciaEmissora> PegarMediaAudienciaEmissora(DateTime DataAudiencia)
        {
            var AudienciaEmissora = (from a in _contexto.Audiencia
                                     join e in _contexto.Emissora
                                     on a.Emissora_Audiencia equals e.Id
                                     where a.Data_Hora_Audiencia.Date == DataAudiencia.Date
                                     && a.Emissora_Audiencia == a.Emissora_Audiencia
                                     group new { e, a } by new { a.Emissora_Audiencia, e.Id } into g
                                     select new MediaAudienciaEmissora
                                     {
                                         Emissora = g.First().e,
                                         Data = g.First().a.Data_Hora_Audiencia,
                                         MediaPontos_Audiencia = g.Average(x => x.a.Pontos_Audiencia)
                                     }).ToList();

            return AudienciaEmissora;

        }

        public IEnumerable<SomaAudienciaEmissora> PegarSomaAudienciaEmissora(DateTime DataAudiencia)
        {
            var AudienciaEmissora = (from a in _contexto.Audiencia
                                     join e in _contexto.Emissora
                                     on a.Emissora_Audiencia equals e.Id
                                     where a.Data_Hora_Audiencia.Date == DataAudiencia.Date
                                     group new { e, a } by new { a.Emissora_Audiencia, e.Id } into g
                                     select new SomaAudienciaEmissora
                                     {
                                         Emissora = g.First().e,
                                         Data = g.First().a.Data_Hora_Audiencia,
                                         TotalPontos_Audiencia = g.Sum(x => x.a.Pontos_Audiencia)
                                     }).ToList();

            return AudienciaEmissora;

        }
    }
}
