using Core;
using DatabaseContext;

namespace Presentation;

public class Facade
{
    private readonly DatabaseContext.DataBaseContext _dataBaseContext;
    private readonly Repository _dataBaseCore;
    private readonly WorkWithData _workWithData;
    private readonly DoctorMenu _doctorMenu;
    private readonly SpecializationMenu _specializationMenu;
    private readonly CertificateMenu _certificateMenu;

    public Facade()
    {
        _dataBaseContext = new DataBaseContext();
        _dataBaseCore = new Repository(_dataBaseContext);
        _workWithData = new WorkWithData(_dataBaseCore);
        _certificateMenu = new CertificateMenu(_workWithData);
        _doctorMenu = new DoctorMenu(_workWithData, _certificateMenu);
        _specializationMenu = new SpecializationMenu(_workWithData, _doctorMenu);
    }

    public void Run()
    {
        Console.WriteLine("START PROGRAM");
        while (true)
        {
            Console.WriteLine("\nMenu: ");
            Console.WriteLine("1. Doctor menu");
            Console.WriteLine("2. Specialization menu");
            Console.WriteLine("3. Certificate menu");
            Console.WriteLine("4. Exit");
                
            Console.Write("Select an action: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _doctorMenu.Run();
                    break;

                case "2":
                    _specializationMenu.Run();
                    break;

                case "3":
                    _certificateMenu.Run();
                    break;

                case "4":
                    Console.WriteLine("END PROGRAM.");
                    return;

                default:
                    Console.WriteLine("Wrong input. Please try again.");
                    break;
            }
        }
    }
}