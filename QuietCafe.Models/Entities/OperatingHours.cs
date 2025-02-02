using QuietCafe.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace QuietCafe.Shared.Entities;

public class OperatingHours(DbKey<Location> locationId, DayOfWeek dayOfWeek, bool isOpen24Hours, TimeOnly openTime, TimeOnly closeTime)
{
    public DbKey<OperatingHours> Id { get; init; } = new();
    public DbKey<Location> LocationId { get; init; } = locationId;

    public DayOfWeek DayOfWeek { get; init; } = dayOfWeek;
    public bool IsOpen24Hours { get; set; } = isOpen24Hours;
    public TimeOnly? OpenTime { get; set; } = openTime;
    public TimeOnly? CloseTime { get; set; } = closeTime;

    public bool IsOpen(DateTime time)
    {
        var dayOfWeek = time.DayOfWeek;
        if(dayOfWeek != DayOfWeek)
        {
            return false;
        }

        if (IsOpen24Hours)
        {
            return true;
        }

        var ticks = time.TimeOfDay.Ticks;

        //we know OpenTime and CloseTime are not null because of the IsOpen24Hours check
        return ticks >= OpenTime?.Ticks && ticks <= CloseTime?.Ticks;
    }

    public bool Overlaps(TimeOnly openTime, TimeOnly closeTime)
    {
        if (IsOpen24Hours)
        {
            return true;
        }

        if (OpenTime is null || CloseTime is null)
        {
            throw new Exception("Invalid operating hours.");
        }
        else
        {
            var open = (TimeOnly)OpenTime;
            var close = (TimeOnly)CloseTime;

            return openTime.IsBetween(open, close) || closeTime.IsBetween(open, close);
        }
    }
}
