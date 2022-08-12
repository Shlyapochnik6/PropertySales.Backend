namespace PropertySales.Application.Common.Exceptions;

public class RecordExistsException : Exception
{
    public RecordExistsException(string name) 
        : base($"Record '{name}' has already been created.")
    {
        
    }
}