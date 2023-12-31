using Core.DTO;
using Core.Model;
using DatabaseModels;

namespace Core.Handler;

public static class ConverterDomain
{
    public static doctorDomain ToDomain(DoctorDTO doctorDto)
    {
        doctorDomain DoctorDomain = new doctorDomain();
        DoctorDomain.ID = doctorDto.ID;
        DoctorDomain.Name = doctorDto.Name;
        DoctorDomain.SpecializationName = doctorDto.SpecializationName;
        DoctorDomain.SpecializationID = doctorDto.SpecializationID;
        DoctorDomain.Specialization = doctorDto.Specialization;
        return DoctorDomain;
    }
    
    public static certificateDomin ToDomain(CertificateDTO certificateDto)
    {
        certificateDomin CertificateDomain = new certificateDomin();
        CertificateDomain.ID = certificateDto.ID;
        CertificateDomain.Description = certificateDto.Description;
        CertificateDomain.Date = certificateDto.Date;
        CertificateDomain.DoctorID = certificateDto.DoctorID;
        CertificateDomain.DoctorName = certificateDto.DoctorName;
        CertificateDomain.Doctor = certificateDto.Doctor;
        return CertificateDomain;
    }
    
    public static specializationDomain ToDomain(SpecializationDTO specializationDto)
    {
        specializationDomain SpecializationDomain = new specializationDomain();
        SpecializationDomain.ID = specializationDto.ID;
        SpecializationDomain.Name = specializationDto.Name;
        return SpecializationDomain;
    }
    
    public static DoctorDTO FromDomain(Doctor doctor)
    {
        DoctorDTO doctorDto = new DoctorDTO();
        doctorDto.ID = doctor.ID;
        doctorDto.Name = doctor.Name;
        doctorDto.SpecializationName = doctor.SpecializationName;
        doctorDto.SpecializationID = doctor.SpecializationID;
        doctorDto.Specialization = doctor.Specialization;
        return doctorDto;
    }
    
    public static CertificateDTO FromDomain(Certificate certificate)
    {
        CertificateDTO certificateDto = new CertificateDTO();
        certificateDto.ID = certificate.ID;
        certificateDto.Description = certificate.Description;
        certificateDto.Date = certificate.Date;
        certificateDto.DoctorID = certificate.DoctorID;
        certificateDto.DoctorName = certificate.DoctorName;
        certificateDto.Doctor = certificate.Doctor;
        return certificateDto;
    }
    
    public static SpecializationDTO FromDomain(Specialization specialization)
    {
        SpecializationDTO specializationDto = new SpecializationDTO();
        specializationDto.ID = specialization.ID;
        specializationDto.Name = specialization.Name;
        return specializationDto;
    }
    
}