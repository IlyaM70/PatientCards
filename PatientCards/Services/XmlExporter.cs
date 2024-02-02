using PatientCards.Models;
using System.Xml;

namespace PatientCards.Services
{
    /// <summary>
    /// Класс для экспорта в XML файл
    /// </summary>
    public static class XmlExporter
    {
        #region ExportPatient
        /// <summary>
        /// Экспортировать пациента в XML файл
        /// </summary>
        /// <param name="patient">объект пациента</param>
        /// <param name="response">объект HttpResponse</param>
        public static void ExportPatient(Patient patient,HttpResponse response)
        {
            #region Создать структуру XML
            XmlDocument doc = new XmlDocument();

            // объявление XML
            XmlNode declaration = doc.CreateNode(XmlNodeType.XmlDeclaration, null, null);
            doc.AppendChild(declaration);

            // главный элемент: Patient
            XmlElement root = doc.CreateElement("Patient");
            doc.AppendChild(root);

            // подэлемент главного: LastName
            XmlElement lastName = doc.CreateElement("LastName");
            lastName.InnerText = patient.LastName;
            root.AppendChild(lastName);

            // подэлемент главного: FirstName
            XmlElement firstName = doc.CreateElement("FirstName");
            firstName.InnerText = patient.FirstName;
            root.AppendChild(firstName);

            // подэлемент главного: Patronymic
            XmlElement patronymic = doc.CreateElement("Patronymic");
            if (patient.Patronymic != null)
            {
                patronymic.InnerText = patient.Patronymic;
            }
            else
            {
                patronymic.InnerText = "";
            }
            root.AppendChild(patronymic);

            // подэлемент главного: Phone
            XmlElement phone = doc.CreateElement("Phone");
            phone.InnerText = patient.Phone;
            root.AppendChild(phone);

            // подэлемент главного: DateOfBirth
            XmlElement dateOfBirth = doc.CreateElement("DateOfBirth");
            dateOfBirth.InnerText = patient.DateOfBirth.ToString("dd.MM.yyyy");
            root.AppendChild(dateOfBirth);

            // подэлемент главного: Appointments
            XmlElement appointments = doc.CreateElement("Appointments");
            root.AppendChild(appointments);

            if (patient.Appointments.Any())
            {
                foreach (var dbAppointment in patient.Appointments)
                {
                    // подэлемент Appointments:Appointment
                    XmlElement appointment = doc.CreateElement("Appointment");
                    appointments.AppendChild(appointment);

                    // подэлемент Appointment: DateOfAppointment
                    XmlElement dateOfAppointment = doc.CreateElement("DateOfAppointment");
                    dateOfAppointment.InnerText = dbAppointment.DateOfAppointment
                        .ToLocalTime()
                        .ToString("dd.MM.yyyy");
                    appointment.AppendChild(dateOfAppointment);

                    // подэлемент Appointment: Diagnosis
                    XmlElement diagnosis = doc.CreateElement("Diagnosis");
                    diagnosis.InnerText = dbAppointment.Diagnosis;
                    appointment.AppendChild(diagnosis);
                }
            }
            #endregion

            // Создать и скачать файл
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);

            doc.WriteTo(writer);
            writer.Flush();
            response.Clear();
            byte[] byteArray = stream.ToArray();
            response.Headers.Append("Content-Disposition", "filename=Patient.xml");
            response.Headers.Append("Content-Length", byteArray.Length.ToString());
            response.ContentType = "application/octet-stream";
            response.Body.WriteAsync(byteArray);
            writer.Close();
        }
        #endregion
    }
}
