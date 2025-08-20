using Microsoft.EntityFrameworkCore;
using Pawmence.API.Data;
using Pawmence.API.Entities;

namespace Pawmence.API.Services;

public sealed class MatchService : IMatchService
{
    private readonly UserDbContext _db;

    public MatchService(UserDbContext db) { _db = db; }

    public Task<bool> IsOwnedByCurrentUserAsync(int profileId, int currentUserId, CancellationToken ct) =>
        _db.DogProfiles.AnyAsync(p => p.Id == profileId && p.UserId == currentUserId && p.IsActive,
            ct);

    public async Task<LikeOutcome> LikeAsync(int fromId, int toId, CancellationToken ct)
    {
        await using var tx = await _db.Database.BeginTransactionAsync(ct);
        
        var like = await _db.Likes.FindAsync(new object[] { fromId, toId }, ct);
        if (like is null)
        {
            _db.Likes.Add(new Like { FromProfileId = fromId, ToProfileId = toId });
            await _db.SaveChangesAsync(ct);
        }
        
        var mutual = await _db.Likes.FindAsync(new object[] { toId, fromId }, ct);
        if (mutual is null)
        {
            await tx.CommitAsync(ct);
            return new LikeOutcome(LikeOutcomeStatus.Liked);
        }
        
        var match = new Match { ProfileA = fromId, ProfileB = toId };
        _db.Matches.Add(match);
        try
        {
            await _db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException)
        {
            match = await _db.Matches
                .OrderByDescending(m => m.CreatedAt)
                .FirstAsync(ct);
        }

        await tx.CommitAsync(ct);
        
        return new LikeOutcome(LikeOutcomeStatus.Matched, match.Id);
    }
}