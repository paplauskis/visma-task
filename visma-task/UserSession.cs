using visma_task.models;

namespace visma_task;

public static class UserSession
{
    public static string Username { get; set; }
    public static UserRole Role { get; set; } = UserRole.User;
}