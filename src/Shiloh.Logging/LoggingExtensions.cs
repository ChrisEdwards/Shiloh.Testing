using System;
using System.Data;
using log4net;
using Shiloh.Common.Extensions;
using Shiloh.Common.Reflection;


namespace Shiloh.Logging
{
	public static class LoggingExtensions
	{
		public static void LogDbCommand( this ILog log, IDbCommand command )
		{
			// Log command call.
			log.Debug( "SQL COMMAND: \n" + command.CommandText );

			// Log parameters.
			foreach ( IDataParameter parameter in command.Parameters )
				log.Debug( String.Format( "SQL PARAM: Name='{0}' Value='{1}'", parameter.ParameterName, parameter.Value ) );
		}


		public static void Parameter( this ILog log, string name, params string[] values )
		{
			log.Debug( "   parameter: " + name + " = { " + values.AsDelimitedString( ", " ) + " }" );
		}


		public static void Variable( this ILog log, string name, params string[] values )
		{
			log.Debug( "   variable: " + name + " = { " + values.AsDelimitedString( ", " ) + " }" );
		}


		public static void MethodStart( this ILog log )
		{
			log.Debug( "Start: " + ReflectionHelper.GetCallingMethod().Name );
		}


		public static void MethodFinish( this ILog log )
		{
			log.Debug( "Finish: " + ReflectionHelper.GetCallingMethod().Name );
		}
	}
}