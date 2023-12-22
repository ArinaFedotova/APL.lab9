using System.ComponentModel;
using DatabaseModels;

namespace Core;

public class WorkWithData
{
    private readonly DBCore _dbCore;

    public WorkWithData(DBCore entities)
    {
        _dbCore = entities;
        
    }
    //Doctor
    public void AddDoctorWithSpecializationId(string name, int specializationId)
    {
        try
        {
            _dbCore.doctor.AddDoctor(name, specializationId);
            Console.WriteLine("Doctor added successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Specialization not found for ID: {specializationId}");
        }
    }

    public void AddDoctorWithSpecializationName(string name, string specializationName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(specializationName))
            {
                Console.WriteLine("Specialization name cannot be empty.");
                return;
            }
            
            int specializationId = _dbCore.specialization.FindSpecializationWithName(specializationName);
            AddDoctorWithSpecializationId(name, specializationId);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error adding Doctor: {e.Message}");
        }
    }
    
    public void UpdateDoctorName(string newName, string tmpId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("New Doctor name cannot be empty.");
                return;
            }
            
            int changeId = Convert.ToInt32(tmpId);
            _dbCore.doctor.UpdateDoctorName(newName, changeId);
            Console.WriteLine("Doctor name updated successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating Doctor name: {e.Message}");
        }
    }
    
    public void UpdateDoctorSpecializationWithId(int newSpecializationId, string tmpId)
    {
        if (_dbCore.specialization.FindSpecializationWithId(newSpecializationId) != null)
        {
            Console.WriteLine("Invalid new Specialization ID.");
            return;
        }
        
        try
        {
            int changeId;
            if (int.TryParse(tmpId, out changeId))
            {
                _dbCore.doctor.UpdateDoctorSpecialization(newSpecializationId, changeId);
                Console.WriteLine("Doctor's Specialization updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid Specialization ID. Please enter a valid integer.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating Doctor's Specialization: {e.Message}");
        }
    }

    public void DeleteDoctor(string tmpId)
    {
        try
        {
            int changeId = Convert.ToInt32(tmpId);
            _dbCore.doctor.DeleteDoctor(changeId);
            Console.WriteLine("Doctor deleted successfully!");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Doctor ID. Please enter a valid integer.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message}");
        }
    }

    public string FindDoctor(int doctorId)
    {
        return _dbCore.doctor.FindDoctorWithId(doctorId);
    }

    public List<Doctor> FindDoctorByName(string doctorName)
    {
        var newDoctor = _dbCore.doctor.FindDoctorWithName(doctorName);
        return newDoctor;
    }

    public int CountAllDoctorsWithSpecialization(string specializationId)
    {
        try
        {
            int changeId;
            int.TryParse(specializationId, out changeId);
            return _dbCore.doctor.CountDoctorsWithSpecialization(changeId);
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid Specialization ID. Please enter a valid integer.");
        }
        return -1;
    }
    public BindingList<Doctor> TakeAllDoctors()
    {
        return _dbCore.doctor.AllDoctors();
    }
    
    
    // Specialization
    public List<string> GetSpecializationNames()
    {
        return _dbCore.specialization.SpecializationNamesGet();
    }
    
    public int FindSpecializationWithName(string name)
    {
        return _dbCore.specialization.FindSpecializationWithName(name);
    }
    
    public string FindSpecializationWithId(int id)
    {
        return _dbCore.specialization.FindSpecializationWithId(id);
    }
    
    public void AddSpecialization(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Specialization name cannot be empty.");
            return;
        }
        
        try
        {
            _dbCore.specialization.AddSpecialization(name);
            Console.WriteLine("Specialization added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding Specialization: {ex.Message}");
        }
    }
    
    public void UpdateSpecializationName(string newName, string tmpId)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            Console.WriteLine("New Specialization's Name cannot be empty.");
            return;
        }
        
        try
        {
            int changeId = Convert.ToInt32(tmpId);
            _dbCore.specialization.UpdateSpecializationName(newName, changeId);
            Console.WriteLine("Specialization's Name updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating Specialization name: {ex.Message}");
        }
    }
    
    public void DeleteSpecialization(string tmpId)
    {
        try
        {
            int changeId = Convert.ToInt32(tmpId);
            _dbCore.specialization.DeleteSpecialization(changeId);
            Console.WriteLine("Specialization deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Invalid Specialization ID. Please enter a valid integer.");
        }
    }
    public void GetSpecializationByCertificate(string certificateId)
    {
        int id;
        if (int.TryParse(certificateId, out id))
        {
            Console.WriteLine(_dbCore.specialization.GetSpecializationByCertificate(id));
        }
        else
        {
            Console.WriteLine("Invalid Certificate ID. Please enter a valid integer.");
        }
    }
    public BindingList<Specialization> TakeAllSpecializations()
    {
        return _dbCore.specialization.AllSpecializations();
    }
    
    
    // Certificate
    public void AddCertificateWithDoctorId(DateTime date, string description, int doctorId)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Certificate's Description cannot be empty");
            return;
        }
    
        try
        {
            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            _dbCore.certificate.AddCertificate(description, date, doctorId);
            Console.WriteLine("Certificate added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding Certificate: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }
    
    public void UpdateCertificateDate(DateTime time, string tmpId)
    {
        try
        {
            int changeId;
            if (int.TryParse(tmpId, out changeId))
            {
                _dbCore.certificate.UpdateCertificateDate(time, changeId);
                Console.WriteLine("Certificate's date updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid Certificate ID. Please enter a valid integer.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating group time: {e.Message}");
        }
    }
    
    public void UpdateCertificateDescription(string newDescription, string tmpId)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
        {
            Console.WriteLine("Certificate's Description cannot be empty");
            return;
        }
        
        try
        {
            int changeId;
            if (int.TryParse(tmpId, out changeId))
            {
                _dbCore.certificate.UpdateCertificateDescription(newDescription, changeId);
                Console.WriteLine("Certificate Description updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid Certificate ID. Please enter a valid integer.");
            }
        }
        catch (Exception e)
        { 
            Console.WriteLine($"Error updating Certificate name: {e.Message}");
        }
    }
    
    public void UpdateCertificateDoctorWithId(int newDoctorId, string tmpId)
    {
        if (_dbCore.doctor.FindDoctorWithId(newDoctorId) == null)
        {
            Console.WriteLine("Invalid new Doctor ID.");
            return;
        }
        
        try
        {
            int changeId;
            if (int.TryParse(tmpId, out changeId))
            {
                _dbCore.certificate.UpdateCertificateDoctor(changeId, newDoctorId);
                Console.WriteLine("Certificate's Doctor updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid Certificate ID. Please enter a valid integer.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating Certificate's doctor: {e.Message}");
        }
    }
    
    public void DeleteCertificate(string tmpId)
    {
        try
        {
            int changeId = Convert.ToInt32(tmpId);
            _dbCore.certificate.DeleteCertificate(changeId);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Certificate ID. Please enter a valid integer.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message}");
        }
    }
    
    public void FindLastCertificateDoctor(int doctorId)
    {
        try
        {
            Console.WriteLine($"Doctor's ({_dbCore.doctor.FindDoctorWithId(doctorId)})" +
                              $"Latest Certificate was at " +
                              $"{_dbCore.certificate.LastCertificate(doctorId)}");
        }
        catch (Exception e)
        {
            Console.WriteLine("Invalid Doctor ID. Please enter a valid integer.");
        }
    }
    
    public bool FindCertificateWithId(int id)
    {
        return _dbCore.certificate.FindCertificateWithId(id);
    }
    
    public BindingList<Certificate> TakeAllCertificates()
    {
        return _dbCore.certificate.AllCertificates();
    }
}