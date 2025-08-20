using Pawmence.API.DTOs;
using Pawmence.API.Entities;
using Pawmence.API.Models;

namespace Pawmence.API.Services;

public partial interface IAuthService
{
    public Task<User?> RegisterAsync(RegisterDtoRequest request);
    public Task<TokenResponseDto?> LoginAsync(LoginRequestDto request);
    public Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
}