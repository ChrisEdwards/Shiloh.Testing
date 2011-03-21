using Machine.Specifications;
using Shiloh.Common.Extensions;

// ReSharper disable InconsistentNaming


namespace Shiloh.Common.Specs.Extensions
{
	public class ObjectExtensionSpecs
	{
		[ Subject( typeof ( ObjectExtensions ) ) ]
		public class When_converting_an_object_to_a_printable_string
		{
			static TestClass _testObject;
			static string result;
			Establish context = () => _testObject = new TestClass { PublicValue = "public", InternalValue = "internal" };
			Because of = () => result = _testObject.ToPrintableString();
			It should_generate_string_with_the_values_of_all_public_properties = () => result.ShouldEqual( "{{ TestClass { PublicValue = [ public ]; } }}" );
		}
	}


	public class TestClass
	{
		public string PublicValue { get; set; }
		internal string InternalValue { get; set; }
	}
}


// ReSharper restore InconsistentNaming