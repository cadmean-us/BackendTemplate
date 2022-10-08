namespace BackendTemplate.Exceptions;

public class EntityNotFoundException : CadException
{
    public EntityNotFoundException() : base("entity_not_found")
    {
    }
    
    public EntityNotFoundException(string entity) : base($"{entity}_not_found")
    {
    }
}