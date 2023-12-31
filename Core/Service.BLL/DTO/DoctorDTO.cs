using DatabaseModels;

namespace Core.DTO;

public class DoctorDTO
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int SpecializationID { get; set; }
    public string SpecializationName { get; set; }
    
    public Specialization Specialization { get; set; }
    
    public override string ToString()
    {
        return  $"Doctor: ID: {ID}, Specialization ID: {SpecializationID}, " +
                $"Name: {Name}, Specialization: {SpecializationName}.";
    }
}