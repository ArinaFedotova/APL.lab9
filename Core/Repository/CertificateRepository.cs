using System.ComponentModel;
using Core.DTO;
using Core.Handler;
using Core.Model;
using DatabaseContext;
using DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class CertificateRepository : ICertificateRepository
{
    DataBaseContext dataBase;

    public CertificateRepository(DataBaseContext dB)
    {
        dataBase = dB ?? throw new ArgumentNullException(nameof(dB));
    }

    public CertificateDTO Add(certificateDomin CertificateDomin)
    {
        if (CertificateDomin.Description == null || CertificateDomin.Date == null 
            || CertificateDomin.DoctorID == -1 || CertificateDomin.DoctorName == null)
            throw new Exception("Not enough data to create Doctor");
        Certificate newCertificate = new Certificate(CertificateDomin.Description, 
            CertificateDomin.DoctorName, CertificateDomin.DoctorID, CertificateDomin.Date);
        newCertificate.Date = newCertificate.Date.SetKindUtc();
        var cert = dataBase.Certificates!.Add(newCertificate);
        
        dataBase.SaveChanges();
        return ConverterDomain.FromDomain(cert.Entity);
    }

    public CertificateDTO Update(certificateDomin CertificateDomin)
    {
        Certificate certificateToUp = null;
        if (CertificateDomin.Description == null || CertificateDomin.DoctorID == -1 
                                                 || CertificateDomin.Date == null)
            throw new Exception("Not enough data of Certificate to update.");
        if (dataBase.Certificates != null)
        {
            certificateToUp = dataBase.Certificates.Find(CertificateDomin.ID)!;
            if (certificateToUp == null)
                throw new Exception("Certificate not found.");
            certificateToUp.Description = CertificateDomin.Description;
            certificateToUp.Date = CertificateDomin.Date;
            
            if (dataBase.Doctors!.Find(CertificateDomin.DoctorID) == null)
                throw new Exception("Doctor not found.");
            
            certificateToUp.DoctorID = CertificateDomin.DoctorID;
            certificateToUp.DoctorName = CertificateDomin.DoctorName;
            certificateToUp.Doctor = CertificateDomin.Doctor;
        }
        else
            throw new Exception("Certificates are empty!");

        dataBase.SaveChanges();
        return ConverterDomain.FromDomain(certificateToUp);
    }
    
    public CertificateDTO Find(certificateDomin CertificateDomin)
    {
        var certificate = dataBase.Certificates!.
            Where(c => c.ID == CertificateDomin.ID).ToList();
        if (certificate.Count == 0)
            throw new Exception($"No Certificate was found with ID: " +
                                $"{CertificateDomin.DoctorID}");
        return ConverterDomain.FromDomain(certificate.First());
    }
    public CertificateDTO Delete(certificateDomin CertificateDomin)
    {
        Certificate certificateToDel = null;
        if (dataBase.Certificates != null)
        {
            certificateToDel = dataBase.Certificates.Find(CertificateDomin.ID)!;
            if (certificateToDel == null)
                throw new Exception("Certificate not found.");
            dataBase.Certificates.Remove(certificateToDel);
        }

        dataBase.SaveChanges();
        return ConverterDomain.FromDomain(certificate: certificateToDel);
    }
    public List<CertificateDTO> GetAll()
    {
        var lst = dataBase.Certificates.Include(cert => cert.Doctor).ToList();
        List<CertificateDTO> certificateDtos = new List<CertificateDTO>();
        foreach (var elem in lst)
        {
            certificateDtos.Add(ConverterRepository.FromRepository(elem));
        }
        return certificateDtos;
    }

}