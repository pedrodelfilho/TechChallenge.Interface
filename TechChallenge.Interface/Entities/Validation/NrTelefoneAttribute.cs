using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TechChallenge.Validation
{
    public class NrTelefoneAttribute : ValidationAttribute
    {
        private const string PhoneNumberPattern = @"^9\d{8}$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return new ValidationResult(ErrorMessage ?? "O número de telefone é obrigatório.");
            }

            var regex = new Regex(PhoneNumberPattern);
            if (!regex.IsMatch(phoneNumber))
            {
                return new ValidationResult(ErrorMessage ?? "O número de telefone não está no formato correto. Deve ser 9XXXXXXXX.");
            }

            return ValidationResult.Success;
        }
    }
}
