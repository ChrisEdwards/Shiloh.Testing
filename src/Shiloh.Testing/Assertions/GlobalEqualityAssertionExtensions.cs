using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Shiloh.Testing.Extensions;
using Shiloh.Testing.Reflection;


namespace Shiloh.Testing.Assertions
{
	public static class GlobalEqualityAssertionExtensions
	{
		/// <summary>
		/// Dynamically compares the common publicly accessible property values of two objects of differing types to see if they are alike.
		/// </summary>
		/// <typeparam name="TActual">The type of the actual.</typeparam>
		/// <typeparam name="TExpected">The type of the expected.</typeparam>
		/// <param name="actual">The actual.</param>
		/// <param name="expected">The expected.</param>
		/// <remarks>
		/// You can use the <see cref="IgnorePropertyWhenAssertingEqualityAttribute"/> to ignore a property during comparison.
		/// </remarks>
		public static void should_be_equal_to< TActual, TExpected >( this TActual actual, TExpected expected )
		{
			foreach ( PropertyPair propertyPair in GetPropertiesToCompare( actual, expected ) )
			{
				// Get the values to compare, using the CleanupValue method as an opportunity to cleanup certain value types. For instance, not comparing milliseconds for DateTime.
				object actualValue = CleanupValue( propertyPair.Actual.GetValue( actual, null ) );
				object expectedValue = CleanupValue( propertyPair.Expected.GetValue( expected, null ) );

				// Perform the assertion writing the property name to the error message if the assertion fails.
				Assert.That( actualValue,
				             Is.EqualTo( expectedValue ),
				             string.Format( "Expected {0} to match, but did not.", propertyPair.Expected.Name ) );
			}
		}


		/// <summary>
		/// Gets the properties to compare by finding all public properties from both objects that have the same name and type..
		/// </summary>
		/// <typeparam name="TActual">The type of the actual.</typeparam>
		/// <typeparam name="TExpected">The type of the expected.</typeparam>
		/// <param name="actual">The actual.</param>
		/// <param name="expected">The expected.</param>
		/// <returns>All the pairs of properties that were public and had matching names and types.</returns>
		private static IEnumerable< PropertyPair > GetPropertiesToCompare< TActual, TExpected >( TActual actual, TExpected expected )
		{
			return
					from exp in GetComparableProperties( expected )
					join act in GetComparableProperties( actual )
							on new {exp.Name, exp.PropertyType}
							equals new {act.Name, act.PropertyType}
					select new PropertyPair {Actual = act, Expected = exp};
		}


		/// <summary>
		/// Gets the comparable properties...i.e. properties that are public and readable, and have not been attributed with the "IgnoreValueWhenAssertingEquality" attribute.
		/// </summary>
		/// <typeparam name="T">The type of the object to get the properties from.</typeparam>
		/// <param name="actual">The actual.</param>
		/// <returns>All the possible properties we could use for asserting equality.</returns>
		private static IEnumerable< PropertyInfo > GetComparableProperties< T >( T actual )
		{
			return
					actual.GetType().GetPublicGetProperties()
							.Where( property =>
							        !property.PropertyType.IsClass &&
							        !property.PropertyType.IsInterface &&
							        !PropertyHasIgnoreForEqualityAttribute( property )
							);
		}


		/// <summary>
		/// Determine if the property is attributed with the IgnorePropertyWhenAssertingEquality Attribute.
		/// </summary>
		/// <param name="propertyInfo">The property info.</param>
		/// <returns>true if attribute exists, else false.</returns>
		private static bool PropertyHasIgnoreForEqualityAttribute( PropertyInfo propertyInfo )
		{
			object[] customAttributes = propertyInfo.GetCustomAttributes( typeof ( IgnorePropertyWhenAssertingEqualityAttribute ), true );
			return customAttributes.Length > 0;
		}


		/// <summary>
		/// Cleans up the value before comparison based on its type.
		/// </summary>
		/// <param name="value">The raw value.</param>
		/// <returns>The cleaned up value.</returns>
		private static object CleanupValue( object value )
		{
			if ( value is DateTime )
				return ( (DateTime)value ).StripMilliseconds();

			// No cleanup was necessary for this type, just return it.
			return value;
		}


		#region Nested type: PropertyPair

		private class PropertyPair
		{
			public PropertyInfo Expected { get; set; }
			public PropertyInfo Actual { get; set; }
		}

		#endregion
	}
}