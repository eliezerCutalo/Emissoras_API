using Core_MVC.Domain.Filter;
using Core_MVC.Domain.Models;
using Core_MVC.Domain.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core_MVC.Data.Repository
{
    public class EmissoraRepository : IEmissoraRepository
    {
        private readonly Context _contexto;
        public EmissoraRepository(Context ctx)
        {
            _contexto = ctx;
        }
        public void Add(Emissora emissora)
        {
            _contexto.Emissora.Add(emissora);
            _contexto.SaveChanges();
        }

        public Emissora Find(EmissoraFilter filter)
        {
            var query = _contexto.Emissora.AsQueryable();

            if (filter.Id != 0)
                query = query.Where(e => e.Id == filter.Id);
            if (filter.Nome != null)
                query = query.Where(e => e.Nome.ToUpper() == filter.Nome.ToUpper());

            return query.FirstOrDefault();                 
                
        }

        public IEnumerable<Emissora> Get()
        {
            return _contexto.Emissora.ToList();
        }

        public void Remove(Emissora emissora)
        {
            _contexto.Emissora.Remove(emissora);
            _contexto.SaveChanges();
        }

        public bool VerificaRegistroAudienciaPorEmissora(int id)
        {
           var lista = _contexto.Audiencia.Where(x => x.Emissora_Audiencia == id).ToArray();
            if (lista.Count() > 0)
                return true;

            return false;
        }

        public void Update(Emissora emissora)
        {
            _contexto.Entry(emissora).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
        }
    }
}
