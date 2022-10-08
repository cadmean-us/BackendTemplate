using BackendTemplate.Data.Entities;

namespace BackendTemplate.Data.DTO;

public class EntityCreationResult
{
    public int Id { get; set; }

    public EntityCreationResult(EntityBase entity)
    {
        Id = entity.Id;
    }
}