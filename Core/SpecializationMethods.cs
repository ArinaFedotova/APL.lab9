using System.ComponentModel;
using DatabaseContext;
using DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class SpecializationMethods : ISpecializationMethods
{
    DataBaseContext dataBase;

    public SpecializationMethods(DataBaseContext dB)
    {
        dataBase = dB;
    }

    public void AddSpecialization(string name)
    {
        dataBase.Specializations.Add(new Specialization(name));
        dataBase.SaveChanges();
    }
    
    public void UpdateSpecializationName(string newName, int Id)
    {
        dataBase.Specializations.Find(Id).Name = newName;
        var doctor = dataBase.Doctors.Where(c => c.SpecializationID == Id).ToList<Doctor>();
        for (int i = 0; i < doctor.Count; i++)
        {
            dataBase.Doctors.Find(doctor[i].ID).SpecializationName = newName;
        }
        dataBase.SaveChanges();
    }
    
    public int FindSpecializationWithName(string specializationName)
    {
        var specialization = dataBase.Specializations.
            Where(s => s.Name == specializationName).ToList();
        return specialization[0].ID;
    }
    
    public string FindSpecializationWithId(int specializationId)
    {
        string res = null;
        var specialization = dataBase.Specializations.
            Where(s => s.ID == specializationId).ToList();
        if (specialization.Count != 0)
            res = specialization[0].Name;;
        return res;
    }
    
    public void DeleteSpecialization(int Id)
    {
        var doctor = dataBase.Doctors.Where(c => c.SpecializationID == Id).ToList<Doctor>();
        for (int i = 0; i < doctor.Count; i++)
            dataBase.Doctors.Remove(doctor[i]);
        dataBase.Specializations.Remove(dataBase.Specializations.Find(Id));
        dataBase.SaveChanges();
    }
    
    public List<string> SpecializationNamesGet()
    {
        dataBase.Specializations.Load();
        List<string> result = new List<string>(dataBase.Specializations.Local.Count);
        foreach (Specialization specialization in dataBase.Specializations)
            result.Add(specialization.Name);
        return result;
    }
    
    // Как называется специализация (Name), по которой выдан сертификат с заданным идентификатором? 
    public string GetSpecializationByCertificate(int certificateId)
    {
        var res = "No Specialization!";
        if (dataBase.Certificates.Find(certificateId) != null)
        {
            res = dataBase.Certificates.Find(certificateId).Doctor.SpecializationName;
        }
    
        return res;
    }
    
    public BindingList<Specialization> AllSpecializations()
    {
        dataBase?.Specializations?.Load();
        return dataBase.Specializations.Local.ToBindingList();
    }
}