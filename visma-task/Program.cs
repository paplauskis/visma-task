
using visma_task;
using visma_task.helpers;
using visma_task.Helpers;
using visma_task.interfaces;
using visma_task.models;
using visma_task.repositories;
using visma_task.services;

Console.WriteLine("Hello!");
string username = LoginInputHelper.GetUsernameInput();
UserSession.Username = username;

Console.WriteLine("Are you an admin? (yes/no)");
string? isAdmin = Console.ReadLine()?.Trim().ToLower();

bool isLoginSuccessful = false;
if (isAdmin == "yes")
{
    isLoginSuccessful = LoginInputHelper.IsPasswordInputSuccessful();
}

if (isLoginSuccessful)
{
    UserSession.Role = UserRole.Admin;
}

int userInput;
IShortageRepository repo = new ShortageRepository();
var shortageService = new ShortageService(repo);

do
{
    Logger.PrintOptions();
    string? input = Console.ReadLine();

    if (!int.TryParse(input, out userInput))
    {
        Console.WriteLine("ERROR: Invalid choice.");
    }

    switch (userInput)
    {
        case 1:
            shortageService.GetShortages();
            break;
        case 2:
            var shortage = ShortageInputHelper.RegisterNewShortageInput();
            shortageService.Add(shortage);
            break;
        case 3:
            var dto = ShortageInputHelper.DeleteShortageInput();
            shortageService.Delete(dto);
            break;
        case 4:
            Console.WriteLine("bye!");
            break;
        default:
            Console.WriteLine("ERROR: Invalid choice.");
            break;
    }
    
} while(userInput != 4);