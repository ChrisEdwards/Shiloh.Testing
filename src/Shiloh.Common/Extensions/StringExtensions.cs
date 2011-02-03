using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Shiloh.Common.Reflection;


namespace Shiloh.Common.Extensions
{
	/// <summary>
	/// Adds useful extensions to the base string class.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Surrounds a string with brackets.
		/// </summary>
		/// <param name="theString">The string.</param>
		/// <returns></returns>
		public static string WithinBrackets( this string theString )
		{
			return theString.Within( "[", "]" );
		}


		/// <summary>
		/// Surrounds a string with braces
		/// </summary>
		/// <param name="theString">The name.</param>
		/// <returns></returns>
		public static string WithinBraces( this string theString )
		{
			return theString.Within( "{", "}" );
		}


		/// <summary>
		/// Surrounds a string with opening and closing strings.
		/// <code>"Bob".Within( "{", "}" ) = "{Bob}"</code>
		/// </summary>
		/// <param name="theString">The string.</param>
		/// <param name="opening">The opening.</param>
		/// <param name="closing">The closing.</param>
		/// <returns></returns>
		public static string Within( this string theString, string opening, string closing )
		{
			var toReturn = new StringBuilder();

			if ( !theString.StartsWith( opening ) )
				toReturn.Append( opening );

			toReturn.Append( theString );

			if ( !theString.EndsWith( closing ) )
				toReturn.Append( closing );

			return toReturn.ToString();
		}


		public static string format_using( this string format, params object[] args )
		{
			return String.Format( format, args );
		}


		/// <summary>
		/// Formats the specified string using syntax:<br/>
		/// <code>var str = "{foo} {bar} {baz}".Format( foo=>foo, bar=>2, baz=>new object() );</code>
		/// to result in:<code> "foo 2 System.Object" </code>
		/// </summary>
		/// <param name="str">The STR.</param>
		/// <param name="args">The args.</param>
		/// <returns></returns>
		public static string Format( this string str, params Expression< Func< string, object > >[] args )
		{
			Dictionary< string, object > parameters = ReflectionHelper.GetNameValuePairsFromLambdas( args );

			var sb = new StringBuilder( str );
			foreach ( KeyValuePair< string, object > valuePair in parameters )
				sb.Replace( valuePair.Key.WithinBraces(), valuePair.Value != null ? valuePair.Value.ToString() : String.Empty );

			string result = sb.ToString();

			return result;
		}


		/// <summary>
		/// Gets the camelCase version of this text.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public static string ToCamelCase( this string source )
		{
			string firstChar = source.Substring( 0, 1 );
			string restOfString = source.Remove( 0, 1 );
			return firstChar.ToLower() + restOfString;
		}


		public static string ToXml< T >( this T toSerialize, Type contract )
		{
			Stream xmlData = new MemoryStream();
			var serializer = new XmlSerializer( contract );
			serializer.Serialize( xmlData, toSerialize );
			xmlData.Seek( 0, SeekOrigin.Begin );
			return new StreamReader( xmlData ).ReadToEnd();
		}


		/// <summary>
		/// Validates wheter a string is a valid IPv4 address.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <returns>
		/// <c>true</c> if the specified string is a valid ip address; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsValidIpAddress( this string s )
		{
			return Regex.IsMatch( s,
			                      @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b" );
		}


		/// <summary>
		/// Strips the double quotes from the string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string StripQuotes( this string value )
		{
			return value.Replace( "\"", string.Empty );
		}


		/// <summary>
		/// Strips any whitespace chars from the string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string StripWhitespace( this string value )
		{
			return Regex.Replace( value, @"\s", string.Empty );
		}


		/// <summary>
		/// Checks if this string value exists in a list of string values.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="expectedValues">The expected values.</param>
		/// <returns></returns>
		public static bool In( this string value, params string[] expectedValues )
		{
			return expectedValues.Contains( value );
		}
	}
}