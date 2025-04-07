namespace EFCoreIsitech.Data.Services;

/// <summary>
/// Interface for retrieving current user information for auditing
/// </summary>
public interface ICurrentUserService
{
    string? GetCurrentUsername();
}