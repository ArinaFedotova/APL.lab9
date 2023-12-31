using DatabaseModels;

namespace Core.Model;

public class doctorDomain
{
    public int? ID { get; set; }
    public string? Name { get; set; }
    public int? SpecializationID { get; set; }
    public string? SpecializationName { get; set; }
    public Specialization? Specialization { get; set; }
}