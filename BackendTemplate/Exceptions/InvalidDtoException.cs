namespace BackendTemplate.Exceptions;

public class InvalidDtoExceptions : CadException
{
    public InvalidDtoExceptions() : base("invalid_dto")
    {
    }
}