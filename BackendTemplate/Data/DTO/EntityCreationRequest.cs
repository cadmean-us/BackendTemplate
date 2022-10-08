using System.ComponentModel.DataAnnotations;

namespace BackendTemplate.Data.DTO;

public class EntityCreationRequest<TEntity> : DtoBase where TEntity : DtoBase
{
    [Required]
    public TEntity Data { get; set; }
}