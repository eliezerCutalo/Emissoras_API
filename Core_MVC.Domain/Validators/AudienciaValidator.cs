using Core_MVC.Domain.Models;
using Core_MVC.Domain.Validators.Auxiliares;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core_MVC.Domain.Validators
{
    public static class AudienciaValidator
    {
        public static string VerificaCampos(Audiencia audiencia)
        {
            if (!Validar.VerificaSeDataValida(audiencia.Data_Hora_Audiencia))
                return "A data e hora da audiência não pode ser maior que a data atual!";

            return "";
        }
    }
}
