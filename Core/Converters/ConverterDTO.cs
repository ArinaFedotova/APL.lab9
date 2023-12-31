using Core.DTO;
using DatabaseModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Handler;

static public class ConverterDTO
{
    public static SpecializationDTO ToDto(
        int specializationId = -1,
        string? specializationName = null)
    {
        SpecializationDTO specializationDto = new SpecializationDTO();
        specializationDto.ID = specializationId;
        specializationDto.Name = specializationName;

        return specializationDto;
    }
    public static CertificateDTO ToDto(
        int certificateId = -1,
        int certificateDoctorId = -1,
        string? certificateDescription = null,
        DateTime certificateDate = new DateTime(),
        string? certificateDoctorName = null)
    {
        CertificateDTO certificateDto = new CertificateDTO();
        certificateDto.ID = certificateId;
        certificateDto.DoctorID = certificateDoctorId;
        certificateDto.Description = certificateDescription;
        certificateDto.Date = certificateDate;
        certificateDto.DoctorName = certificateDoctorName;

        return certificateDto;
    }
    
    public static DoctorDTO ToDTO(
        int doctorId = -1,
        int doctorSpecializationId = -1,
        string? doctorName = null,
        string? doctorSpecializationName = null)
    {
        DoctorDTO doctorDto = new DoctorDTO();
        doctorDto.ID = doctorId;
        doctorDto.SpecializationID = doctorSpecializationId;
        doctorDto.Name = doctorName;
        doctorDto.SpecializationName = doctorSpecializationName;
        return doctorDto;
    }
}