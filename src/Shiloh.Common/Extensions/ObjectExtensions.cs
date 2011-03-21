using System;
using System.Reflection;
using System.Text;
using Shiloh.Testing.Reflection;


namespace Shiloh.Common.Extensions
{
	public static class ObjectExtensions
	{
		/// <summary>
		/// Generates a dynamic string showing the type of the class and all its properties using reflection. String is in the form:<br/>
		/// <code>{{ TypeName { Property1 = [ Value1 ]; Property2 = [ Value2 ]; ... PropertyN = [ Value N ]; } }}</code>
		/// </summary>
		/// <typeparam name="T">The type of object we are wanting to print.</typeparam>
		/// <param name="objectToPrint">The object to print.</param>
		/// <returns>String describing the object.</returns>
		public static string ToPrintableString< T >( this T objectToPrint ) where T : class
		{
			Type type = typeof ( T );

			var sb = new StringBuilder();
			sb.AppendFormat( "{{{{ {0} {{ ", type.Name );

			foreach ( PropertyInfo property in type.GetPublicGetProperties() )
			{
				sb.AppendFormat( "{0} = [ {1} ]; ",
				                 property.Name,
				                 property.GetValue( objectToPrint, null ) );
			}

			sb.Append( "} }}" );

			return sb.ToString();
		}
	}
}