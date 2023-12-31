using DatabaseModels;

namespace Core.DTO;

public class CertificateDTO
{
    public int ID { get; set; }
    public int DoctorID { get; set; }
    public string DoctorName { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; } = DateTime.MinValue;
    
    public Doctor Doctor { get; set; }
    
    public override string ToString()
    {
        return $"Certificate: ID: {ID}, Date: {Date}, Doctor ID: {DoctorID}, " +
               $"Doctor: {DoctorName}, Description: {Description}";
    }
}