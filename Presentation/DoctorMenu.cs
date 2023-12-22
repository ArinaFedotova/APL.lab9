using Core;

namespace Presentation;

public class DoctorMenu
{ 
    private readonly WorkWithData _workWithData;
    private readonly Presentation.CertificateMenu _certificateMenu;

    public DoctorMenu(WorkWithData workWithData, Presentation.CertificateMenu certificateMenu)
    {
        _workWithData = workWithData;
        _certificateMenu = certificateMenu;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nDoctor Menu: ");
            Console.WriteLine("1. Add Doctor");
            Console.WriteLine("2. Update Doctor");
            Console.WriteLine("3. Delete Doctor");
            Console.WriteLine("4. Display a List of Doctors");
            Console.WriteLine("5. Number of Doctors with Specialization");
            Console.WriteLine("6. Back to Main Menu");

            Console.Write("Select an action: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddDoctor();
                    break;

                case "2":
                    UpdateDoctor();
                    break;

                case "3": 
                    DeleteDoctor();
                    break;

                case "4":
                    PrintDoctors();
                    break;
                
                case "5":
                    GetNumOfDoctors();
                    break;
                
                case "6":
                    return;

                default:
                    Console.WriteLine("Wrong input. Please try again.");
                    break;
            }
        }
    }
    
    public void AddDoctor()
    {
        Console.WriteLine("\nEnter Doctor's Name: ");
        string name = Console.ReadLine();

        Console.WriteLine("\nEnter Specialization ID: ");
        int specializationId;
        while (!int.TryParse(Console.ReadLine(), out specializationId))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for specialization ID:");
        }
        _workWithData.AddDoctorWithSpecializationId(name, specializationId);
        Console.WriteLine("Doctor add operation completed.");
        Console.WriteLine($"Doctor {name} does not have any certificates, add at least one");
        var newDoctor = _workWithData.FindDoctorByName(name);
        if (newDoctor.Count != 0)
            _certificateMenu.AddCertificate(newDoctor[0].ID);
    }
    
    public void AddDoctor(int specializationId)
    {
        Console.WriteLine("\nEnter Doctor's Name: ");
        string name = Console.ReadLine();
        
        _workWithData.AddDoctorWithSpecializationId(name, specializationId);
        Console.WriteLine("Doctor add operation completed.");
        Console.WriteLine($"Doctor {name} does not have any certificates, add at least one");
        var newDoctor = _workWithData.FindDoctorByName(name);
        if (newDoctor.Count != 0)
            _certificateMenu.AddCertificate(newDoctor[0].ID);
    }

    private void DeleteDoctor()
    {
        Console.Write("\nEnter Doctor ID to delete: ");
        string doctorId = Console.ReadLine();
        _workWithData.DeleteDoctor(doctorId);
        Console.WriteLine("Doctor delete operation completed.");
    }

    private void UpdateDoctor()
    {
        Console.Write("\nEnter Doctor ID to update: ");
        string tmpId = Console.ReadLine();

        Console.WriteLine("\nChoose an option:");
        Console.WriteLine("1. Update name");
        Console.WriteLine("2. Update specialization");
        
        Console.Write("Select an action: ");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
        {
            Console.WriteLine("Invalid choice. Please enter 1 or 2:");
        }
        
        if (choice == 1)
        {
            Console.Write("\nEnter new name: ");
            string newName = Console.ReadLine();
            _workWithData.UpdateDoctorName(newName, tmpId);
            Console.WriteLine("Doctor name update operation completed.");
        }
        else
        {
            Console.Write("\nEnter new Specialization ID: ");
            if (int.TryParse(Console.ReadLine(), out int newSpecializationId))
            {
                _workWithData.UpdateDoctorSpecializationWithId(newSpecializationId, tmpId);
                Console.WriteLine("Doctor specialization update operation completed.");
            }
        }
    }

    private void GetNumOfDoctors()
    {
        Console.Write("\nEnter Doctor's Specialization ID: ");
        string tmpId = Console.ReadLine();
        var res = _workWithData.CountAllDoctorsWithSpecialization(tmpId);

        if (res > -1)
        {
            int.TryParse(tmpId, out int id);
            Console.WriteLine($"Number of Doctors With Specialization " +
                              $"{_workWithData.FindSpecializationWithId(id)}: {res} ");
        }
    }

    private void PrintDoctors()
    {
        var doctors = _workWithData.TakeAllDoctors();
        
        Console.WriteLine("\nDoctors: ");
        foreach (var doctor in doctors)
        {
            Console.WriteLine($"{doctor.ID}. {doctor.Name}, Specialization: {doctor.SpecializationName}");
        }
    }

}