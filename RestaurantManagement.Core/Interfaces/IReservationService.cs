using RestaurantManagement.Shared.DTOs.Reservations;
using RestaurantManagement.Shared.Enums;

namespace RestaurantManagement.Core.Interfaces;

public interface IReservationService
{
    Task<List<ReservationResponse>> GetAllReservationsAsync();
    Task<ReservationResponse> GetReservationByIdAsync(Guid id);
    Task<List<ReservationResponse>> GetReservationsByUserIdAsync(string userId);
    Task<ReservationResponse> CreateReservationAsync(ReservationRequest request);
    Task<ReservationResponse> UpdateReservationAsync(Guid id, ReservationRequest request);
    Task DeleteReservationAsync(Guid id);
    Task<List<ReservationResponse>> GetTodayReservationsAsync();
    Task<ReservationResponse> UpdateReservationStatusAsync(Guid id, ReservationStatus status);
} 