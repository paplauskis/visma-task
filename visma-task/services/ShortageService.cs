using System.Diagnostics;
using visma_task.helpers;
using visma_task.interfaces;
using visma_task.models;

namespace visma_task.services;

public class ShortageService
{
    private readonly IShortageRepository _shortageRepository;
    public ShortageService(IShortageRepository repository)
    {
        _shortageRepository = repository;
    }
    
    public void Add(Shortage shortage)
    {
        var existingShortage = _shortageRepository.GetByTitleAndRoom(shortage.Title, shortage.Room);
        
        if (existingShortage != null
            && existingShortage.Title == shortage.Title
            && existingShortage.Room == shortage.Room)
        {
            Console.WriteLine("WARNING: Shortage with the same title and room already exists.");
            if (shortage.Priority > existingShortage.Priority)
            {
                Console.WriteLine("Overwriting existing shortage...");
                _shortageRepository.Delete(existingShortage);
                _shortageRepository.Add(shortage); 
            }
        }
        else
        {
            _shortageRepository.Add(shortage);
        }
    }

    public void Delete(ShortageIndetificationDto dto)
    {
        var shortages = _shortageRepository.GetAll();
        if (shortages == null) return;
        
        var existingShortage = shortages.Find(s => s.Title == dto.Title && s.Room == dto.Room);
        if (existingShortage == null) return;

        if (UserSession.Role == UserRole.Admin
            || existingShortage.CreatedBy == UserSession.Username)
        {
            _shortageRepository.Delete(existingShortage);
            Console.WriteLine("Shortage deleted.");
        }
    }
    
    public void GetShortages()
    {
        var shortages = FilterByUserRole(_shortageRepository.GetAll());
        
        switch (ShortageInputHelper.GetShortageFilterInput())
        {
            case "1":
                var title = ShortageInputHelper.GetStringInput("Title");
                shortages = GetShortages(title, shortages);
                break;

            case "2":
                Console.WriteLine("Enter start date:");
                var fromDate = ShortageInputHelper.GetDateInput();

                Console.WriteLine("Enter end date:");
                var toDate = ShortageInputHelper.GetDateInput();

                shortages = GetShortages(fromDate, toDate, shortages);
                break;

            case "3":
                var room = ShortageInputHelper.GetRoomInput();
                shortages = GetShortages(room, shortages);
                break;

            case "4":
                var category = ShortageInputHelper.GetCategoryInput();
                shortages = GetShortages(category, shortages);
                break;
            
            case "5":
                break;
        }
        
        Logger.PrintShortages(shortages);
    }
    
    public List<Shortage> GetShortages(string title, List<Shortage> shortages)
    {
        title = title.ToLower();
        
        return shortages
            .Where(s => s.Title.ToLower().Contains(title))
            .OrderByDescending(s => s.Priority)
            .ToList();
    }

    public List<Shortage> GetShortages(DateOnly fromDate, DateOnly toDate, List<Shortage> shortages)
    {
        return shortages
            .Where(s => s.CreatedOn >= fromDate && s.CreatedOn <= toDate)
            .OrderByDescending(s => s.Priority)
            .ToList();
    }

    public List<Shortage> GetShortages(Room room, List<Shortage> shortages)
    {
        return shortages
            .Where(s => s.Room == room)
            .OrderByDescending(s => s.Priority)
            .ToList();
    }

    public List<Shortage> GetShortages(Category category, List<Shortage> shortages)
    {
        return shortages
            .Where(s => s.Category == category)
            .OrderByDescending(s => s.Priority)
            .ToList();
    }
    
    private List<Shortage>? FilterByUserRole(List<Shortage>? shortages)
    {
        if (UserSession.Role != UserRole.Admin)
        {
            shortages = shortages?
                .Where(s => s.CreatedBy == UserSession.Username)
                .ToList();
        }
        
        return shortages;
    }
}