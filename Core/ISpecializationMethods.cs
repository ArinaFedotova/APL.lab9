using System.ComponentModel;
using DatabaseModels;

namespace Core;

public interface ISpecializationMethods
{
    void AddSpecialization(string name);
    void UpdateSpecializationName(string newName, int Id);
    int FindSpecializationWithName(string specializationName);
    string FindSpecializationWithId(int specializationId);
    void DeleteSpecialization(int Id);
    List<string> SpecializationNamesGet();
    string GetSpecializationByCertificate(int certificateId);
    BindingList<Specialization> AllSpecializations();
}