using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NUnit.Framework;

namespace Sogeti.App.Tests {
	[TestFixture]
	public class RecordFilterTest {
		[Test]
		public void UsesPredicateToFilterRecords() {
			var filter = new RecordFilter(record => record[0] == "A");
			Assert.That(filter.ShouldPass(new Record(new string[] { "A", "B", "C" })), Is.True);
			Assert.That(filter.ShouldPass(new Record(new string[] { "X", "Y", "Z" })), Is.False);
		}
	}
}