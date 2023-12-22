using System.ComponentModel;
using DatabaseContext;
using DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class CertificateMethods : ICertificateMethods
{
    DataBaseContext dataBase;

    public CertificateMethods(DataBaseContext dB)
    {
        dataBase = dB ?? throw new ArgumentNullException(nameof(dB));
    }

    public void AddCertificate(string description, DateTime date, int doctorId)
    {
        if (dataBase.Doctors.Find(doctorId) != null)
        {
            dataBase.Certificates.Add(new Certificate(description,
                dataBase.Doctors.Find(doctorId), date));
        }
        else
        {
            throw new Exception();
        }

        dataBase.SaveChanges();
    }

    public void UpdateCertificateDoctor(int certificateId, int doctorId)
    {
        if (dataBase.Doctors.Find(doctorId) != null)
        {
            dataBase.Certificates.Find(certificateId).Doctor =
                dataBase.Doctors.Find(doctorId);
            dataBase.Certificates.Find(certificateId).DoctorName =
                dataBase.Doctors.Find(doctorId).Name;
            dataBase.Certificates.Find(certificateId).DoctorID = doctorId;
        }
        else
        {
            throw new Exception();
        }

        dataBase.SaveChanges();
    }

    public void UpdateCertificateDescription(string newDescription, int Id)
    {
        dataBase.Certificates.Find(Id).Description = newDescription;
        dataBase.SaveChanges();
    }

    public void UpdateCertificateDate(DateTime newDate, int Id)
    {
        dataBase.Certificates.Find(Id).Date = newDate;
        dataBase.SaveChanges();
    }

    public bool FindCertificateWithId(int certificateId)
    {
        var res = false;
        var certificate = dataBase.Certificates.Where(c => c.ID == certificateId).ToList();
        if (certificate.Count != 0)
            res = true;
        return res;
    }

    // Когда был выдан (Date) хронологически последний сертификат для врача с заданным идентификатором?
    public DateTime LastCertificate(int doctorId)
    {
        if (dataBase.Doctors.Find(doctorId) != null)
        {
            var certificate = dataBase.Certificates.Where(c => c.DoctorID == doctorId)
                .OrderByDescending(c => c.Date).ToList();
            return certificate[0].Date;
        }
        else
            throw new Exception();
    }

    public void DeleteCertificate(int Id)
    {
        var certificate = dataBase.Certificates.Where
            (c => c.Doctor.ID == dataBase.Certificates.Find(Id).DoctorID).ToList();
        if (certificate.Count() > 1)
            dataBase.Certificates.Remove(dataBase.Certificates.Find(Id));
        else
        {
            throw new Exception("Doctor must have at least one Certificate");
        }
        dataBase.SaveChanges();
    }

    public BindingList<Certificate> AllCertificates()
    {
        dataBase?.Certificates?.Load();
        return dataBase.Certificates.Local.ToBindingList();
    }
}