using System;
using System.ComponentModel.DataAnnotations;

namespace Core_MVC.Domain.Models
{
    public class Audiencia
    {
        [Key]
        public int Id { get; set; }
        public double Pontos_Audiencia { get; set; }
        public DateTime Data_Hora_Audiencia { get; set; }
        public int Emissora_Audiencia { get; set; }


    }
}
