using DatabaseModels;

namespace Core.Model;

public class certificateDomin
{
    public int ID { get; set; }
    public int DoctorID { get; set; }
    public string DoctorName { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; } = DateTime.MinValue;
    public Doctor Doctor { get; set; }
}