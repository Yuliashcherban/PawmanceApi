using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pawmence.API.Data;
using Pawmence.API.DTOs;
using Pawmence.API.Services;

namespace Pawmence.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profiles;
    private readonly UserDbContext _db;

    public ProfileController(IProfileService profiles, UserDbContext db)
    {
        _profiles = profiles;
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProfileRequestDto dto)
    {
        var idFromToken = User.FindFirstValue(ClaimTypes.NameIdentifier)
                          ?? User.FindFirstValue("sub");

        if (string.IsNullOrWhiteSpace(idFromToken) || !int.TryParse(idFromToken, out var userId))
            return Unauthorized(new { message = "Invalid or missing user id in token" });

        var profile = await _profiles.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById), new { id = profile.Id }, profile);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _db.DogProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return p is null ? NotFound() : Ok(p);
    }
}