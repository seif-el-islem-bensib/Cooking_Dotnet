using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RestaurantManagement.Shared.DTOs.Auth;

namespace RestaurantManagement.App.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var authResponse = await _localStorage.GetItemAsync<AuthResponse>("authResponse");
            if (authResponse == null)
                return await Task.FromResult(new AuthenticationState(_anonymous));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, authResponse.UserId.ToString()),
                new Claim(ClaimTypes.Email, authResponse.Email),
                new Claim(ClaimTypes.Role, authResponse.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task MarkUserAsAuthenticated(AuthResponse authResponse)
    {
        await _localStorage.SetItemAsync("authResponse", authResponse);
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, authResponse.UserId.ToString()),
            new Claim(ClaimTypes.Email, authResponse.Email),
            new Claim(ClaimTypes.Role, authResponse.Role.ToString())
        }, "jwt");

        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _localStorage.RemoveItemAsync("authResponse");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
} 