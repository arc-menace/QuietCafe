using QuietCafe.Shared.Models;

namespace QuietCafe.Shared.Entities;

/// <summary>
/// Represents the calculated value shown to the user for a location.
/// This is updated less frequently than the raw data.
/// </summary>
/// <param name="locationId"></param>
/// <param name="created"></param>
/// <param name="numberOfPeople"></param>
public class LocationActivityLogRollup(DbKey<Location> locationId, DateTime created, int numberOfPeople)
{
    public DbKey<LocationActivityLogRollup> Id { get; init; } = new();
    public DbKey<Location> LocationId { get; init; } = locationId;
    public DateTime Created { get; init; } = created;
    public int NumberOfPeople { get; init; } = numberOfPeople;
}
