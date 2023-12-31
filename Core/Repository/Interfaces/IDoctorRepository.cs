using System.ComponentModel;
using Core.DTO;
using Core.Model;
using DatabaseModels;

namespace Core;

public interface IDoctorRepository
{
    DoctorDTO Add(doctorDomain DoctorDomain);
    DoctorDTO Update(doctorDomain DoctorDomain);
    DoctorDTO Find(doctorDomain DoctorDomain);
    DoctorDTO Delete(doctorDomain DoctorDomain);
    List<DoctorDTO> GetAll();
}