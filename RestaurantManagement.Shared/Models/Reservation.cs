using RestaurantManagement.Shared.Enums;

namespace RestaurantManagement.Shared.Models;

public class Reservation
{
    public Guid Id { get; set; }
    public required string UserId { get; set; }
    public Guid PlateId { get; set; }
    public DateTime ReservationDate { get; set; }
    public ReservationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public Plate? Plate { get; set; }
} 