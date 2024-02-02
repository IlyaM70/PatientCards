using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PatientCards.Data;
using PatientCards.Models;

namespace AppointmentCards.Controllers
{
    public class AppointmentController : Controller
    {
        #region ctor
        private readonly AppDbContext _db;
        public AppointmentController(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        #endregion

        #region Details
        public IActionResult Details(string appointmentId, string patientId)
        {
            Appointment? appointment = _db.Appointments.Include(x=>x.Patient).Where(x=>x.Id==appointmentId).FirstOrDefault();
            if (appointment == null)
            {
                TempData["error"] = "Встреча не найдена";
                RedirectToAction("Details", "Patient", new { id = patientId });
            }

            return View(appointment);
        }
        #endregion

        #region Upsert
        public IActionResult Upsert(string patientId, string? appointmentId = null)
        {
            Appointment? appointment = null;

            if (!string.IsNullOrEmpty(appointmentId))
            {
                //Update
                appointment = _db.Appointments.Where(x => x.Id == appointmentId).FirstOrDefault();
                if (appointment == null)
                {
                    TempData["error"] = "Встреча не найдена";
                    //вернуться на предыдущую страницу
                    RedirectToAction("Details", "Patient", new { id = patientId });
                }
            }
            else
            {
                appointment = new Appointment();
                appointment.PatientId = patientId;
            }

            appointment!.DateOfAppointment = appointment!.DateOfAppointment.ToLocalTime();

            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Appointment appointment)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(appointment.Id))
                {
                    //update
                    Appointment? appointmentFromDb = _db.Appointments.Where(x => x.Id == appointment.Id).FirstOrDefault();
                    if (appointmentFromDb==null)
                    {
                        TempData["error"] = "Встреча не найдена";
                        //вернуться на предыдущую страницу
                        RedirectToAction("Details", "Patient", new { id = appointment.Id });
                    }

                    appointmentFromDb.Diagnosis = appointment.Diagnosis;
                    appointmentFromDb.DateOfAppointment = appointment.DateOfAppointment.ToUniversalTime();

                    _db.SaveChanges();

                    TempData["success"] = "Встреча обновлена";

                    appointment.DateOfAppointment = appointment.DateOfAppointment.ToLocalTime();
                   
                    return View(appointment);
                }

                //Create
                appointment.Id = Guid.NewGuid().ToString();

                DateTime localAppointmentTime = appointment.DateOfAppointment;
                DateTime utcAppointmentTime = localAppointmentTime.ToUniversalTime();

                appointment.DateOfAppointment = utcAppointmentTime;

                _db.Appointments.Add(appointment);
                _db.SaveChanges();
                TempData["success"] = "Встреча создана";

                appointment.Id = "";
                appointment.DateOfAppointment = localAppointmentTime;
            }
            else
            {
                TempData["error"] = "Введенные данные не соответсвуют требованиям";
            }
            return View(appointment);
        }
        #endregion

        #region Delete
        public IActionResult Delete(string appointmentId, string patientId)
        {
            Appointment? appointment = _db.Appointments.Include(x=>x.Patient).Where(x => x.Id == appointmentId).FirstOrDefault();
            if (appointment == null)
            {
                TempData["error"] = "Встреча не найдена";
                RedirectToAction("Details", "Patient", new { id = patientId });
            }

            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Delete(Appointment appointment)
        {
            Appointment? appointmentFromDb = _db.Appointments.Where(x => x.Id == appointment.Id).FirstOrDefault();

            if (appointmentFromDb != null)
            {
                _db.Appointments.Remove(appointmentFromDb);
                _db.SaveChanges();
                TempData["success"] = "Встреча удалена";
            }
            else
            {
                TempData["error"] = "Встреча не найдена";

            }
            return RedirectToAction("Details", "Patient", new { id = appointment.PatientId});

        }
        #endregion
    }
}
