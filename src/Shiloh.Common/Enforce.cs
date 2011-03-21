using System;
using System.Linq.Expressions;
using System.Reflection;
using Shiloh.Common.Extensions;
using Shiloh.Common.Reflection;


namespace Shiloh.Common
{
	public static class Enforce
	{
		/// <summary>
		/// Ensure's a parameter is not null by passing in a lambda expression. Throws and error with a meaningful error message if the parameter is null.
		/// </summary>
		/// <example>
		/// Enforce.ParameterNotNull( () => paramName );
		/// </example>
		/// <param name="paramExpression">The param expression.</param>
		public static void ParameterNotNull( Expression< Func< object > > paramExpression )
		{
			EnforceThatValueIsNotNull( paramExpression, "This is a required parameter. Its value should not be null." );
		}


		/// <summary>
		/// Ensure's a variable is not null by passing in a lambda expression. Throws and error with a meaningful error message if the variable is null.
		/// </summary>
		/// <example>
		/// Enforce.VariableIsNotNull( () => paramName );
		/// </example>
		/// <param name="variableExpression">The variable expression.</param>
		public static void VariableIsNotNull( Expression< Func< object > > variableExpression )
		{
			EnforceThatValueIsNotNull( variableExpression, "This variable's value should not be null." );
		}


		/// <summary>
		/// Ensure's a parameter is of a specific type by passing in a lambda expression. Throws and error with a meaningful error message if the parameter is not of the expected type.
		/// </summary>
		/// <typeparam name="T">The type that the specified parameter should be an instance of</typeparam>
		/// <param name="paramExpression">The param expression.</param>
		/// <remarks>
		/// Idea and some logic taken from http://abdullin.com/journal/2008/12/13/how-to-find-out-variable-or-parameter-name-in-c.html
		/// </remarks>
		/// <example>
		/// Enforce.ParameterIsOfType( () =&gt; paramName );
		/// </example>
		public static void ParameterIsOfType< T >( Expression< Func< object > > paramExpression )
		{
			var paramExpressionBody = (MemberExpression)paramExpression.Body;
			object paramValue = ( (FieldInfo)paramExpressionBody.Member ).GetValue( ( (ConstantExpression)paramExpressionBody.Expression ).Value );

			if ( !( paramValue is T ) )
			{
				string paramName = paramExpressionBody.Member.Name;
				MethodBase method = ReflectionHelper.GetCallingMethod();

				string message = "The value supplied for parameter [ {parameter} ] was of the wrong type.\nExpected [ {expectedTypeName} ], but was [ {actualTypeName} ]./nError encountered when calling [ {className}.{methodName} ]"
						.Format( parameter => paramName,
						         expectedTypeName => typeof ( T ).Name,
						         actualTypeName => paramValue.GetType().Name,
						         className => method.DeclaringType.FullName,
						         methodName => method.Name );

				throw new ArgumentException( message );
			}
		}


		public static void ParameterIsDateWithoutTime( Expression< Func< DateTime > > paramExpression )
		{
			var paramExpressionBody = (MemberExpression)paramExpression.Body;
			var paramValue = (DateTime)( (FieldInfo)paramExpressionBody.Member ).GetValue( ( (ConstantExpression)paramExpressionBody.Expression ).Value );

			if ( paramValue != paramValue.Date && paramValue != DateTime.MinValue && paramValue != DateTime.MaxValue )
			{
				string paramName = paramExpressionBody.Member.Name;
				MethodBase method = ReflectionHelper.GetCallingMethod();

				string message = "Expected Date, but got DateTime for parameter [ {parameter} ]./nError encountered when calling [ {className}.{methodName} ]"
						.Format( parameter => paramName,
						         className => method.DeclaringType.FullName,
						         methodName => method.Name );

				throw new ArgumentException( message );
			}
		}


		public static void IsDateWithoutTime( DateTime date )
		{
			if ( date != date.Date && date != DateTime.MinValue && date != DateTime.MaxValue )
			{
				MethodBase method = ReflectionHelper.GetCallingMethod();

				string message = "Expected Date, but got DateTime for parameter./nError encountered when calling [ {className}.{methodName} ]"
						.Format( className => method.DeclaringType.FullName,
						         methodName => method.Name );

				throw new ArgumentException( message );
			}
		}


		#region Privates

		/// <summary>
		/// Enforces the that value is not null.
		/// </summary>
		/// <param name="valueExpression">The value expression.</param>
		/// <param name="customErrorMessage">The custom error message.</param>
		/// <remarks>
		/// Idea and some logic taken from http://abdullin.com/journal/2008/12/13/how-to-find-out-variable-or-parameter-name-in-c.html
		/// </remarks>
		static void EnforceThatValueIsNotNull( Expression< Func< object > > valueExpression, string customErrorMessage )
		{
			var expressionBody = (MemberExpression)valueExpression.Body;
			object expressionValue = ( (FieldInfo)expressionBody.Member ).GetValue( ( (ConstantExpression)expressionBody.Expression ).Value );

			if ( expressionValue == null )
			{
				// To get the source method, we have to go to the caller of the caller (since the call is two layers deep to get here from the source).
				MethodBase callingMethod = ReflectionHelper.GetCallingMethodOfCallingMethod();
				string errorMessage = "Null value encountered for [ " + expressionBody.Member.Name + " ].\n" +
				                      customErrorMessage + "\n" +
				                      "Error encountered in method [ " + callingMethod.DeclaringType.FullName + "." + callingMethod.Name + " ]";
				throw new ArgumentNullException( errorMessage );
			}
		}

		#endregion
	}
}