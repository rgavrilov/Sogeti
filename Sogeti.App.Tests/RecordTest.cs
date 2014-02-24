using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NUnit.Framework;

namespace Sogeti.App.Tests {
	[TestFixture]
	public class RecordTest {
		private IEnumerable<object> EqualTestCases() {
			yield return new object[] {new string[0], new string[0]};
			yield return new object[] {new[] {"A"}, new[] {"A"}};
			yield return new object[] {new[] {"A", "B"}, new[] {"A", "B"}};
		}

		private IEnumerable<object> NotEqualTestCases() {
			yield return new object[] { new[] { "" }, new[] { "A" } };
			yield return new object[] { new[] { "A" }, new[] { "B" } };
			yield return new object[] { new[] { "A", "B" }, new[] { "B" } };
			yield return new object[] { new[] { "A", "B" }, new[] { "A", "B", "C" } };
			yield return new object[] { new[] { "B", "C" }, new[] { "A", "B", "C" } };
			yield return new object[] { new[] { "A", "B" }, new[] { "B", "A" } };
		}

		private IEnumerable<TestCaseData> ToStringTestCases() {
			yield return new TestCaseData(new Record(""), "");
			yield return new TestCaseData(new Record("A"), "A");
			yield return new TestCaseData(new Record("A", "B", "C"), "A, B, C");
		}

		[Test]
		[TestCaseSource("EqualTestCases")]
		public void ImplementsEquals_EqualCases(string[] xFields, string[] yFields) {
			Assert.That(new Record(xFields), Is.EqualTo(new Record(yFields)));
		}

		[Test]
		[TestCaseSource("NotEqualTestCases")]
		public void ImplementsEquals_NotEqualCases(string[] xFields, string[] yFields) {
			Assert.That(new Record(xFields), Is.Not.EqualTo(new Record(yFields)));
		}

		[Test]
		[TestCaseSource("ToStringTestCases")]
		public void ImplementsToString(Record record, string expectedOutput) {
			Assert.That(record.ToString(), Is.EqualTo(expectedOutput));
		}
	}
}