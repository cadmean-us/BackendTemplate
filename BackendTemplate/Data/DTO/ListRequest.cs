using System.ComponentModel.DataAnnotations;

namespace BackendTemplate.Data.DTO;

public class ListRequest : DtoBase
{
    [Required]
    [Range(10, 100)]
    public int PageSize { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public int PageIndex { get; set; }
}