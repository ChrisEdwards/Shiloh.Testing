using System;


namespace Shiloh.Testing.Assertions
{
	[ AttributeUsage( AttributeTargets.Property ) ]
	public class IgnorePropertyWhenAssertingEqualityAttribute : Attribute {}
}