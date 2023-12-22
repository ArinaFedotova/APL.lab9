using System.ComponentModel;
using DatabaseContext;
using DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class DoctorMethods : IDoctorMethods
{
    DataBaseContext dataBase;

    public DoctorMethods(DataBaseContext dB)
    {
        dataBase = dB;
    }
    
    public void AddDoctor(string name, int specializationId)
    {
        if (dataBase.Specializations.Find(specializationId) != null) 
        { 
            dataBase.Doctors.Add(new Doctor(name, 
                dataBase.Specializations.Find(specializationId)));
        }
        else
        {
            throw new Exception();
        }
        dataBase.SaveChanges();
    }
    
    public void UpdateDoctorName(string name, int Id)
    {
        dataBase.Doctors.Find(Id).Name = name;
        var certificate = dataBase.Certificates.Where(c => c.DoctorID == Id).ToList<Certificate>();
        for (int i = 0; i < certificate.Count; i++)
        {
            dataBase.Certificates.Find(certificate[i].ID).DoctorName = name;
        } 
        dataBase.SaveChanges();
    }
    
    public void UpdateDoctorSpecialization(int specializationId, int doctorId)
    { 
        dataBase.Doctors.Find(doctorId).Specialization = dataBase.Specializations.Find(specializationId);
        dataBase.Doctors.Find(doctorId).SpecializationName = dataBase.Specializations.Find(specializationId).Name;
        dataBase.Doctors.Find(doctorId).SpecializationID = specializationId;
        dataBase.SaveChanges();
    }
    
    public string FindDoctorWithId(int doctorId)
    {
        string res = null;
        var doctor = dataBase.Doctors.
            Where(d => d.ID == doctorId).ToList();
        if (doctor.Count != 0)
            res = doctor[0].Name;;
        return res;
    }

    public List<Doctor> FindDoctorWithName(string doctorName)
    {
        var doctor = dataBase.Doctors?.
            Where(d => d.Name == doctorName).ToList();
        return doctor;
    }
    
    public void DeleteDoctor(int Id)
    {
        var doctor = dataBase.Doctors.Where
            (d => d.SpecializationID == dataBase.Doctors.Find(Id).SpecializationID).ToList();
        if (doctor.Count() > 1)
        {
            var certificate = dataBase.Certificates.Where(c => c.DoctorID == Id).ToList<Certificate>();
            for (int i = 0; i < certificate.Count; i++)
                dataBase.Certificates.Remove(certificate[i]);
            dataBase.Doctors.Remove(dataBase.Doctors.Find(Id));
        }
        else
            throw new Exception("Specialization must have at least one Doctor");
        dataBase.SaveChanges();
    }
    
    // Сколько всего врачей обладают специализацией с заданным идентификатором?
    public int CountDoctorsWithSpecialization(int specializationId)
    {
        if (dataBase.Specializations.Find(specializationId) != null)
        {
            var doctor = dataBase.Doctors.Where(d => d.SpecializationID == specializationId).ToList();
            return doctor.Count;
        }
        else
        {
            throw new Exception();
        }
    }
    
    public BindingList<Doctor> AllDoctors()
    {
        dataBase?.Doctors?.Load();
        return dataBase.Doctors.Local.ToBindingList();
    }
}