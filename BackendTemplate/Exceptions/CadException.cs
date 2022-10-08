using Cadmean.RPC;

namespace BackendTemplate.Exceptions;

public class CadException : FunctionException
{
    public CadException(string code) : base(code)
    {
    }
}