using visma_task.models;

namespace visma_task.interfaces;

public interface IReadable
{
    List<Shortage>? GetAll();
}