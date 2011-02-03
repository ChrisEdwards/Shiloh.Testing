using System;
using System.Collections.Generic;
using System.Linq;


namespace Shiloh.Common.Extensions
{
	public static class EnumerableExtensions
	{
		// public static IEnumerable< T > one_at_a_time< T >( this IEnumerable< T > items )
		// {
		// return items.Select( item => item );
		// }
		public static string AsDelimitedString< T >( this IEnumerable< T > items, string delimiter )
		{
			Enforce.ParameterNotNull( () => items );
			string[] itemStrings = items.Select( item => item == null ? string.Empty : item.ToString() ).ToArray();
			return string.Join( delimiter, itemStrings );
		}


		public static void each< T >( this IEnumerable< T > items, Action< T > action )
		{
			foreach ( T item in items ) action( item );
		}


		// public static IEnumerable< int > to( this int start, int end )
		// {
		// for ( int i = start; i <= end; i++ ) yield return i;
		// }

		/*
		/// <summary>
		/// Adds the specified item to the collection so long as no other object with the same identity exists.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">The list to add to.</param>
		/// <param name="itemToAdd">The item to add.</param>
		/// <param name="hasSameIdentityAsItemToAdd">The predicate to use to determine if any list item has same identity as item to add.</param>
		/// <returns></returns>
		public static bool AddIfUnique< T >( this ICollection< T > list, T itemToAdd, Func< T, bool > hasSameIdentityAsItemToAdd ) where T : class
		{
			if ( itemToAdd == null || list.Any( hasSameIdentityAsItemToAdd ) )
				return false;

			// Cache the object.
			list.Add( itemToAdd );
			return true;
		}


		/// <summary>
		/// Adds the specified item to the collection so long as no other object with the same identity exists.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">The list to add to.</param>
		/// <param name="itemToAdd">The item to add.</param>
		/// <param name="identityComparator">The comparator.</param>
		/// <returns></returns>
		public static bool AddIfUnique< T >( this ICollection< T > list, T itemToAdd ) where T : class, IComparableByIdentity< T >
		{
			if ( itemToAdd == null || list.Any( x => itemToAdd.CompareByIdentity( x, itemToAdd ) ) )
				return false;

			// Cache the object.
			list.Add( itemToAdd );
			return true;
		}


		/// <summary>
		/// Adds the specified string to the collection so long as it doesn't already exist in the collection.
		/// </summary>
		/// <param name="list">The list to add to.</param>
		/// <param name="itemToAdd">The item to add.</param>
		/// <returns></returns>
		public static bool AddIfUnique( this ICollection< string > list, string itemToAdd )
		{
			if ( itemToAdd == null || list.Any( x => itemToAdd == x ) )
				return false;

			// Cache the object.
			list.Add( itemToAdd );
			return true;
		}
        */
	}
}