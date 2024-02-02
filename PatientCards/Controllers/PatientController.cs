using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientCards.Data;
using PatientCards.Models;
using PatientCards.Services;
using System.Xml;

namespace PatientCards.Controllers
{
    public class PatientController : Controller
    {
        #region ctor
        private readonly AppDbContext _db;
        public PatientController(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        #endregion

        #region Details
        public IActionResult Details(string id)
        {
            Patient? patient = _db.Patients.Include(x=>x.Appointments).Where(x=>x.Id==id).FirstOrDefault();
            if (patient == null)
            {
                TempData["error"] = "Пациент не найден";
                RedirectToAction("Index", "Home");
            }

            return View(patient);
        }
        #endregion

        #region Upsert
        public IActionResult Upsert(string? id = null)
        {
            Patient? patient = null;

            if (!string.IsNullOrEmpty(id))
            {
                //Update
                patient = _db.Patients.Where(x => x.Id == id).FirstOrDefault();
                if (patient == null)
                {
                    TempData["error"] = "Пациент не найден";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                patient = new Patient();
            }

            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Patient patient, string? id = null)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    //update
                    Patient? patientFromDb = _db.Patients.Where(x => x.Id == id).FirstOrDefault();
                    if (patientFromDb==null)
                    {
                        TempData["error"] = "Пациент не найден";
                        return RedirectToAction("Index", "Home");
                    }

                    patientFromDb.LastName = patient.LastName;
                    patientFromDb.FirstName = patient.FirstName;
                    patientFromDb.Patronymic = patient.Patronymic;
                    patientFromDb.Phone = patient.Phone;
                    patientFromDb.DateOfBirth = patient.DateOfBirth;

                    _db.SaveChanges();

                    TempData["success"] = "Пациент обновлен";
                   
                    return View(patient);
                }

                //Create
                patient.Id = Guid.NewGuid().ToString();
                _db.Patients.Add(patient);
                _db.SaveChanges();
                TempData["success"] = "Пациент создан";
                patient.Id = "";
            }
            else
            {
                TempData["error"] = "Введенные данные не соответсвуют требованиям";
            }
            return View(patient);
        }
        #endregion

        #region Delete
        public IActionResult Delete(string id)
        {
            Patient? patient = _db.Patients.Where(x => x.Id == id).FirstOrDefault();
            if (patient == null)
            {
                TempData["error"] = "Пациент не найден";
            }

            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id, IFormCollection collection)
        {
            Patient? patient = _db.Patients.Where(x => x.Id == id).FirstOrDefault();

            if (patient != null)
            {
                var patientAppointments =  _db.Appointments.Where(x => x.PatientId == id).ToList();
                if (patientAppointments != null)
                {
                    _db.Appointments.RemoveRange(patientAppointments);
                    _db.SaveChanges();
                }

                _db.Patients.Remove(patient);
                _db.SaveChanges();
                TempData["success"] = "Пациент удален";
            }
            else
            {
                TempData["error"] = "Пациент не найден";
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region ExportToXml
        public IActionResult ExportToXml(string id)
        {
            Patient? patient = _db.Patients.Include(x=>x.Appointments).Where(x => x.Id == id).FirstOrDefault();
            if (patient == null)
            {
                TempData["error"] = "Пациент не найден";
            }
            else
            {
                //логика экспорта
                XmlExporter.ExportPatient(patient, Response); 
                TempData["succes"] = "Файл пациента скачан";
            }
            return new EmptyResult();
        }
        #endregion
    }
}
