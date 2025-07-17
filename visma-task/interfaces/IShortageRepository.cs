using visma_task.models;

namespace visma_task.interfaces;

public interface IShortageRepository : IRepository
{
    Shortage? GetByTitleAndRoom(string title, Room room);
}