namespace Application.Abstractions.Interfaces.Services;

public interface ISessionStorageService
{
    Task<T?> GetItemAsync<T>(string key);
    Task SetItemAsync<T>(string key, T value);
    Task RemoveItemAsync(string key);
}