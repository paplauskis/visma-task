using visma_task.models;

namespace visma_task;

public static class Logger
{
    public static void PrintOptions()
    {
        Console.WriteLine("Select your option:");
        Console.WriteLine("1 - Show data");
        Console.WriteLine("2 - Register shortage");
        Console.WriteLine("3 - Delete shortage");
        Console.WriteLine("4 - Exit");
    }

    public static void PrintRoomOptions()
    {
        Console.WriteLine("Enter Room number:");
        Console.WriteLine("1 - Office");
        Console.WriteLine("2 - Kitchen");
        Console.WriteLine("3 - Bathroom");
        Console.WriteLine("4 - Reception");
    }
    
    public static void PrintCategoryOptions()
    {
        Console.WriteLine("Enter Category number:");
        Console.WriteLine("1 - Electronics");
        Console.WriteLine("2 - Food");
        Console.WriteLine("3 - Furniture");
        Console.WriteLine("4 - Other");
    }

    public static void PrintShortages(List<Shortage>? shortages)
    {
        if (shortages == null || shortages.Count == 0)
        {
            Console.WriteLine("There are no shortages");
            return;
        }

        Console.WriteLine("---------------------------------");
        
        foreach (var sh in shortages)
        {
            Console.WriteLine($"Title: {sh.Title}");
            Console.WriteLine($"Name: {sh.Name}");
            Console.WriteLine($"Room: {sh.Room}");
            Console.WriteLine($"Category: {sh.Category}");
            Console.WriteLine($"Priority: {sh.Priority}");
            Console.WriteLine($"Date created: {sh.CreatedOn}");
            Console.WriteLine("---------------------------------");
        }
    }
}