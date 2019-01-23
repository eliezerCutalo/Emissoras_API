using Core_MVC.Domain.CustomerResponses;
using Core_MVC.Domain.Filter;
using Core_MVC.Domain.Models;
using System;
using System.Collections.Generic;

namespace Core_MVC.Domain.Repository
{
    public interface IAudienciaRepository
    {
        void Add(Audiencia audiencia);
        IEnumerable<Audiencia> Get();
        Audiencia Find(AudienciaFilter filter);
        Emissora FindEmissora(int codigo_emissora);
        void Remove(Audiencia audiencia);
        void Update(Audiencia audiencia);
        IEnumerable<MediaAudienciaEmissora> PegarMediaAudienciaEmissora(DateTime DataAudiencia);
        IEnumerable<SomaAudienciaEmissora> PegarSomaAudienciaEmissora(DateTime DataAudiencia);


    }
}
