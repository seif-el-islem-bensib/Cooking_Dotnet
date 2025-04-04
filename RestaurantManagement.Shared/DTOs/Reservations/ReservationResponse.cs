using RestaurantManagement.Shared.Enums;

namespace RestaurantManagement.Shared.DTOs.Reservations;

public class ReservationResponse
{
    public Guid Id { get; set; }
    public required string UserId { get; set; }
    public required string UserName { get; set; }
    public Guid PlateId { get; set; }
    public required string PlateName { get; set; }
    public DateTime ReservationDate { get; set; }
    public ReservationStatus Status { get; set; }
} 