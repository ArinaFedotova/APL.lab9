using System.ComponentModel;
using Core.DTO;
using Core.Handler;
using DatabaseModels;

namespace Core;

public class WorkWithData
{
    private readonly Repository _repository;
    

    public WorkWithData(Repository entities)
    {
        _repository = entities;
        
    }
    //Doctor
    public DoctorDTO? AddDoctor(DoctorDTO doctorDto)
    {
        DoctorDTO doctor = null;
        try
        {
            SpecializationDTO specializationDto = new SpecializationDTO();
            specializationDto.ID = doctorDto.SpecializationID;

            SpecializationDTO specialization =
                _repository.specialization.Find(ConverterDomain.ToDomain(specializationDto));
            doctorDto.SpecializationID = specialization.ID;
            doctorDto.SpecializationName = specialization.Name;
            
            doctor = _repository.doctor.Add(ConverterDomain.ToDomain(doctorDto));
            doctorDto.ID = doctor.ID;
            Console.WriteLine($"{doctor} added successfully!");
            return doctorDto;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Specialization not found. {e.Message}");
            Console.WriteLine(e);
            return null;
        }
    }
    
    public void UpdateDoctor(DoctorDTO doctorDto)
    {
        try
        {
            SpecializationDTO specializationDto = new SpecializationDTO();
            specializationDto.ID = doctorDto.SpecializationID;
            _repository.specialization.Find(ConverterDomain.ToDomain(specializationDto));
            
            DoctorDTO doctor = _repository.doctor.Update(ConverterDomain.ToDomain(doctorDto));
            Console.WriteLine($"Doctor: {doctor} updated successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating Doctor name: {e.Message}");
        }
    }

    public void DeleteDoctor(DoctorDTO doctorDto)
    {
        try
        {
            DoctorDTO doctor = _repository.doctor.Delete(ConverterDomain.ToDomain(doctorDto));
            foreach (var certificate in _repository.certificate.GetAll())
            {
                if (certificate.DoctorID == doctor.ID)
                    _repository.certificate.Delete(ConverterDomain.ToDomain(certificate));
            }
            Console.WriteLine("Doctor deleted successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Doctor not found! {e.Message}");
        }
    }

    public void FindDoctor(DoctorDTO doctorDto)
    {
        try
        {
            DoctorDTO doctor = _repository.doctor.Find(
                ConverterDomain.ToDomain(doctorDto));
            Console.WriteLine($"Doctor: {doctor}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Doctor not found: {e.Message}");
        }
    }
    
    public  List<DoctorDTO> TakeAllDoctors()
    {
        List<DoctorDTO> doctors = _repository.doctor.GetAll();
        Console.WriteLine("Doctors: ");
        foreach (DoctorDTO doctor in doctors)
        {
            Console.WriteLine($"{doctor}");
        }

        return doctors;
    }
    
    
    // Specialization
    public void GetSpecializations()
    {
        List<SpecializationDTO> specializations = _repository.specialization.GetAll();
        Console.WriteLine("Specializations: ");
        foreach (SpecializationDTO specialization in specializations)
        {
            Console.WriteLine($"{specialization}");
        }
    }
    
    public void FindSpecialization(SpecializationDTO specializationDto)
    {
        try
        {
            var specialization = _repository.specialization.Find(ConverterDomain.ToDomain(specializationDto));
            Console.WriteLine($"{specialization}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Specialization not found: {e.Message}");
        }
    }
    
    public SpecializationDTO AddSpecialization(SpecializationDTO specializationDto)
    {
        SpecializationDTO specialization = null;
        try
        {
            Console.WriteLine();
            specialization = _repository.specialization.Add
                (ConverterDomain.ToDomain(specializationDto));
            Console.WriteLine($"{specialization} was added");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding Specialization: {ex.Message}");
        }

        return specialization;
    }
    
    public void UpdateSpecialization(SpecializationDTO specializationDto)
    {
        try
        {
            Console.WriteLine(specializationDto);
            SpecializationDTO specialization = _repository.specialization.Update(
                ConverterDomain.ToDomain(specializationDto));
            Console.WriteLine($"{specialization.ID} was updated");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating Specialization name: {e.Message}");
            Console.WriteLine(e);
        }
    }
    
    public void DeleteSpecialization(SpecializationDTO specializationDto)
    {
        try
        {
            SpecializationDTO specialization = _repository.specialization.Delete
                (ConverterDomain.ToDomain(specializationDto));
            foreach (var doctor in _repository.doctor.GetAll())
            {
                if (doctor.SpecializationID == specialization.ID)
                    _repository.doctor.Delete(ConverterDomain.ToDomain(doctor));
            }
            Console.WriteLine($"{specialization} was removed");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Specialization not found, create before using it: {e.Message}");
        }
    }
    public List<SpecializationDTO> TakeAllSpecializations()
    {
        List<SpecializationDTO> specializations = _repository.specialization.GetAll();
        foreach (SpecializationDTO specialization in specializations)
        {
            Console.WriteLine($"{specialization}");
        }

        return specializations;
    }
    
    
    // Certificate
    public void AddCertificate(CertificateDTO certificateDto)
    {
        try
        {
            DoctorDTO doctorDto = new DoctorDTO();
            Console.WriteLine($"{certificateDto.DoctorID}");
            doctorDto.ID = certificateDto.DoctorID;
            DoctorDTO doctor = _repository.doctor.Find
                (ConverterDomain.ToDomain(doctorDto));
            certificateDto.DoctorID = doctor.ID;
            certificateDto.DoctorName = doctor.Name;
            CertificateDTO certificate = _repository.certificate.Add
                (ConverterDomain.ToDomain(certificateDto));
            Console.WriteLine($"{certificate} was added");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Doctor not found, create before using it: {e.Message}");
            Console.WriteLine(e.InnerException);
        }
    }
    
    public void UpdateCertificate(CertificateDTO certificateDto)
    {
        try
        {
            DoctorDTO doctorDto = new DoctorDTO();
            doctorDto.ID = certificateDto.DoctorID;
            
            _repository.doctor.Find(ConverterDomain.ToDomain(doctorDto));
            CertificateDTO certificate = _repository.certificate.Update
                (ConverterDomain.ToDomain(certificateDto));
            Console.WriteLine($"{certificate} was updated");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Doctor not found, create before using it: {e.Message}");
        }
    }
    
    public void DeleteCertificate(CertificateDTO certificateDto)
    {
        try
        {
            bool dependency = false;
            CertificateDTO certificate = _repository.certificate.Delete
                (ConverterDomain.ToDomain(certificateDto));
            foreach (var cert in _repository.certificate.GetAll())
            {
                if (cert.DoctorID == certificate.DoctorID)
                {
                    dependency = true;
                    break;
                }
            }

            DoctorDTO doctorDto = new DoctorDTO();
            doctorDto.ID = certificateDto.DoctorID;
            if (!dependency)
            {
                _repository.doctor.Delete(ConverterDomain.ToDomain(doctorDto));
            }
            Console.WriteLine($"{certificate} was removed");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Certificate not found, create before using it: {e.Message}");
        }
    }
    
    public List<CertificateDTO> TakeAllCertificates()
    {
        List<CertificateDTO> certificates = _repository.certificate.GetAll();
        Console.WriteLine("Certificates: ");
        foreach (CertificateDTO certificate in certificates)
        {
            Console.WriteLine($"{certificate}");
        }
        return certificates;
    }
    
    public void GetAllDoctorsWithSpecialization(SpecializationDTO specializationDto)
    {
        try
        {
            int count = 0;
            SpecializationDTO specialization = _repository.specialization.Find
                (ConverterDomain.ToDomain(specializationDto));
            foreach (var doctor in _repository.doctor.GetAll())
            {
                if(doctor.SpecializationID == specializationDto.ID)
                {
                    Console.WriteLine($"Doctor {doctor} has current Specialization");
                    count++;
                }
            }
            Console.WriteLine($"{count} doctors has current Specialization {specialization}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Specialization not found: {e.Message}");
        }
    }

    public void GetSpecializationNameByCertificate(CertificateDTO certificateDto)
    {
        try
        {
            CertificateDTO certificate = _repository.certificate.Find
                (ConverterDomain.ToDomain(certificateDto));

            DoctorDTO doctorDto = new DoctorDTO();
            doctorDto.ID = certificate.DoctorID;
            
            DoctorDTO doctor = _repository.doctor.Find
                (ConverterDomain.ToDomain(doctorDto));
            Specialization specialization = doctor.Specialization!;

            Console.WriteLine($"Certificate {certificate} has been given by Specialization {specialization}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Certificate Record not found: {e.Message}");
        }
    }

    public void FindLastCertificateDoctor(DoctorDTO doctorDto)
    {
        try
        {
            DoctorDTO doctor = _repository.doctor.Find
                (ConverterDomain.ToDomain(doctorDto));
            CertificateDTO lastCertificate = null!;
            
            foreach (var cert in _repository.certificate.GetAll())
            {
                if (cert.DoctorID == doctor.ID)
                {
                    lastCertificate = cert;
                }
            }
            Console.WriteLine($"Last given Certificate to Doctor is {lastCertificate}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Doctor Record not found: {e.Message}");
        }
    }
}