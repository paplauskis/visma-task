using System.Text.Json;
using visma_task.models;
using visma_task.repositories;

namespace Tests;

public class TestRepository : ShortageRepository
{
    private readonly string _filePath = "q.json";
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true, };

    public TestRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Dispose();
        }
    }
    
    public List<Shortage>? GetAll()
    {
        string retrievedJson = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Shortage>>(retrievedJson, _options);
    }

    public void Add(Shortage shortage)
    {
        List<Shortage>? shortages = [];
        string retrievedJson = File.ReadAllText(_filePath);
        
        if (!string.IsNullOrEmpty(retrievedJson))
        {
            shortages = JsonSerializer.Deserialize<List<Shortage>>(retrievedJson, _options);
        }
        
        shortages?.Add(shortage);
        Save(shortages);
    }
    
    public void Delete(Shortage shortage)
    {
        var shortages = GetAll();
        if (shortages == null) return;
        
        //Remove() method did not work because object references differ
        shortages.RemoveAll(s =>
            s.Title == shortage.Title
            && s.Room == shortage.Room);
        
        Save(shortages);
    }

    public Shortage? GetByTitleAndRoom(string title, Room room)
    {
        List<Shortage>? shortages = GetAll();
        if (shortages == null) shortages = [];
        
        Shortage? shortage = shortages.Find(s => s.Title == title && s.Room == room);

        return shortage;
    }

    private void Save(List<Shortage> shortages)
    {
        string json = JsonSerializer.Serialize(shortages, _options);
        File.WriteAllText(_filePath, json);
    }
}