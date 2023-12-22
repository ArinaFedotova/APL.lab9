using System.ComponentModel;
using DatabaseContext;
using DatabaseModels;

namespace Core;
// интерфейсы и реализации фасада и основных классов, работающих  с бизнес логикой
public class DBCore
{
    public IDoctorMethods doctor;
    public ISpecializationMethods specialization;
    public ICertificateMethods certificate;

    public DBCore(DatabaseContext.DataBaseContext dataBase)
    {
        doctor = new DoctorMethods(dataBase);
        specialization = new SpecializationMethods(dataBase);
        certificate = new CertificateMethods(dataBase);
    }
}