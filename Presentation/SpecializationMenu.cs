using Core;
using Core.DTO;
using Core.Handler;

namespace Presentation;

public class SpecializationMenu
{
    private readonly WorkWithData _workWithData;
    private readonly DoctorMenu _doctorMenu;

    public SpecializationMenu(WorkWithData workWithData, DoctorMenu doctorMenu)
    {
        _workWithData = workWithData;
        _doctorMenu = doctorMenu;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nSpecialization Menu: ");
            Console.WriteLine("1. Add Specialization");
            Console.WriteLine("2. Update Specialization");
            Console.WriteLine("3. Delete Specialization");
            Console.WriteLine("4. Display a List of Specialization");
            Console.WriteLine("5. Back to Main Menu");

            Console.Write("Select an action: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddSpecialization();
                    break;

                case "2":
                    UpdateSpecialization();
                    break;

                case "3":
                    DeleteSpecialization();
                    break;

                case "4":
                    PrintSpecializations();
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Wrong input. Please try again.");
                    break;
            }
        }
    }
    
    private void AddSpecialization()
    {
        Console.WriteLine("\nEnter Specialization's Name: ");
        string name = Console.ReadLine();
        while (name == null)
            name = Console.ReadLine();
        var specialization = _workWithData.AddSpecialization(ConverterDTO.
            ToDto(specializationName: name));
        Console.WriteLine("Specialization add operation completed.");
        
        bool noDependency = true;
        foreach (var doc in _workWithData.TakeAllDoctors())
        {
            if (doc.SpecializationID == specialization.ID)
            {
                noDependency = false;
                break;
            }
        }
        
        Console.WriteLine($"!!!{specialization.ID}");
        if (noDependency && specialization.ID != null)
        {
            Console.WriteLine($"Specializations {specialization.Name} does not have any " +
                              $"doctors, " + $"add at least one");
            _doctorMenu.AddDoctor(specializationId: specialization.ID);
        }
    }

    private void DeleteSpecialization()
    {
        Console.Write("\nEnter Specialization ID to delete: ");
        int specializationId;
        while (!int.TryParse(Console.ReadLine(), out specializationId))
            Console.WriteLine("Invalid input. Please enter a valid integer for " +
                              "specialization ID:");
        
        _workWithData.DeleteSpecialization(ConverterDTO.ToDto
            (specializationId: specializationId));
        Console.WriteLine("Specialization delete operation completed.");
    }

    private void UpdateSpecialization()
    {
        Console.Write("\nEnter Specialization ID to update: ");
        int specializationId;
        while (!int.TryParse(Console.ReadLine(), out specializationId))
            Console.WriteLine("Invalid input. Please enter a valid integer for specialization ID:");

    
        Console.Write("\nEnter new name: ");
        string newName = Console.ReadLine();
        while (newName == null)
            newName = Console.ReadLine();
        
        _workWithData.UpdateSpecialization(ConverterDTO.ToDto(specializationId: specializationId,
            specializationName: newName));
        Console.WriteLine("Specialization name update operation completed.");
    }
    
    public void GetAllDoctorsWithSpecialization()
    {
        Console.WriteLine("Enter Specialization ID: ");
        if (int.TryParse(Console.ReadLine(), out var specId))
            throw new ArgumentException("Error. Please enter correct Specialization ID");
        
        _workWithData.GetAllDoctorsWithSpecialization
            (ConverterDTO.ToDto(specializationId: specId));
    }

    private void PrintSpecializations()
    {
        _workWithData.TakeAllSpecializations();
    }
}