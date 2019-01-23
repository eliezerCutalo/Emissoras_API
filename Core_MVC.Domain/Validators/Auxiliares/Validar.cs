using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Core_MVC.Domain.Validators.Auxiliares
{
    public static class Validar
    {
        public static bool VerificaTamanhoString(string item, int tamanhoMinimo, int tamanhoMaximo, bool?podesernulo = false)
        {
            if (podesernulo == false && item == null)
                return false;

            if (item.Length < tamanhoMinimo || item.Length > tamanhoMaximo)
                return false;

            return true;
        }
        public static bool VerificaSeNaoExisteCaracteresEspeciais(string texto)
        {
            texto = texto.ToLower();
            bool existeCaractereEspecial = Regex.IsMatch(texto, (@"[^a-zA-Z0-9- ]"));
            return existeCaractereEspecial;
        }
        public static bool VerificaSeDataValida(DateTime data)
        {
            if (data > DateTime.Now)
                return false;

            return true;
        }

    }
}
