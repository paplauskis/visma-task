using visma_task.models;

namespace visma_task.interfaces;

public interface IWritable
{
    void Add(Shortage shortage);
    void Delete(Shortage shortage);
}