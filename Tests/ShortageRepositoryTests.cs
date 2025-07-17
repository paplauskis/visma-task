using visma_task.models;
using visma_task.repositories;
using visma_task.services;

namespace Tests;

public class ShortageRepositoryTests : IDisposable
{
    private readonly TestRepository _repo;

    public ShortageRepositoryTests()
    {
        _repo = new TestRepository();
    }

    public void Dispose()
    {
        if (File.Exists("q.json"))
        {
            File.Delete("q.json");
        }
    }

    [Theory]
    [InlineData("Projector", "Projector", Room.Office, Category.Electronics, 2, "Jake")]
    [InlineData("Coffee", "Coffee Beans", Room.Kitchen, Category.Food, 5, "Petras")]
    [InlineData("Desk", "Wooden Desk", Room.Reception, Category.Furniture, 10, "John")]
    [InlineData("Chair", "Office Chair", Room.Office, Category.Furniture, 7, "Bob")]
    [InlineData("Snack", "Chocolate Bar", Room.Kitchen, Category.Other, 8, "Vismis")]
    public void Add_ShouldAddShortage_WhenNoDuplicateExists(
        string title,
        string name,
        Room room,
        Category category,
        int priority,
        string createdBy)
    {
        var shortage = new Shortage
        {
            Title = title,
            Name = name,
            Room = room,
            Category = category,
            Priority = priority,
            CreatedBy = createdBy,
            CreatedOn = DateOnly.FromDateTime(DateTime.Today)
        };
        
        _repo.Add(shortage);
        
        var allShortages = _repo.GetAll();
        Assert.NotNull(allShortages);
        Assert.Single(allShortages);
        Assert.Equal(title, allShortages[0].Title);
        Assert.Equal(name, allShortages[0].Name);
        Assert.Equal(room, allShortages[0].Room);
        Assert.Equal(category, allShortages[0].Category);
        Assert.Equal(priority, allShortages[0].Priority);
        Assert.Equal(createdBy, allShortages[0].CreatedBy);
    }

    [Theory]
    [InlineData("Projector", "Projector", Room.Office, Category.Electronics, 2, "Jake")]
    [InlineData("Coffee", "Coffee Beans", Room.Kitchen, Category.Food, 5, "Petras")]
    [InlineData("Desk", "Wooden Desk", Room.Reception, Category.Furniture, 10, "John")]
    [InlineData("Chair", "Office Chair", Room.Office, Category.Furniture, 7, "Bob")]
    [InlineData("Snack", "Chocolate Bar", Room.Kitchen, Category.Other, 8, "Vismis")]
    public void GetByTitleAndRoom_ShouldReturnShortage_WhenExists(
        string title,
        string name,
        Room room,
        Category category,
        int priority,
        string createdBy)
    {
        var shortage = new Shortage
        {
            Title = title,
            Name = name,
            Room = room,
            Category = category,
            Priority = priority,
            CreatedBy = createdBy,
            CreatedOn = DateOnly.FromDateTime(DateTime.Today)
        };
    
        _repo.Add(shortage);
        
        var result = _repo.GetByTitleAndRoom(title, room);
        
        Assert.NotNull(result);
        Assert.Equal(title, result.Title);
        Assert.Equal(name, result.Name);
        Assert.Equal(room, result.Room);
        Assert.Equal(category, result.Category);
        Assert.Equal(priority, result.Priority);
        Assert.Equal(createdBy, result.CreatedBy);
    }
    
    [Theory]
    [InlineData("Projector", "Projector", Room.Office, Category.Electronics, 2, "Jake")]
    [InlineData("Coffee", "Coffee Beans", Room.Kitchen, Category.Food, 5, "Petras")]
    [InlineData("Desk", "Wooden Desk", Room.Reception, Category.Furniture, 10, "John")]
    [InlineData("Chair", "Office Chair", Room.Office, Category.Furniture, 7, "Bob")]
    [InlineData("Snack", "Chocolate Bar", Room.Kitchen, Category.Other, 8, "Vismis")]
    public void Delete_ShouldRemoveShortage(
        string title,
        string name,
        Room room,
        Category category,
        int priority,
        string createdBy)
    {
        var shortage = new Shortage
        {
            Title = title,
            Name = name,
            Room = room,
            Category = category,
            Priority = priority,
            CreatedBy = createdBy,
            CreatedOn = DateOnly.FromDateTime(DateTime.Today)
        };
    
        _repo.Add(shortage);
        
        _repo.Delete(shortage);
        
        var allShortages = _repo.GetAll();
        Assert.NotNull(allShortages);
        Assert.Empty(allShortages);
    }
}