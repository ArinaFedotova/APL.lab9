using System.ComponentModel;
using DatabaseContext;
using DatabaseModels;

namespace Core;
// интерфейсы и реализации фасада и основных классов, работающих  с бизнес логикой
public class Repository
{
    public IDoctorRepository doctor;
    public ISpecializationRepository specialization;
    public ICertificateRepository certificate;

    public Repository(DatabaseContext.DataBaseContext dataBase)
    {
        doctor = new DoctorRepository(dataBase);
        specialization = new SpecializationRepository(dataBase);
        certificate = new CertificateRepository(dataBase);
    }
}