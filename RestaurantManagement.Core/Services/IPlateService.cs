using RestaurantManagement.Shared.DTOs.Plates;

namespace RestaurantManagement.Core.Services;

public interface IPlateService
{
    Task<List<PlateResponse>> GetAllPlatesAsync();
    Task<PlateResponse> GetPlateByIdAsync(Guid id);
    Task<PlateResponse> CreatePlateAsync(PlateRequest request);
    Task<PlateResponse> UpdatePlateAsync(Guid id, PlateRequest request);
    Task DeletePlateAsync(Guid id);
} 