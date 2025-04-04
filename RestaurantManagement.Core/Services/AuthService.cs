using RestaurantManagement.Shared.DTOs.Auth;

namespace RestaurantManagement.Core.Services;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<bool> ValidateTokenAsync(string token);
} 