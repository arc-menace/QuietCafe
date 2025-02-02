using QuietCafe.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuietCafe.Shared.Entities;

/// <summary>
/// Represents a location where the Raspberry Pi is deployed.
/// </summary>
public class Location
{
    public DbKey<Location> Id { get; init; } = new();
    public string Name { get; set; } = string.Empty;

    public DateTime? LastRollupCalculation { get; init; }
    public int NumberOfPeople { get; init; }

    public List<LocationActivityLog> ActivityLogs { get; set; } = new();
    public LocationActivityLogRollup? ActivityLogRollup { get; set; }

    public List<OperatingHours> Hours { get; private set; }

    public Location(string name)
    {
        Name = name;
        Hours = new List<OperatingHours>();
    }

    public bool IsOpen(DateTime time)
    {
        var dayOfWeek = time.DayOfWeek;

        // Get the store hours for the given day. The store may open and close multiple times in a day.
        var storeHours = Hours.Where(h => h.DayOfWeek == dayOfWeek).ToList();

        // If the store is not open on the given day, return false.
        if (storeHours is null)
        {
            return false;
        }

        // Check if the time provided is within any of the store hours.
        return storeHours.Any(h => h.IsOpen(time));
    }

    public void AddStoreHours(DayOfWeek dayOfWeek, bool isOpen24Hours, TimeOnly openTime, TimeOnly closeTime)
    {
        if(closeTime > openTime)
        {
            throw new ArgumentException("Close time must be after open time.");
        }

        //check if new store hours overlap with existing store hours
        var existingStoreHours = Hours.Where(x => x.DayOfWeek == dayOfWeek).ToList();

        if (existingStoreHours.Any() && isOpen24Hours)
        {
            throw new ArgumentException("Cannot add 24 hour store hours when store hours already exist for the day.");
        }

        if (existingStoreHours.Any(x => x.Overlaps(openTime, closeTime)))
        {
            throw new ArgumentException("Store hours cannot overlap.");
        }

        Hours.Add(new OperatingHours(Id, dayOfWeek, isOpen24Hours, openTime, closeTime));
    }
}
