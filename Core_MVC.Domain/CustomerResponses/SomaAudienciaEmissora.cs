using Core_MVC.Domain.Models;
using System;

namespace Core_MVC.Domain.CustomerResponses
{
    public class SomaAudienciaEmissora
    {
        public Emissora Emissora { get; set; }
        public DateTime Data { get; set; }
        public double TotalPontos_Audiencia { get; set; }

        public SomaAudienciaEmissora()
        {
            Emissora = new Emissora();
        }

    }
}
