using RestaurantManagement.Shared.Enums;

namespace RestaurantManagement.Shared.DTOs.Reservations;

public class ReservationRequest
{
    public required string UserId { get; set; }
    public Guid PlateId { get; set; }
    public DateTime ReservationDate { get; set; }
    public ReservationStatus Status { get; set; }
    public string SpecialRequests { get; set; } = string.Empty;
} 