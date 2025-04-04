using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Infrastructure.Data;
using RestaurantManagement.Shared.DTOs.Plates;
using RestaurantManagement.Shared.Models;

namespace RestaurantManagement.Core.Services;

public class PlateServiceImplementation : IPlateService
{
    private readonly ApplicationDbContext _context;

    public PlateServiceImplementation(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PlateResponse>> GetAllPlatesAsync()
    {
        return await _context.Plates
            .Select(p => new PlateResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                IsAvailable = p.IsAvailable
            })
            .ToListAsync();
    }

    public async Task<PlateResponse> GetPlateByIdAsync(Guid id)
    {
        var plate = await _context.Plates.FindAsync(id);
        if (plate == null)
            throw new Exception("Plate not found");

        return new PlateResponse
        {
            Id = plate.Id,
            Name = plate.Name,
            Description = plate.Description,
            Price = plate.Price,
            ImageUrl = plate.ImageUrl,
            IsAvailable = plate.IsAvailable
        };
    }

    public async Task<PlateResponse> CreatePlateAsync(PlateRequest request)
    {
        var plate = new Plate
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            ImageUrl = request.ImageUrl,
            IsAvailable = request.IsAvailable,
            CreatedAt = DateTime.UtcNow
        };

        _context.Plates.Add(plate);
        await _context.SaveChangesAsync();

        return new PlateResponse
        {
            Id = plate.Id,
            Name = plate.Name,
            Description = plate.Description,
            Price = plate.Price,
            ImageUrl = plate.ImageUrl,
            IsAvailable = plate.IsAvailable
        };
    }

    public async Task<PlateResponse> UpdatePlateAsync(Guid id, PlateRequest request)
    {
        var plate = await _context.Plates.FindAsync(id);
        if (plate == null)
            throw new Exception("Plate not found");

        plate.Name = request.Name;
        plate.Description = request.Description;
        plate.Price = request.Price;
        plate.ImageUrl = request.ImageUrl;
        plate.IsAvailable = request.IsAvailable;
        plate.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new PlateResponse
        {
            Id = plate.Id,
            Name = plate.Name,
            Description = plate.Description,
            Price = plate.Price,
            ImageUrl = plate.ImageUrl,
            IsAvailable = plate.IsAvailable
        };
    }

    public async Task DeletePlateAsync(Guid id)
    {
        var plate = await _context.Plates.FindAsync(id);
        if (plate == null)
            throw new Exception("Plate not found");

        _context.Plates.Remove(plate);
        await _context.SaveChangesAsync();
    }
} 