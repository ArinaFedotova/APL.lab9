namespace DatabaseModels;

public class Certificate
{
    public int ID { get; set; }
    public int DoctorID { get; set; }
    public string DoctorName { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; } = DateTime.MinValue;
    public Doctor Doctor { get; set; }

    public Certificate(string description, string doctorName, int doctorId, DateTime date)
    {
        Date = date;
        Description = description;
        DoctorName = doctorName;
        DoctorID = doctorId;
    }
    
    public Certificate() { }
    
}