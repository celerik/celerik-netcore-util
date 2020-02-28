using System;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Utility to get the localized DateTime of a particular time zone.
    /// </summary>
    public class LocalDate
    {
        /// <summary>
        /// The current time zone.
        /// </summary>
        private readonly TimeZoneInfo _timeZone;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="utcOffset">The difference between this time zone and UTC.
        /// The int part represent hours, and the float part represents minutes.</param>
        public LocalDate(double utcOffset)
        {
            var hours = (int)utcOffset;
            var minutes = (int)(Math.Round(utcOffset - Math.Truncate(utcOffset), 2) * 100);

            _timeZone = TimeZoneInfo.CreateCustomTimeZone(
                id: $"UTC {utcOffset}",
                baseUtcOffset: new TimeSpan(hours, minutes, 0),
                displayName: $"UTC {utcOffset}",
                standardDisplayName: $"UTC {utcOffset}"
            );
        }

        /// <summary>
        /// Gets the current DateTime of this zone.
        /// </summary>
        public DateTime Now =>
            TimeZoneInfo.ConvertTime(DateTime.UtcNow, _timeZone);

        /// <summary>
        /// Converts the passed-in system DateTime to this time zone.
        /// </summary>
        /// <param name="systemTime">The system DateTime to be converted.</param>
        /// <returns>System DateTime converted to this time zone.</returns>
        public DateTime FromSystemTime(DateTime systemTime) =>
            TimeZoneInfo.ConvertTime(systemTime.ToUniversalTime(), _timeZone);
    }
}
