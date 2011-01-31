using System;


namespace Shiloh.Testing.Extensions
{
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Converts a datetime to a sql date time with milli seconds.
		/// </summary>
		/// <param name="date">The date to convert.</param>
		/// <returns>String with the format of the date with time and milliseconds</returns>
		public static string ToDateStringWithMilliSeconds( this DateTime date )
		{
			return String.Format( "{0:yyyy-MM-dd HH:mm:ss.fff}", date );
		}


		/// <summary>
		/// Strips the milliseconds from the datetime.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The date with no milliseconds.</returns>
		public static DateTime StripMilliseconds( this DateTime value )
		{
			return new DateTime(
					value.Year,
					value.Month,
					value.Day,
					value.Hour,
					value.Minute,
					value.Second );
		}
	}
}