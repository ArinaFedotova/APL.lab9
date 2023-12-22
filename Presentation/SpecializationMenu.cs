using Core;

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
            Console.WriteLine("5. Find Specialization by Certificate.");
            Console.WriteLine("6. Back to Main Menu");

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
                    FindSpecializationByCertificate();
                    break;

                case "6":
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
        
        _workWithData.AddSpecialization(name);
        Console.WriteLine("Specialization add operation completed.");
        Console.WriteLine($"Specialization {name} does not have any doctors, add at least one");
        var newSpecialization = _workWithData.FindSpecializationWithName(name);
        _doctorMenu.AddDoctor(newSpecialization);
    }

    private void DeleteSpecialization()
    {
        Console.Write("\nEnter Specialization ID to delete: ");
        string specializationId = Console.ReadLine();
        _workWithData.DeleteSpecialization(specializationId);
        Console.WriteLine("Specialization delete operation completed.");
    }

    private void UpdateSpecialization()
    {
        Console.Write("\nEnter Specialization ID to update: ");
        string tmpId = Console.ReadLine();
    
        Console.Write("\nEnter new name: ");
        string newName = Console.ReadLine();
        _workWithData.UpdateSpecializationName(newName, tmpId);
        Console.WriteLine("Specialization name update operation completed.");
    }

    private void FindSpecializationByCertificate()
    {
        Console.Write("\nEnter Certificate ID: ");
        string tmpId = Console.ReadLine();
        _workWithData.GetSpecializationByCertificate(tmpId);
        Console.WriteLine("Obtaining Specialization by Certificate operation completed.");
    }
    private void PrintSpecializations()
    {
        var specializations = _workWithData.TakeAllSpecializations();
        Console.WriteLine("\nSpecializations: ");
        foreach (var specialization in specializations)
        {
            Console.WriteLine($"{specialization.ID}. {specialization.Name}");
        }
    }
}