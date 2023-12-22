using System.ComponentModel;
using DatabaseModels;

namespace Core;

public interface IDoctorMethods
{
    void AddDoctor(string name, int specializationId);
    void UpdateDoctorName(string name, int Id);
    void UpdateDoctorSpecialization(int specializationId, int doctorId);
    string FindDoctorWithId(int doctorId);
    List<Doctor> FindDoctorWithName(string doctorName);
    void DeleteDoctor(int Id);
    int CountDoctorsWithSpecialization(int specializationId);
    BindingList<Doctor> AllDoctors();
}