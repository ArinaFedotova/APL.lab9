using Core;
using Core.DTO;
using Core.Handler;

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
            Console.WriteLine("5. Back to Main Menu");

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
                    return;

                default:
                    Console.WriteLine("Wrong input. Please try again.");
                    break;
            }
        }
    }
    
    public void AddDoctor(int specializationId = -1)
    {
        Console.WriteLine("\nEnter Doctor's Name: ");
        string name = null;
        while (name == null)
            name = Console.ReadLine();

        if (specializationId == -1)
        {
            Console.WriteLine("\nEnter Specialization ID: ");
            while (!int.TryParse(Console.ReadLine(), out specializationId))
                Console.WriteLine("Invalid input. Please enter a valid integer for specialization ID:");
        }
        DoctorDTO? doctor = _workWithData.AddDoctor
            (ConverterDTO.ToDTO(doctorName: name, doctorSpecializationId: specializationId));
        if(doctor == null)
            return;
        
        bool noDependency = true;
        foreach (var cert in _workWithData.TakeAllCertificates())
        {
            if (cert.DoctorID == doctor.ID)
            {
                noDependency = false;
                break;
            }
        }
        if (noDependency && doctor.ID != -1)
        {
            Console.WriteLine($"Doctor does not have any certificates, add at least one");
            _certificateMenu.AddCertificate(doctor.ID);
        }
    }

    private void DeleteDoctor()
    {
        Console.Write("\nEnter Doctor ID to delete: ");
        int doctorId;
        while (!int.TryParse(Console.ReadLine(), out doctorId))
            Console.WriteLine("Invalid input. Please enter a valid integer for specialization ID:");
        _workWithData.DeleteDoctor(ConverterDTO.ToDTO(doctorId: doctorId));
        Console.WriteLine("Doctor delete operation completed.");
    }

    private void UpdateDoctor()
    {
        Console.Write("\nEnter Doctor ID to update: ");
        int doctorId;
        while (!int.TryParse(Console.ReadLine(), out doctorId))
            Console.WriteLine("Invalid input. Please enter a valid integer for doctor ID:");

        Console.Write("\nEnter new name: ");
        string newName = Console.ReadLine();
        while (newName == null)
            newName = Console.ReadLine();
        
        Console.Write("\nEnter new Specialization ID: ");
        int specId;
        while (!int.TryParse(Console.ReadLine(), out specId))
            Console.WriteLine("Invalid input. Please enter a valid integer for specialization ID:");

        _workWithData.UpdateDoctor(ConverterDTO.ToDTO
            (doctorId: doctorId, doctorName: newName, doctorSpecializationId: specId));
        Console.WriteLine("Doctor specialization update operation completed.");
    }

    // private void GetNumOfDoctors()
    // {
    //     Console.Write("\nEnter Doctor's Specialization ID: ");
    //     string tmpId = Console.ReadLine();
    //     var res = _workWithData.CountAllDoctorsWithSpecialization(tmpId);
    //
    //     if (res > -1)
    //     {
    //         int.TryParse(tmpId, out int id);
    //         Console.WriteLine($"Number of Doctors With Specialization " +
    //                           $"{_workWithData.FindSpecializationWithId(id)}: {res} ");
    //     }
    // }

    private void PrintDoctors()
    {
         _workWithData.TakeAllDoctors();
    }

}