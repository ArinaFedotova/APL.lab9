namespace DatabaseModels;

public class Certificate
{
    public int ID { get; set; }
    public int DoctorID { get; set; }
    public string DoctorName { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public Doctor Doctor { get; set; }

    public Certificate(string description, Doctor doctor, DateTime date)
    {
        Date = date;
        Description = description;
        Doctor = doctor;
        DoctorName = doctor.Name;
        DoctorID = doctor.ID;
    }
    
    public Certificate() { }
}