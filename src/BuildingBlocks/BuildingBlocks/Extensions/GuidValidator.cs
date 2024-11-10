namespace BuildingBlocks.Extensions;

public static class GuidValidator
{
    public static bool IsValidGuid(this string guidString)
    {
        return Guid.TryParse(guidString, out _);
    }   
}