using System.ComponentModel.DataAnnotations;

namespace HRM.Data.ValidationAtribute
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrWhiteSpace(password))
                return new ValidationResult("Mật khẩu không được để trống");

            if (password.Length < 8)
                return new ValidationResult("Mật khẩu phải có ít nhất 8 ký tự");

            if (!password.Any(char.IsLower))
                return new ValidationResult("Mật khẩu phải chứa ít nhất một chữ cái thường");

            if (!password.Any(char.IsUpper))
                return new ValidationResult("Mật khẩu phải chứa ít nhất một chữ cái hoa");

            if (!password.Any(char.IsDigit))
                return new ValidationResult("Mật khẩu phải chứa ít nhất một chữ số");

            if (!password.Any(ch => "@$!%*?&".Contains(ch)))
                return new ValidationResult("Mật khẩu phải chứa ít nhất một ký tự đặc biệt (@, $, !, %, *, ?, &)");

            return ValidationResult.Success;
        }
    }

}
