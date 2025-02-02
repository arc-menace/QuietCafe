using QuietCafe.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuietCafe.Shared.Entities;

/// <summary>
/// Represents a single log of activity at a location provided from the Raspberry Pi.
/// </summary>
/// <param name="locationId"></param>
/// <param name="timestamp"></param>
/// <param name="numberOfPeople"></param>
/// <param name="confidenceValues"></param>
public class LocationActivityLog(DbKey<Location> locationId, DateTime timestamp, int numberOfPeople, double[]? confidenceValues)
{
    public DbKey<LocationActivityLog> Id { get; init; } = new();
    public DbKey<Location> LocationId { get; init; } = locationId;
    public DateTime Timestamp { get; init; } = timestamp;
    public int NumberOfPeople { get; init; } = numberOfPeople;
}
