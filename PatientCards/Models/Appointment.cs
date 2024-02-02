using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PatientCards.Models
{
    /// <summary>
    /// Сущность встречи с врачом
    /// </summary>
    public class Appointment
    {
        /// <summary>
        ///  Id встречи в базе даных
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = "";

        /// <summary>
        /// Дата встречи
        /// </summary>
        [Required(ErrorMessage = "Введите Дату встречи")]
        [NotNull]
        public DateTime DateOfAppointment{ get; set; } = DateTime.Now;

        /// <summary>
        /// Диагноз
        /// </summary>
        [Required(ErrorMessage = "Введите Диагноз")]
        [NotNull]
        [MaxLength(200, ErrorMessage = "Максимальное количество знаков 200")]
        public string Diagnosis { get; set; } = "";

        /// <summary>
        /// Id пациента в базе данных
        /// </summary>
        [Required]
        [NotNull]
        public string PatientId { get; set; } = "";

        /// <summary>
        /// Сущность пациента, навигационное свойство
        /// </summary>
        public Patient? Patient { get; set;}
    }
}
