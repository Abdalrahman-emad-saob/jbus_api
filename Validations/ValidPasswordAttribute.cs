using System.ComponentModel.DataAnnotations;

namespace API.Validations
{
    public class ValidPasswordAttribute : ValidationAttribute
    {
        public int MinimumLength { get; set; } = 6;
        public int MinimumUpperCase { get; set; } = 1;
        public int MinimumLowerCase { get; set; } = 1;
        public int MinimumDigit { get; set; } = 1;
        public int MinimumSpecialCharacter { get; set; } = 1;

        public override bool IsValid(object? value)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (password.Length < MinimumLength)
            {
                return false;
            }

            if (password.Count(char.IsUpper) < MinimumUpperCase)
            {
                return false;
            }

            if (password.Count(char.IsLower) < MinimumLowerCase)
            {
                return false;
            }

            if (password.Count(char.IsDigit) < MinimumDigit)
            {
                return false;
            }

            if (password.Count(c => !char.IsLetterOrDigit(c)) < MinimumSpecialCharacter)
            {
                return false;
            }

            return true;
        }
    }
}