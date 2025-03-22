using Microsoft.JSInterop;

namespace personal_website.Services;

public class ThemeService
{
    private readonly IJSRuntime _jsRuntime;
    private const string ThemeKey = "app-theme";

    public ThemeService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string> GetTheme()
    {
        return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", ThemeKey) ?? "dark";
    }

    public async Task SetTheme(string theme)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", ThemeKey, theme);
        await _jsRuntime.InvokeVoidAsync("setThemeOnHtml", theme);
    }
}
