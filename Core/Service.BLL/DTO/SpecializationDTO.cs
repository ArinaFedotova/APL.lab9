namespace Core.DTO;

public class SpecializationDTO
{
    public int ID { get; set; }
    public string Name { get; set; }
    
    public override string ToString()
    {
        return  $"Specialization: ID: {ID}, Name: {Name}";
    }
}