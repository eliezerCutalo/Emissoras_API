using Core_MVC.Domain.Models;
using System;

namespace Core_MVC.Domain.CustomerResponses
{
    public class MediaAudienciaEmissora
    {
        public Emissora Emissora { get; set; }
        public DateTime Data { get; set; }
        public double MediaPontos_Audiencia { get; set; }

        public MediaAudienciaEmissora()
        {
            Emissora = new Emissora();
        }
    }
}
