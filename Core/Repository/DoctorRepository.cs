using System.ComponentModel;
using Core.DTO;
using Core.Handler;
using Core.Model;
using DatabaseContext;
using DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class DoctorRepository : IDoctorRepository
{
    DataBaseContext dataBase;

    public DoctorRepository(DataBaseContext dB)
    {
        dataBase = dB;
    }
    
    public DoctorDTO Add(doctorDomain DoctorDomain)
    {
        if (DoctorDomain.Name == null || DoctorDomain.SpecializationID == null || 
            DoctorDomain.SpecializationName == null)
            throw new Exception("Not enough data to create Doctor");
        Doctor newDoctor = new Doctor(DoctorDomain.Name, dataBase.Specializations!
            .Find(DoctorDomain.SpecializationID)!);
        var doc = dataBase.Doctors.Add(newDoctor);
        dataBase.SaveChanges();
        return ConverterDomain.FromDomain(doc.Entity);
    }
    
    public DoctorDTO Update(doctorDomain DoctorDomain)
    {
        Doctor doctorToUp = null;
        if (DoctorDomain.Name == null || DoctorDomain.SpecializationID == -1)
            throw new Exception("Not enough data of Doctor to update.");
        if (dataBase.Doctors != null)
        {
            doctorToUp = dataBase.Doctors.Find(DoctorDomain.ID);
            if (doctorToUp == null)
                throw new Exception("Doctor not found.");
            doctorToUp.Name = DoctorDomain.Name;

            if (dataBase.Specializations!.Find(DoctorDomain.SpecializationID) == null)
                throw new Exception("Specialization not found.");
            
            doctorToUp.SpecializationID = (int)DoctorDomain.SpecializationID!;
            doctorToUp.SpecializationName = DoctorDomain.SpecializationName!;
            doctorToUp.Specialization = DoctorDomain.Specialization!;
        }
        else
            throw new Exception("Doctors are empty!");

        dataBase.SaveChanges();
        return ConverterDomain.FromDomain(doctorToUp);
    }
    
    public DoctorDTO Find(doctorDomain DoctorDomain)
    {
        List<Doctor> doctor = dataBase.Doctors.Where(doctor => doctor.ID == DoctorDomain.ID)
            .Include(doctor => doctor.Specialization).ToList();
        if (doctor.Count == 0)
            throw new Exception($"No Doctor was found with ID: " +
                                $"{DoctorDomain.ID}");
        return ConverterDomain.FromDomain(doctor.First());
    }
    public DoctorDTO Delete(doctorDomain DoctorDomain)
    {
        Doctor doctorToDel = null;
        if (dataBase.Doctors != null)
        {
            doctorToDel = dataBase.Doctors.Find(DoctorDomain.ID);
            if (doctorToDel == null)
                throw new Exception("Doctor not found.");
            dataBase.Doctors.Remove(doctorToDel);
        }

        dataBase.SaveChanges();
        return ConverterDomain.FromDomain(doctor: doctorToDel);
    }

    public List<DoctorDTO> GetAll()
    {
        var lst = dataBase.Doctors.Include(doc => doc.Specialization).ToList();
        List<DoctorDTO> certificateDtos = new List<DoctorDTO>();
        foreach (var elem in lst)
        {
            certificateDtos.Add(ConverterRepository.FromRepository(elem));
        }
        return certificateDtos;
    }
}