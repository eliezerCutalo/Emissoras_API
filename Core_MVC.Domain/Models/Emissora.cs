using System;
using System.ComponentModel.DataAnnotations;

namespace Core_MVC.Domain.Models
{
    public class Emissora
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

    }
}
