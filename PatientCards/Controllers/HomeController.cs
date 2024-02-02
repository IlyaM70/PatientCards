using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientCards.Data;
using PatientCards.Models;
using System.Diagnostics;

namespace PatientCards.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public IActionResult Index()
        {
            List<Patient> patients = _db.Patients.ToList();
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.patients = patients;
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index([FromForm]HomeViewModel viewModel)
        {
            string filter = viewModel.SelectedFilter;
            string searchString = viewModel.SearchString;
            IQueryable<Patient> query = _db.Patients;

            switch(filter) 
            {
                case "LastName":
                    if (!string.IsNullOrEmpty(searchString))
                        query = query.Where(x => x.LastName.Contains(searchString));
                    if (viewModel.Descending)
                        query = query.OrderByDescending(x => x.LastName);
                    break;
                case "FirstName":
                    if (!string.IsNullOrEmpty(searchString))
                        query = query.Where(x => x.FirstName.Contains(searchString));
                    if (viewModel.Descending)
                        query = query.OrderByDescending(x => x.FirstName);
                    break;
                case "Phone":
                    if (!string.IsNullOrEmpty(searchString))
                        query = query.Where(x => x.Phone.Contains(searchString));
                    if (viewModel.Descending)
                        query = query.OrderByDescending(x => x.Phone);
                    break;
                case "DateOfBirth":
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        // конвертировать строку в 3 числа, день, месяц, год
                        var splitedString = new List<int>();
                        foreach (var intString in searchString.Split('.'))
                        {
                            int intElement;
                            if (int.TryParse(intString, out intElement))
                                splitedString.Add(intElement);
                        }
                        //Проверить успешность конвертации
                        if (splitedString.Count()==3)
                        {
                            //создать дату из полученных чисел
                            int day = splitedString[0];
                            int month = splitedString[1];
                            int year = splitedString[2];
                            DateOnly searchDate = new DateOnly(year, month, day);
                            //найти пациентов по дате рождения
                            query = query.Where(x => x.DateOfBirth == searchDate);
                        }
                        else
                        {
                            TempData["error"] = "Введите полную дату рождения в формате дд.мм.гггг";
                        }

                    }
                    if (viewModel.Descending)
                        query = query.OrderByDescending(x => x.DateOfBirth);
                    break;
            }

            List<Patient> patients = query.ToList();
            viewModel.patients = patients;

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
