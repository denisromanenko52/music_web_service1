using System.ComponentModel.DataAnnotations;

namespace music_web_service1.Models.Domain
{
    public class Account
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Ваш Email")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Неверный формат имейла")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
