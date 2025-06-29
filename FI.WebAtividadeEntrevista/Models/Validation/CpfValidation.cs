
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FI.WebAtividadeEntrevista.Models.Validation
{
    public class CpfValidation : ValidationAttribute
    {
        private static readonly Regex CpfRegex = new Regex(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$");

        public CpfValidation()
        {
            ErrorMessage = "CPF inválido. Formato esperado: 000.000.000-00";
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return true;

            string cpf = value.ToString().Trim();
            return ValidarCpf(cpf);
        }

        private bool ValidarCpf(string cpf)
        {
            if (!CpfRegex.IsMatch(cpf))
                return false;

            string numeros = Regex.Replace(cpf, @"[^\d]", "");

            if (numeros.Length != 11)
                return false;

            if (TemTodosDigitosIguais(numeros))
                return false;

            return ValidarDigitosVerificadores(numeros);
        }

        private bool TemTodosDigitosIguais(string cpf)
        {
            return cpf == new string(cpf[0], cpf.Length);
        }

        private bool ValidarDigitosVerificadores(string cpf)
        {
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }

            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cpf[9].ToString()) != digitoVerificador1)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }

            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            return int.Parse(cpf[10].ToString()) == digitoVerificador2;
        }
    }
}