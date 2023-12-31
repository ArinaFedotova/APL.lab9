using Core;
using Core.DTO;
using Core.Handler;

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
            Console.WriteLine("6. Get Specialization Name By Certificate");
            Console.WriteLine("7. Back to Main Menu");

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
                    GetSpecializationNameByCertificate();
                    break;

                case "7":
                    return;

                default:
                    Console.WriteLine("Wrong input. Please try again.");
                    break;
            }
        }
    }
    
    public void AddCertificate(int doctorId = -1)
    {
        Console.WriteLine("\nEnter Certificate's Description: ");
        string description = Console.ReadLine();
        while (description == null)
            description = Console.ReadLine();
        
        Console.WriteLine("\nEnter when Certificate was given date (YYYY-MM-DD):");
        DateTime creationDate;
        while (!DateTime.TryParse(Console.ReadLine(), out creationDate))
        {
            Console.WriteLine("Invalid input. Please enter a valid date (YYYY-MM-DD):");
        }
        
        if (doctorId == -1)
        {
            Console.WriteLine("\nEnter Doctor ID: ");
            while (!int.TryParse(Console.ReadLine(), out doctorId))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for doctor ID:");
            }
        }

        _workWithData.AddCertificate(ConverterDTO.
            ToDto(certificateDoctorId: doctorId, certificateDate: creationDate, certificateDescription: description));
        Console.WriteLine("Certificate add operation completed.");
    }

    private void DeleteCertificate()
    {
        Console.Write("\nEnter Certificate ID to delete: ");
        int certificateId;
        while (!int.TryParse(Console.ReadLine(), out certificateId))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for doctor ID:");
        }
        
        _workWithData.DeleteCertificate(ConverterDTO.ToDto(certificateId: certificateId));
        Console.WriteLine("Certificate delete operation completed.");
    }

    private void UpdateCertificate()
    {
        Console.Write("\nEnter Certificate ID to update: ");
        int certificateId;
        while (!int.TryParse(Console.ReadLine(), out certificateId))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for certificate ID:");
        }

        Console.Write("\nEnter new Description: ");
        string newDescription = Console.ReadLine();
        while (newDescription == null)
            newDescription = Console.ReadLine();
        
        Console.WriteLine("\nEnter new creation date (YYYY-MM-DD):");
        DateTime newCreationDate;
        while (!DateTime.TryParse(Console.ReadLine(), out newCreationDate))
        {
            Console.WriteLine("Invalid input. Please enter a valid date (YYYY-MM-DD):");
        }
            
        Console.Write("\nEnter new Doctor ID: ");
        int doctorId;
        while (!int.TryParse(Console.ReadLine(), out doctorId))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for doctor ID:");
        }
        _workWithData.UpdateCertificate(ConverterDTO.ToDto
        (certificateId : certificateId, certificateDescription : newDescription,
                certificateDate : newCreationDate, certificateDoctorId: doctorId));
        Console.WriteLine("Certificate Doctor update operation completed.");
    }

    private void LatestCertificate()
    {
        Console.Write("\nEnter Doctor ID: ");
        if (int.TryParse(Console.ReadLine(), out int doctorId)) 
        {
            _workWithData.FindLastCertificateDoctor(ConverterDTO.ToDTO(doctorId: doctorId));
            Console.WriteLine("Latest Doctor's Certificate obtaining operation completed.");
        }
        else
            Console.WriteLine("Invalid Doctor ID. Please enter a valid integer.");
    }
    
    private void GetSpecializationNameByCertificate()
    {
        Console.WriteLine("Enter Certificate ID: ");
        if (int.TryParse(Console.ReadLine(), out var certId))
            throw new ArgumentException("Error. Please enter correct Certificate ID");
        
        _workWithData.GetSpecializationNameByCertificate
            (ConverterDTO.ToDto(certificateId: certId));
    }
    
    

    private void PrintCertificates()
    {
         _workWithData.TakeAllCertificates();
    }
}