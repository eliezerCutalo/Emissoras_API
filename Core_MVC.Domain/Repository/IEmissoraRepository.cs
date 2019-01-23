using Core_MVC.Domain.Filter;
using Core_MVC.Domain.Models;
using System.Collections.Generic;

namespace Core_MVC.Domain.Repository
{
    public interface IEmissoraRepository
    {
        void Add(Emissora emissora);
        IEnumerable<Emissora> Get();
        Emissora Find(EmissoraFilter filter);
        void Remove(Emissora emissora);
        void Update(Emissora emissora);
        bool VerificaRegistroAudienciaPorEmissora(int id);
    }
}
