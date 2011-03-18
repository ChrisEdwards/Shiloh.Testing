using System.Collections.Generic;
using Machine.Specifications;
using Shiloh.Common.Extensions;

// ReSharper disable InconsistentNaming


namespace Shiloh.Common.Specs.Extensions
{
	public class When_joining_an_enumberable_into_a_delimited_string
	{
		protected static string _result;
		protected static IEnumerable< string > _items;
		private Because of = () => _result = _items.AsDelimitedString( "," );
	}


	[ Subject( typeof ( EnumerableExtensions ) ) ]
	public class When_the_enumerable_has_multiple_items : When_joining_an_enumberable_into_a_delimited_string
	{
		private Establish context = () => _items = new List< string > {"1", "2", "3"};
		private It should_return_the_items_separated_by_the_specified_delimiter = () => _result.ShouldEqual( "1,2,3" );
	}


	[ Subject( typeof ( EnumerableExtensions ) ) ]
	public class When_the_enumerable_has_one_items : When_joining_an_enumberable_into_a_delimited_string
	{
		private Establish context = () => _items = new List< string > {"1"};
		private It should_return_the_single_item_without_the_delimiter = () => _result.ShouldEqual( "1" );
	}

	[ Subject( typeof ( EnumerableExtensions ) ) ]
	public class When_the_enumerable_has_no_items : When_joining_an_enumberable_into_a_delimited_string
	{
		private Establish context = () => _items = new List< string >();
		private It should_return_an_empty_string = () => _result.ShouldEqual( string.Empty );
	}
}


// ReSharper restore InconsistentNaming