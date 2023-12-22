using Microsoft.EntityFrameworkCore;

namespace DatabaseModels;
// модели базы данных
public class Doctor
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int SpecializationID { get; set; }
    public string SpecializationName { get; set; }
    public Specialization Specialization { get; set; }

    public Doctor(string name, Specialization specialization)
    {
        Name = name;
        Specialization = specialization;
        SpecializationName = specialization.Name;
        SpecializationID = specialization.ID;
    }
    
    public Doctor(){ }
}