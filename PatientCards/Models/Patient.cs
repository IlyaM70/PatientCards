using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PatientCards.Models
{
    /// <summary>
    /// Сущность пациента
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Id пациента в базе данных
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = "";

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage ="Введите Фамилию")]
        [NotNull]
        [MaxLength(50,ErrorMessage ="Максимальное количество знаков 50")]
        public string LastName { get; set; } = "";

        /// <summary>
        /// Имя
        /// </summary>
        [Required(ErrorMessage = "Введите Имя")]
        [NotNull]
        [MaxLength(50, ErrorMessage = "Максимальное количество знаков 50")]
        public string FirstName { get; set; } = "";

        /// <summary>
        /// Отчество, если нет - пустая строка либо null 
        /// </summary>
        [MaxLength(50, ErrorMessage = "Максимальное количество знаков 50")]
        public string? Patronymic { get; set; } = "";

        /// <summary>
        /// Телефон
        /// </summary>
        [Required(ErrorMessage = "Введите Телефон")]
        [NotNull]
        [MaxLength(15,ErrorMessage = "Максимальное количество знаков 15")]
        public string Phone { get; set; } = "";

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Required(ErrorMessage = "Введите Дату рождения")]
        [NotNull]
        public DateOnly DateOfBirth { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        /// <summary>
        /// Список посещений, навигационное свойство
        /// </summary>
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
