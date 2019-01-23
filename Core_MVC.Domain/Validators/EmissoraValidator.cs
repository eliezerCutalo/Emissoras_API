using Core_MVC.Domain.Models;
using Core_MVC.Domain.Validators.Auxiliares;

namespace Core_MVC.Domain.Validators
{
    public static class EmissoraValidator
    {
        public static string VerificaCampos(Emissora emissora)
        {
            if (!Validar.VerificaTamanhoString(emissora.Nome, 1, 80))
                return "O campo Nome deve conter um tamanho entre 1 e 50 caracteres";

            if (Validar.VerificaSeNaoExisteCaracteresEspeciais(emissora.Nome))
                return "O campo emissora não pode conter caracteres especiais";

            return "";
        }
    }
}
