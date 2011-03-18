using NUnit.Framework;
using Shiloh.Testing.Assertions;

// ReSharper disable InconsistentNaming


namespace Shiloh.Testing.Tests
{
	public class TestClass1
	{
		public int Value1 { get; set; }
		internal int Value2 { get; set; }
	}


	public class TestClass2
	{
		public int Value1 { get; set; }
	}


	public enum TestValues
	{
		TestValue1,
		TestValue2
	}


	public class TestClass3
	{
		public TestValues Value1 { get; set; }
	}


	[ TestFixture ]
	public class GlobalEqualityAssertionTests
	{
		[ Test ]
		public void comparing_two_identical_instances_of_the_same_class_should_pass()
		{
			var obj1 = new TestClass1 {Value1 = 1};
			var obj2 = new TestClass1 {Value1 = 1};

			obj1.should_be_equal_to( obj2 );
		}


		[ Test ]
		public void comparing_two_instances_of_different_classes_that_have_the_same_public_property_where_both_instances_have_the_same_value_for_that_property()
		{
			var obj1 = new TestClass1 {Value1 = 1};
			var obj2 = new TestClass2 {Value1 = 1};

			obj1.should_be_equal_to( obj2 );
		}


		[ Test ]
		public void comparing_two_instances_of_different_classes_that_have_the_same_public_property_where_the_values_for_that_property_differ()
		{
			var obj1 = new TestClass1 {Value1 = 1};
			var obj2 = new TestClass2 {Value1 = 2};

			Assert.Throws< AssertionException >( () => obj1.should_be_equal_to( obj2 ) );
		}


		[ Test ]
		public void comparing_two_instances_of_the_same_class_where_an_internal_property_differs_should_pass()
		{
			var obj1 = new TestClass1 {Value1 = 1, Value2 = 1};
			var obj2 = new TestClass1 {Value1 = 1, Value2 = 3};

			obj1.should_be_equal_to( obj2 );
		}


		[ Test ]
		public void comparing_two_instances_of_the_same_class_with_different_values_should_fail()
		{
			var obj1 = new TestClass1 {Value1 = 1};
			var obj2 = new TestClass1 {Value1 = 2};
			Assert.Throws< AssertionException >( () => obj1.should_be_equal_to( obj2 ) );
		}


		[ Test ]
		public void comparing_two_instances_with_the_same_enum_value_should_pass()
		{
			var obj1 = new TestClass3 {Value1 = TestValues.TestValue1};
			var obj2 = new TestClass3 {Value1 = TestValues.TestValue1};

			obj1.should_be_equal_to( obj2 );
		}


		[ Test ]
		public void comparing_two_instances_with_different_enum_values_should_fail()
		{
			var obj1 = new TestClass3 {Value1 = TestValues.TestValue1};
			var obj2 = new TestClass3 {Value1 = TestValues.TestValue2};
			Assert.Throws< AssertionException >( () => obj1.should_be_equal_to( obj2 ) );
		}
	}
}


// ReSharper restore InconsistentNaming