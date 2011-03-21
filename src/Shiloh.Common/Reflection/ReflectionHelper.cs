using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace Shiloh.Common.Reflection
{
	public static class ReflectionHelper
	{
		/*
		public static bool IsMethodExpression< MODEL >( Expression< Func< MODEL, object > > expression )
		{
			return expression.Body is MethodCallExpression;
		}


		public static bool IsPropertyExpression< MODEL >( Expression< Func< MODEL, object > > expression )
		{
			return getMemberExpression( expression, false ) != null;
		}
        */


		public static MemberInfo GetMember< MODEL >( Expression< Func< MODEL, object > > expression )
		{
			MemberExpression memberExpression = getMemberExpression( expression );
			return memberExpression.Member;
		}


		/*
		public static FieldInfo GetField< MODEL >( Expression< Func< MODEL, object > > expression )
		{
			return (FieldInfo)GetMember( expression );
		}
        */


		public static PropertyInfo GetProperty< MODEL >( Expression< Func< MODEL, object > > expression )
		{
			return (PropertyInfo)GetMember( expression );
		}


		/*
		public static PropertyInfo GetProperty< MODEL, T >( Expression< Func< MODEL, T > > expression )
		{
			MemberExpression memberExpression = getMemberExpression( expression );
			return (PropertyInfo)memberExpression.Member;
		}
        */


		static MemberExpression getMemberExpression< MODEL, T >( Expression< Func< MODEL, T > > expression )
		{
			return getMemberExpression( expression, true );
		}


		/// <summary>
		/// Gets the member expression.
		/// </summary>
		/// <typeparam name="MODEL">The type containing the member that the provided expression accesses.</typeparam>
		/// <typeparam name="T">The return type of the member the provided expression accesses.</typeparam>
		/// <param name="expression">The member expression expressed as an Expression.</param>
		/// <param name="enforceCheck">if set to <c>true</c> throw an error if it is not an expression that accesses a member property. If <c>false</c>, return a null in that case.</param>
		/// <returns></returns>
		static MemberExpression getMemberExpression< MODEL, T >( Expression< Func< MODEL, T > > expression, bool enforceCheck )
		{
			MemberExpression memberExpression = null;
			if ( expression.Body.NodeType == ExpressionType.Convert )
			{
				var body = (UnaryExpression)expression.Body;
				memberExpression = body.Operand as MemberExpression;
			}
			else if ( expression.Body.NodeType == ExpressionType.MemberAccess )
				memberExpression = expression.Body as MemberExpression;

			if ( enforceCheck && memberExpression == null )
				throw new ArgumentException( "Not a member access", "member" );
			return memberExpression;
		}


		/*
		public static MethodInfo GetMethod< T >( Expression< Func< T, object > > expression )
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}


		public static MethodInfo GetMethod< T, U >( Expression< Func< T, U > > expression )
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}


		public static MethodInfo GetMethod< T, U, V >( Expression< Func< T, U, V > > expression )
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}


		public static MethodInfo GetMethod( Expression< Func< object > > expression )
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}

		public static MethodBase GetCurrentMethod()
		{
			var stackTrace = new StackTrace();
			StackFrame stackFrame = stackTrace.GetFrame( 1 );
			return stackFrame.GetMethod();
		}
        */


		public static MethodBase GetCallingMethod()
		{
			var stackTrace = new StackTrace();
			StackFrame stackFrame = stackTrace.GetFrame( 2 );
			return stackFrame.GetMethod();
		}


		public static MethodBase GetCallingMethodOfCallingMethod()
		{
			var stackTrace = new StackTrace();
			StackFrame stackFrame = stackTrace.GetFrame( 3 );
			return stackFrame.GetMethod();
		}


		/*
		public static string GetNameValuePairString( Expression< Func< object > > paramExpression )
		{
			var paramExpressionBody = (MemberExpression)paramExpression.Body;
			string paramName = paramExpressionBody.Member.Name;
			object paramValue = ( (FieldInfo)paramExpressionBody.Member ).GetValue( ( (ConstantExpression)paramExpressionBody.Expression ).Value );
			return paramName + "=" + paramValue;
		}
        */


		/// <summary>
		/// Gets the name value pairs from several lambda expressions.
		/// <code>bob => "test", larry => object1</code> 
		/// yeilds
		/// <code>Dictionary<string, object></code> containing string keys of "bob" and "larry" with corresponding object values of "test" and object1 respectively.
		/// </summary>
		/// <param name="args">The args.</param>
		/// <returns></returns>
		public static Dictionary< string, object > GetNameValuePairsFromLambdas( Expression< Func< string, object > >[] args )
		{
			return args.ToDictionary(
					e => e.Parameters[0].Name,
					e => e.Compile()( e.Parameters[0].Name ) );
		}
	}
}