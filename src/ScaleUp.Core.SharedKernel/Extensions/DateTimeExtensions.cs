namespace ScaleUp.Core.SharedKernel.Extensions;

public static class DateTimeExtensions
{
    private static readonly DateTime _epoch = new DateTime(1970,
        1,
        1,
        0,
        0,
        0,
        DateTimeKind.Utc);

    public static DateTime DateFromTimestamp(this long timestamp)
    {
        return new DateTime(timestamp);
    }

    public static DateTime? DateFromTimestamp(this double? timestamp)
    {
        if (timestamp.HasValue && timestamp.Value != 0)
        {
            return _epoch.AddSeconds(timestamp.Value);
        }

        return null;
    }

    public static string? DateFromTimestamp(this double? timestamp, string timeZoneById, string format = "M/d/yyyy, h:mm:ss tt")
    {
        if (timestamp.HasValue && timestamp.Value != 0)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneById);
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(_epoch.AddSeconds(timestamp.Value), timeZone);

            return localTime.ToString("M/d/yyyy, h:mm:ss tt");
        }

        return null;
    }

    public static List<DateTime> GetDatesBetween(DateTime fromDate, DateTime toDate)
    {
        var dates = new List<DateTime>();

        for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
        {
            dates.Add(date);
        }

        return dates;
    }

    //
    public static string ToIso8601(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
    }

    public static string ToFileDate(this DateTime dateTime)
    {
        return dateTime.ToString("yyyyMMddTHHmmss");
    }
}