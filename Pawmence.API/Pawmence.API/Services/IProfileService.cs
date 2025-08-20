using Pawmence.API.DTOs;
using Pawmence.API.Entities;

namespace Pawmence.API.Services;

public interface IProfileService
{
    Task<DogProfile> CreateAsync(int userId, CreateProfileRequestDto dto);
}