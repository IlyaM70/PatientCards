using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.ObjectModel;

namespace PatientCards.Models
{
    /// <summary>
    /// Класс view model для главной страницы
    /// </summary>
    public class HomeViewModel
    {
        /// <summary>
        /// Список пациентов
        /// </summary>
        public List<Patient> patients = new List<Patient>();

        /// <summary>
        /// Список фильтров для поиска
        /// </summary>
        private static readonly Dictionary<string, string> filters = new Dictionary<string, string>() 
        {
            { "LastName","Фамилия" },
            { "FirstName","Имя" },
            { "Phone","Телефон" },
            { "DateOfBirth","Дата рождения" }
        };

        /// <summary>
        /// Selectlist для выбора фильтра
        /// </summary>
        public SelectList filterSelect = new SelectList(filters, "Key", "Value");

        /// <summary>
        /// Выбранный фильтр
        /// </summary>
        public string SelectedFilter { get; set; } = "None";

        /// <summary>
        /// Значение строки поиска
        /// </summary>
        public string SearchString { get; set; } = "";

        /// <summary>
        /// Флаг для поиска по убыванию
        /// </summary>
        public bool Descending { get; set; } = false;
    }
}
