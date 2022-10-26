namespace Core.Exceptions;

public class NameTooLongException : Exception
{
    public NameTooLongException(string name, byte maxLength)
        : base($"Name '{name}' is longer than allowed. Max length is {maxLength}")
    {
        // do nothing
    }
}