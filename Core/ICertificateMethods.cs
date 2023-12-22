using System.ComponentModel;
using DatabaseModels;

namespace Core;

public interface ICertificateMethods
{
    void AddCertificate(string description, DateTime date, int doctorId);
    void UpdateCertificateDoctor(int certificateId, int doctorId);
    void UpdateCertificateDescription(string newDescription, int Id);
    void UpdateCertificateDate(DateTime newDate, int Id);
    bool FindCertificateWithId(int certificateId);
    DateTime LastCertificate(int doctorId);
    void DeleteCertificate(int Id);
    BindingList<Certificate> AllCertificates();
}