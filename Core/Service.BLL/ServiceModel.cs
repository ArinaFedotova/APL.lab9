using System.Runtime.ConstrainedExecution;
using Core.DTO;
using DatabaseModels;

namespace Core;

public class ServiceModel
{
    public SpecializationDTO SpecializationDto;
    public CertificateDTO CertificateDto;
    public DoctorDTO DoctorDto;
   
    public ServiceModel(
        int cId, DateTime cDate, int cDoctorId, string cDescription, 
        int dId, string dName, int dSpecializationId, string dSpecializationName,
        int sId, string sName)
    {
        CertificateDto.ID = cId;
        CertificateDto.DoctorID = cDoctorId;
        CertificateDto.Description = cDescription;
        CertificateDto.Date = cDate;
        
        DoctorDto.ID = dId;
        DoctorDto.Name = dName;
        DoctorDto.SpecializationID = dSpecializationId;
        DoctorDto.SpecializationName = dSpecializationName;

        SpecializationDto.ID = sId;
        SpecializationDto.Name = sName;
    }
}