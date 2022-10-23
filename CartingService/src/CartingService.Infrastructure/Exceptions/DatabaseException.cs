namespace CartingService.Infrastructure.Exceptions;

public class DatabaseException : Exception
{
    public DatabaseException(string connectionString, Exception innerException)
        : base($"There is an error in DB ('{connectionString}')", innerException)
    {
        // do nothing
    }
}