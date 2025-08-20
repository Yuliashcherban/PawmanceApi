using System.Threading;
using System.Threading.Tasks;

namespace Pawmence.API.Services;

public enum LikeOutcomeStatus { Liked, Matched }
public record LikeOutcome(LikeOutcomeStatus Status, int? MatchId = null, int? ChatId = null);

public interface IMatchService
{
    Task<bool> IsOwnedByCurrentUserAsync(int profileId, int currentUserId, CancellationToken ct);
    Task<LikeOutcome> LikeAsync(int fromId, int toId, CancellationToken ct);
}