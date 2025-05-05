using System.Text.Json;
using Application.Abstractions.Interfaces.Services;
using Microsoft.JSInterop;

namespace WebApp.Services;

public class SessionStorageService(IJSRuntime jsRuntime) : ISessionStorageService
{
    public async Task<T?> GetItemAsync<T>(string key)
    {
        try
        {
            var json = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", key);
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (JsonException)
        {
            await RemoveItemAsync(key);
            return default;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en SessionStorage: {ex.Message}");
            return default;
        }
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", key, JsonSerializer.Serialize(value));
    }

    public async Task RemoveItemAsync(string key)
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", key);
    }
}