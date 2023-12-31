using System.ComponentModel;
using Core.DTO;
using Core.Model;
using DatabaseModels;

namespace Core;

public interface ICertificateRepository
{
    CertificateDTO Add(certificateDomin CertificateDomin );
    CertificateDTO Update(certificateDomin CertificateDomin);
    CertificateDTO Find(certificateDomin CertificateDomin);
    CertificateDTO Delete(certificateDomin CertificateDomin);
    List<CertificateDTO> GetAll();
}