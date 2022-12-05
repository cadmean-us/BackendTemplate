namespace BackendTemplate.Exceptions;

public class MissingConfigurationException : Exception
{
    public MissingConfigurationException(string configName) : base($"Missing configuration entry: '{configName}'")
    {
    }
}