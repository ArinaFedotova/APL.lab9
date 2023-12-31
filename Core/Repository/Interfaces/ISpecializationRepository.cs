using System.ComponentModel;
using Core.DTO;
using Core.Model;
using DatabaseModels;

namespace Core;

public interface ISpecializationRepository
{
    SpecializationDTO Add(specializationDomain SpecializationDomain);
    SpecializationDTO Update(specializationDomain SpecializationDomain);
    SpecializationDTO Find(specializationDomain SpecializationDomain);
    SpecializationDTO Delete(specializationDomain SpecializationDomain);
    List<SpecializationDTO> GetAll();
}