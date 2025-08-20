using Microsoft.EntityFrameworkCore;
using Pawmence.API.Data;
using Pawmence.API.DTOs;
using Pawmence.API.Entities;

namespace Pawmence.API.Services;

public class ProfileService : IProfileService
{
    private readonly UserDbContext _db;

    public ProfileService(UserDbContext db)
    {
        _db = db;
    }

    public async Task<DogProfile> CreateAsync(int userId, CreateProfileRequestDto dto)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
            throw new KeyNotFoundException("User not found");

        var profile = new DogProfile
        {
            UserId = userId,
            Nickname = dto.Nickname,
            Breed = dto.Breed,
            Location = dto.Location,
            Age = dto.Age,
            Description = dto.Description,
            Photo = dto.Photo
        };

        _db.DogProfiles.Add(profile);

        if (!user.IsActive)
            user.IsActive = true;

        await _db.SaveChangesAsync();
        return profile;
    }
}