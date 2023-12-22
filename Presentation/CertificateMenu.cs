using Core;

namespace Presentation;

public class CertificateMenu
{
    private readonly WorkWithData _workWithData;

    public CertificateMenu(WorkWithData workWithData)
    {
        _workWithData = workWithData;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nCertificate Menu: ");
            Console.WriteLine("1. Add Certificate");
            Console.WriteLine("2. Update Certificate");
            Console.WriteLine("3. Delete Certificate");
            Console.WriteLine("4. Display a List of Certificate");
            Console.WriteLine("5. Display Latest Doctor's Certificate");
            Console.WriteLine("6. Back to Main Menu");

            Console.Write("Select an action: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddCertificate();
                    break;

                case "2":
                    UpdateCertificate();
                    break;

                case "3": 
                    DeleteCertificate();
                    break;

                case "4":
                    PrintCertificates();
                    break;
                
                case "5":
                    LatestCertificate();
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Wrong input. Please try again.");
                    break;
            }
        }
    }
    
    public void AddCertificate()
    {
        Console.WriteLine("\nEnter Certificate's description Name: ");
        string description = Console.ReadLine();
        
        Console.WriteLine("\nEnter when Certificate was given date (YYYY-MM-DD):");
        DateTime creationDate;
        while (!DateTime.TryParse(Console.ReadLine(), out creationDate))
        {
            Console.WriteLine("Invalid input. Please enter a valid date (YYYY-MM-DD):");
        }
        
        Console.WriteLine("\nEnter Doctor ID: ");
        int doctorId;
        while (!int.TryParse(Console.ReadLine(), out doctorId))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for doctor ID:");
        }
        _workWithData.AddCertificateWithDoctorId(creationDate, description, doctorId);
        Console.WriteLine("Certificate add operation completed.");
    }
    
    public void AddCertificate(int doctorId)
    {
        Console.WriteLine("\nEnter Certificate's description Name: ");
        string description = Console.ReadLine();
        
        Console.WriteLine("\nEnter when Certificate was given date (YYYY-MM-DD):");
        DateTime creationDate;
        while (!DateTime.TryParse(Console.ReadLine(), out creationDate))
        {
            Console.WriteLine("Invalid input. Please enter a valid date (YYYY-MM-DD):");
        }
        _workWithData.AddCertificateWithDoctorId(creationDate, description, doctorId);
        Console.WriteLine("Certificate add operation completed.");
    }

    private void DeleteCertificate()
    {
        Console.Write("\nEnter Certificate ID to delete: ");
        string certificateId = Console.ReadLine();
        _workWithData.DeleteCertificate(certificateId);
        Console.WriteLine("Certificate delete operation completed.");
    }

    private void UpdateCertificate()
    {
        Console.Write("\nEnter Certificate ID to update: ");
        string tmpId = Console.ReadLine();
        

        Console.WriteLine("\nChoose an option:");
        Console.WriteLine("1. Update Description");
        Console.WriteLine("2. Update Date");
        Console.WriteLine("3. Update Doctor");
        
        Console.Write("Select an action: ");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
        {
            Console.WriteLine("Invalid choice. Please enter 1 or 2:");
        }
        
        if (choice == 1)
        {
            Console.Write("\nEnter new Description: ");
            string newDescription = Console.ReadLine();
            _workWithData.UpdateCertificateDescription(newDescription, tmpId);
            Console.WriteLine("Certificate Description update operation completed.");
        }
        else if (choice == 2)
        {
            Console.WriteLine("\nEnter new creation date (YYYY-MM-DD):");
            DateTime newCreationDate;
            while (!DateTime.TryParse(Console.ReadLine(), out newCreationDate))
            {
                Console.WriteLine("Invalid input. Please enter a valid date (YYYY-MM-DD):");
            }
            _workWithData.UpdateCertificateDate(newCreationDate, tmpId);
            Console.WriteLine("Certificate Date update operation completed.");
        }
        else
        {
            Console.Write("\nEnter new Doctor ID: ");
            if (int.TryParse(Console.ReadLine(), out int newDoctorId))
            {
                _workWithData.UpdateCertificateDoctorWithId(newDoctorId, tmpId);
                Console.WriteLine("Certificate Doctor update operation completed.");
            }
        }
    }

    private void LatestCertificate()
    {
        Console.Write("\nEnter Doctor ID: ");
        if (int.TryParse(Console.ReadLine(), out int doctorId)) 
        {
            _workWithData.FindLastCertificateDoctor(doctorId);
            Console.WriteLine("Latest Doctor's Certificate obtaining operation completed.");
        }
        else
            Console.WriteLine("Invalid Doctor ID. Please enter a valid integer.");
    }

    private void PrintCertificates()
    {
        var certificates = _workWithData.TakeAllCertificates();
        Console.WriteLine("\nCertificates: ");
        foreach (var certificate in certificates)
        {
            Console.WriteLine($"{certificate.ID}. {certificate.Date}, " +
                              $"Specialization: {certificate.Description}, " +
                              $"Doctor: {certificate.DoctorName}");
        }
    }
}