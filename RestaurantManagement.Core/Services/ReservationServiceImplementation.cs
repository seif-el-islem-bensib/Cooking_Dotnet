using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Interfaces;
using RestaurantManagement.Infrastructure.Data;
using RestaurantManagement.Shared.DTOs.Reservations;
using RestaurantManagement.Shared.Models;
using RestaurantManagement.Shared.Enums;

namespace RestaurantManagement.Core.Services;

public class ReservationServiceImplementation : IReservationService
{
    private readonly ApplicationDbContext _context;

    public ReservationServiceImplementation(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReservationResponse>> GetAllReservationsAsync()
    {
        return await _context.Reservations
            .Include(r => r.User)
            .Include(r => r.Plate)
            .Select(r => new ReservationResponse
            {
                Id = r.Id,
                UserId = r.UserId,
                UserName = r.User != null ? $"{r.User.FirstName} {r.User.LastName}" : string.Empty,
                PlateId = r.PlateId,
                PlateName = r.Plate != null ? r.Plate.Name : string.Empty,
                ReservationDate = r.ReservationDate,
                Status = r.Status
            })
            .ToListAsync();
    }

    public async Task<ReservationResponse> GetReservationByIdAsync(Guid id)
    {
        var reservation = await _context.Reservations
            .Include(r => r.User)
            .Include(r => r.Plate)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reservation == null)
            throw new Exception("Reservation not found");

        return new ReservationResponse
        {
            Id = reservation.Id,
            UserId = reservation.UserId,
            UserName = reservation.User != null ? $"{reservation.User.FirstName} {reservation.User.LastName}" : string.Empty,
            PlateId = reservation.PlateId,
            PlateName = reservation.Plate != null ? reservation.Plate.Name : string.Empty,
            ReservationDate = reservation.ReservationDate,
            Status = reservation.Status
        };
    }

    public async Task<List<ReservationResponse>> GetReservationsByUserIdAsync(string userId)
    {
        return await _context.Reservations
            .Include(r => r.User)
            .Include(r => r.Plate)
            .Where(r => r.UserId == userId)
            .Select(r => new ReservationResponse
            {
                Id = r.Id,
                UserId = r.UserId,
                UserName = r.User != null ? $"{r.User.FirstName} {r.User.LastName}" : string.Empty,
                PlateId = r.PlateId,
                PlateName = r.Plate != null ? r.Plate.Name : string.Empty,
                ReservationDate = r.ReservationDate,
                Status = r.Status
            })
            .ToListAsync();
    }

    public async Task<ReservationResponse> CreateReservationAsync(ReservationRequest request)
    {
        var reservation = new Reservation
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            PlateId = request.PlateId,
            ReservationDate = request.ReservationDate,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        return await GetReservationByIdAsync(reservation.Id);
    }

    public async Task<ReservationResponse> UpdateReservationAsync(Guid id, ReservationRequest request)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
            throw new Exception("Reservation not found");

        reservation.UserId = request.UserId;
        reservation.PlateId = request.PlateId;
        reservation.ReservationDate = request.ReservationDate;
        reservation.Status = request.Status;
        reservation.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetReservationByIdAsync(reservation.Id);
    }

    public async Task DeleteReservationAsync(Guid id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
            throw new Exception("Reservation not found");

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ReservationResponse>> GetTodayReservationsAsync()
    {
        var today = DateTime.Today;
        return await _context.Reservations
            .Include(r => r.User)
            .Include(r => r.Plate)
            .Where(r => r.ReservationDate.Date == today)
            .Select(r => new ReservationResponse
            {
                Id = r.Id,
                UserId = r.UserId,
                UserName = r.User != null ? $"{r.User.FirstName} {r.User.LastName}" : string.Empty,
                PlateId = r.PlateId,
                PlateName = r.Plate != null ? r.Plate.Name : string.Empty,
                ReservationDate = r.ReservationDate,
                Status = r.Status
            })
            .ToListAsync();
    }

    public async Task<ReservationResponse> UpdateReservationStatusAsync(Guid id, ReservationStatus status)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
            throw new Exception("Reservation not found");

        reservation.Status = status;
        reservation.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetReservationByIdAsync(reservation.Id);
    }
} 