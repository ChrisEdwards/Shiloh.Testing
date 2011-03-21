using System;
using Machine.Specifications;

// ReSharper disable InconsistentNaming


namespace Shiloh.Common.Specs
{
	[ Subject( typeof ( Enforce ) ) ]
	public class Enforcing_that_a_parameter_is_not_null
	{
		protected static string _parameterToTest;
		protected static Exception Exception;

		Because of = () => Exception = Catch.Exception( () => TestMethod( _parameterToTest ) );


		protected static void TestMethod( string parameterValue )
		{
			Enforce.ParameterNotNull( () => parameterValue );
		}
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class When_the_parameter_is_null : Enforcing_that_a_parameter_is_not_null
	{
		Establish context = () => _parameterToTest = null;
		It should_fail = () => Exception.ShouldBeOfType< ArgumentException >();
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class When_the_parameter_is_not_null : Enforcing_that_a_parameter_is_not_null
	{
		Establish context = () => _parameterToTest = "this is not null";
		It should_pass = () => Exception.ShouldBeNull();
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class Enforcing_that_a_variable_is_not_null
	{
		protected static string _valueToTest;
		protected static Exception Exception;

		Because of = () =>
		             	{
		             		string variable = _valueToTest;
		             		Exception = Catch.Exception( () => Enforce.VariableIsNotNull( () => variable ) );
		             	};
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class When_the_variable_is_null : Enforcing_that_a_variable_is_not_null
	{
		Establish context = () => _valueToTest = null;
		It should_fail = () => Exception.ShouldBeOfType< ArgumentException >();
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class When_the_variable_is_not_null : Enforcing_that_a_variable_is_not_null
	{
		Establish context = () => _valueToTest = "this is not null";
		It should_pass = () => Exception.ShouldBeNull();
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class Enforcing_that_a_parameter_is_a_specified_type
	{
		protected static string _stringParameter = "test vaue";
		protected static int _intParameter = 10;
		protected static Exception Exception;


		protected static void TestMethod( object parameterValue )
		{
			Enforce.ParameterIsOfType< string >( () => parameterValue );
		}
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class When_the_parameter_matches_the_specified_type : Enforcing_that_a_parameter_is_a_specified_type
	{
		Because of = () => Exception = Catch.Exception( () => TestMethod( _stringParameter ) );
		It should_pass = () => Exception.ShouldBeNull();
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class When_the_parameter_does_not_match_the_specified_type : Enforcing_that_a_parameter_is_a_specified_type
	{
		Because of = () => Exception = Catch.Exception( () => TestMethod( _intParameter ) );
		It should_fail = () => Exception.ShouldBeOfType< ArgumentException >();
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class Enforcing_that_a_parameter_is_a_date_with_no_time
	{
		protected static Exception Exception;


		protected static void TestMethod( DateTime value )
		{
			Enforce.ParameterIsDateWithoutTime( () => value );
		}
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class When_the_parameter_is_a_date_with_no_time : Enforcing_that_a_parameter_is_a_date_with_no_time
	{
		Because of = () => Exception = Catch.Exception( () => TestMethod( new DateTime( 2011, 02, 03 ) ) );
		It should_pass = () => Exception.ShouldBeNull();
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class When_the_parameter_is_a_date_that_includes_a_time : Enforcing_that_a_parameter_is_a_date_with_no_time
	{
		Because of = () => Exception = Catch.Exception( () => TestMethod( new DateTime( 2011, 02, 03, 8, 50, 0 ) ) );
		It should_fail = () => Exception.ShouldBeOfType< ArgumentException >();
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class Enforcing_that_a_date_value_has_no_time
	{
		protected static DateTime _dateTimeValue;
		protected static Exception Exception;
		Because of = () => Exception = Catch.Exception( () => Enforce.IsDateWithoutTime( _dateTimeValue ) );
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class When_the_date_has_no_time : Enforcing_that_a_date_value_has_no_time
	{
		Establish context = () => _dateTimeValue = new DateTime( 2011, 02, 03 );
		It should_pass = () => Exception.ShouldBeNull();
	}


	[ Subject( typeof ( Enforce ) ) ]
	public class When_the_date_includes_a_time : Enforcing_that_a_date_value_has_no_time
	{
		Establish context = () => _dateTimeValue = new DateTime( 2011, 02, 03, 8, 50, 0 );
		It should_fail = () => Exception.ShouldBeOfType< ArgumentException >();
	}
}


// ReSharper enable InconsistentNaming