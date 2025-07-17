namespace visma_task.Helpers;

public static class LoginInputHelper
{
    private const string Password = "milk";
    
    public static string GetUsernameInput()
    {
        Console.WriteLine("Enter your username:");
        
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

    public static bool IsPasswordInputSuccessful()
    {
        Console.WriteLine("Enter the password:");
        
        while (true)
        {
            string? input = Console.ReadLine()?.Trim();

            if (string.Equals(input, Password)) return true;

            if (string.Equals(input, "user")) return false;

            Console.WriteLine("Incorrect password. Try again.");
            Console.WriteLine("If you wish to proceed as a regular user, enter 'user'");
        }
    }
}