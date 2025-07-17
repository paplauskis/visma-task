using visma_task.services;

namespace Tests;

public class ShortageServiceTests
{
    private readonly TestRepository _testRepository;
    private readonly ShortageService _service;

    public ShortageServiceTests()
    {
        _testRepository = new TestRepository();
        _service = new ShortageService(_testRepository);
    }
    
    
    
}