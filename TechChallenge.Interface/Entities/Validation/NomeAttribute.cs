using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Validation
{
    public class NomeAttribute : ValidationAttribute
    {
        private readonly int _minWords;

        public NomeAttribute(int minWords)
        {
            _minWords = minWords;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var input = value as string;

            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(ErrorMessage ?? "O campo não pode estar vazio.");
            }

            var words = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length < _minWords)
            {
                return new ValidationResult(ErrorMessage ?? $"O campo deve conter pelo menos {_minWords} palavras.");
            }

            return ValidationResult.Success;
        }
    }
}
