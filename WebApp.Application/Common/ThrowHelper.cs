namespace WebApp.Application.Common;

public static class ThrowHelper
{
    public static void ThrowValidationException(string message) => throw new ValidationException(message);

    public static void ThrowRecordNotFoundException(string message) =>
        throw new RecordNotFoundException(message);
}