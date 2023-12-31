using Core.DTO;
using DatabaseContext;
using DatabaseModels;

namespace Core.Handler;

public static class ConverterRepository
{
    public static RepositoryModel ToRepository(
        DoctorDTO? doctor = null,
        SpecializationDTO? specialization = null,
        CertificateDTO? certificate = null)
    {
        RepositoryModel repositoryModel = new RepositoryModel();
        if (doctor != null)
        {
            repositoryModel.doctor = new Doctor();
            repositoryModel.doctor.ID = doctor.ID;
            repositoryModel.doctor.SpecializationID = doctor.SpecializationID;
            repositoryModel.doctor.Name = doctor.Name;
            repositoryModel.doctor.SpecializationName = doctor.SpecializationName;
        }

        if (certificate != null)
        {
            repositoryModel.certificate = new Certificate();
            repositoryModel.certificate.ID = certificate.ID;
            repositoryModel.certificate.DoctorID = certificate.DoctorID;
            repositoryModel.certificate.DoctorName = certificate.DoctorName;
            repositoryModel.certificate.Date = certificate.Date;
            repositoryModel.certificate.Description = certificate.Description;
        }

        if (specialization != null)
        {
            repositoryModel.specialization = new Specialization();
            repositoryModel.specialization.ID = specialization.ID;
            repositoryModel.specialization.Name = specialization.Name;
        }
        return repositoryModel;
    }

    public static DoctorDTO FromRepository(Doctor doctor)
    {
        DoctorDTO doctorDto = new DoctorDTO();
        doctorDto.ID = doctor.ID;
        doctorDto.SpecializationID = doctor.SpecializationID;
        doctorDto.SpecializationName = doctor.SpecializationName;
        doctorDto.Name = doctor.Name;

        return doctorDto;
    }
    
    public static SpecializationDTO FromRepository(Specialization specialization)
    {
        SpecializationDTO specializationDto = new SpecializationDTO();
        specializationDto.ID = specialization.ID;
        specializationDto.Name = specialization.Name;

        return specializationDto;
    }
    
    public static CertificateDTO FromRepository(Certificate certificate)
    {
        CertificateDTO certificateDto = new CertificateDTO();
        certificateDto.ID = certificate.ID;
        certificateDto.Date = certificate.Date;
        certificateDto.Description = certificate.Description;
        certificateDto.DoctorID = certificate.DoctorID;
        certificateDto.DoctorName = certificate.DoctorName;

        return certificateDto;
    }
}