using System.ComponentModel.DataAnnotations;
using BackendTemplate.Data.DTO;
using BackendTemplate.Exceptions;

namespace BackendTemplate.Helpers;

public static class ValidationHelper
{
    public static void ValidateModel(object obj)
    {
        var context = new ValidationContext(obj);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(obj, context, results, true);
        if (!isValid)
        {
            throw new InvalidDtoExceptions();
        }
    }

    public static void Validate(this DtoBase dto)
    {
        ValidateModel(dto);
    }
}