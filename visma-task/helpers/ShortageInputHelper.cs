using System.Globalization;
using visma_task.models;
using visma_task.repositories;
using visma_task.services;

namespace visma_task.helpers;

public class ShortageInputHelper
{
    public static Shortage RegisterNewShortageInput()
    {
        string title = GetStringInput("title");
        string name = GetStringInput("Name");
        Room room = GetRoomInput();
        Category category = GetCategoryInput();
        int priority = GetPriorityInput();
        
        return new Shortage
        {
            Title = title,
            Name = name,
            Room = room,
            Category = category,
            Priority = priority,
            CreatedOn = DateOnly.FromDateTime(DateTime.Now),
            CreatedBy = UserSession.Username
        };
    }

    public static ShortageIndetificationDto DeleteShortageInput()
    {
        string title = GetStringInput("Title");
        Room room = GetRoomInput();

        return new ShortageIndetificationDto
        {
            Title = title,
            Room = room,
        };
    }
    
    // public List<Shortage> ApplyShortageFilterInput()
    // {
    //     var shortageService = new ShortageService(new ShortageRepository());
    //     
    //     switch (GetShortageFilterInput())
    //     {
    //         case "1":
    //             var title = GetStringInput("Title");
    //             return shortageService.GetShortages(title);
    //
    //         case "2":
    //             Console.WriteLine("Enter start date:");
    //             var fromDate = GetDateInput();
    //
    //             Console.WriteLine("Enter end date:");
    //             var toDate = GetDateInput();
    //
    //             return shortageService.GetShortages(fromDate, toDate);
    //
    //         case "3":
    //             var room = GetRoomInput();
    //             return shortageService.GetShortages(room);
    //
    //         case "4":
    //             var category = GetCategoryInput();
    //             return shortageService.GetShortages(category);
    //         default:
    //             return shortageService.GetShortages();
    //     }
    // }

    public static string GetShortageFilterInput()
    {
        while (true)
        {
            Console.WriteLine("Select filter option:");
            Console.WriteLine("1. Filter by title");
            Console.WriteLine("2. Filter by date");
            Console.WriteLine("3. Filter by room");
            Console.WriteLine("4. Filter by category");
            Console.WriteLine("5. No filter");

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int option) && 
                option >= 1 && 
                option <= 5)
            {
                return input;
            }

            Console.WriteLine("Invalid input. Try again.");
        }
    }

    public static string GetStringInput(string fieldName)
    {
        Console.WriteLine($"Enter {fieldName}:");
        
        while (true)
        {
            string? input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            Console.WriteLine("Input can't be empty. Try again.");
        }
    }

    public static Room GetRoomInput()
    {
        Logger.PrintRoomOptions();
        
        while (true)
        {
            string? input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out int roomNumber)
                && Enum.IsDefined(typeof(Room), roomNumber))
            {
                return (Room)roomNumber;
            }

            Console.WriteLine("Invalid input. Enter a number between 1 and 4.");
        }
    }
    
    public static Category GetCategoryInput()
    {
        Logger.PrintCategoryOptions();
        
        while (true)
        {
            string? input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out int categoryNumber)
                && Enum.IsDefined(typeof(Category), categoryNumber))
            {
                return (Category)categoryNumber;
            }

            Console.WriteLine("Invalid input. Enter a number between 1 and 4.");
        }
    }

    public static int GetPriorityInput()
    {
        Console.WriteLine("Enter priority (between 1 and 10):");
        
        while (true)
        {
            string? input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out int priority)
                && priority >= 1
                && priority <= 10)
            {
                return priority;
            }

            Console.WriteLine("Priority must be between 1 and 10. Try again.");
        }
    }

    public static DateOnly GetDateInput()
    {
        while (true)
        {
            string? input = Console.ReadLine()?.Trim();

            if (DateTime.TryParseExact(
                    input,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsedDate))
            {
                return DateOnly.FromDateTime(parsedDate);
            }

            Console.WriteLine("Invalid date. Try again.");
        }
    } 
}