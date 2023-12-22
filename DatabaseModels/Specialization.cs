namespace DatabaseModels;

public class Specialization
{
    public int ID { get; set; }
    public string Name { get; set; }

    public Specialization(string name)
    {
        Name = name;
    }
    
    public Specialization(){ }
}