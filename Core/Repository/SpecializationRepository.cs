using System.ComponentModel;
using Core.DTO;
using Core.Handler;
using Core.Model;
using DatabaseContext;
using DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class SpecializationRepository : ISpecializationRepository
{
    DataBaseContext dataBase;

    public SpecializationRepository(DataBaseContext dB)
    {
        dataBase = dB;
    }
    
    public SpecializationDTO Add(specializationDomain SpecializationDomain)
    {
        if (SpecializationDomain.Name == null)
            throw new Exception("Not enough data to create Specialization");
        Specialization newSpecialization = new Specialization(SpecializationDomain.Name);
        var spec = dataBase.Specializations.Add(newSpecialization);
        dataBase.SaveChanges();
        return ConverterDomain.FromDomain(spec.Entity);
    }

    public SpecializationDTO Update(specializationDomain SpecializationDomain)
    {
        Specialization specializationToUp = null;
        if (SpecializationDomain.Name == null)
            throw new Exception("Name of Specialization cant be empty.");
        if (dataBase.Specializations != null)
        {
            specializationToUp = dataBase.Specializations.Find(SpecializationDomain.ID)!;
            if (specializationToUp == null)
                throw new Exception("Specialization not found.");
            specializationToUp.Name = SpecializationDomain.Name;
        }
        else
            throw new Exception("Specializations are empty!");

        dataBase.SaveChanges();
        return ConverterDomain.FromDomain(specializationToUp);
    }

    public SpecializationDTO Find(specializationDomain SpecializationDomain)
    {
        var specialization = dataBase.Specializations!.
            Where(c => c.ID == SpecializationDomain.ID).ToList();
        if (specialization.Count == 0)
            throw new Exception($"No Specialization was found with ID: " +
                                $"{SpecializationDomain.ID}");
        
        return ConverterDomain.FromDomain(specialization.First());
    }

    public SpecializationDTO Delete(specializationDomain SpecializationDomain)
    {
        Specialization specializationToDel = null;
        if (dataBase.Specializations != null)
        {
            specializationToDel = dataBase.Specializations.Find(SpecializationDomain.ID)!;
            if (specializationToDel == null)
                throw new InvalidOperationException("Specialization not found.");
            dataBase.Specializations.Remove(specializationToDel); 
        }
        dataBase.SaveChanges();
        return ConverterDomain.FromDomain(specializationToDel);
    }

    public List<SpecializationDTO> GetAll()
    {
        var lst = dataBase.Specializations.ToList();
        List<SpecializationDTO> specializationDtos = new List<SpecializationDTO>();
        foreach (var elem in lst)
        {
            specializationDtos.Add(ConverterRepository.FromRepository(elem));
        }
        return specializationDtos;
    }
    
}