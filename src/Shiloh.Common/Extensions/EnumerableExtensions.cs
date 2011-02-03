using System.Collections.Generic;
using System.Linq;


namespace Shiloh.Common.Extensions
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Calls ToString() on all the items in enumeration and joins them with the specified delimiter.
		/// </summary>
		/// <typeparam name="T">The type of object.</typeparam>
		/// <param name="items">The items.</param>
		/// <param name="delimiter">The delimiter.</param>
		/// <returns>A string of all the values separated by the specified delimiter.</returns>
		public static string AsDelimitedString< T >( this IEnumerable< T > items, string delimiter )
		{
			Enforce.ParameterNotNull( () => items );
			string[] itemStrings = items.Select( item => item == null ? string.Empty : item.ToString() ).ToArray();
			return string.Join( delimiter, itemStrings );
		}
	}
}