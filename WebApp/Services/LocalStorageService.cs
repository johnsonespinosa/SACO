using System.Text.Json;
using Application.Abstractions.Interfaces.Services;
using Microsoft.JSInterop;

namespace WebApp.Services;

public class LocalStorageService(IJSRuntime jsRuntime) : ILocalStorageService
{
    public async Task<T?> GetItemAsync<T>(string key)
    {
        var json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
    }

    public async Task RemoveItemAsync(string key)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}