using Microsoft.AspNetCore.Mvc;
using Pawmence.API.Services;

namespace Pawmence.API.Controllers;

public record LikeRequest(string FromProfileId, string ToProfileId);

[ApiController]
[Route("api/match")]
public class MatchController(IMatchService service) : ControllerBase
{
}