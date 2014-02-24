using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NUnit.Framework;

namespace Sogeti.App.Tests {
	[TestFixture]
	public class PersonFullNameTest {
		private IEnumerable<TestCaseData> ParseTestCases() {
			yield return new TestCaseData("F L", new PersonFullName("L", "F"));
			yield return new TestCaseData("F M1 L", new PersonFullName("L", "F", "M1"));
			yield return new TestCaseData("F M1 M2 L", new PersonFullName("L", "F", "M1", "M2"));
		}

		[Test]
		public void ImplementsEquals() {
			Assert.That(PersonFullName.Parse("A B"), Is.EqualTo(PersonFullName.Parse("a b")));
			Assert.That(PersonFullName.Parse("A B C"), Is.EqualTo(PersonFullName.Parse("a b c")));
			Assert.That(PersonFullName.Parse("A B C D"), Is.EqualTo(PersonFullName.Parse("a b c d")));
		}

		[Test]
		[TestCaseSource("ParseTestCases")]
		public void Parse(string text, PersonFullName expected) {
			Assert.That(PersonFullName.Parse(text), Is.EqualTo(expected));
		}

		[Test]
		public void ProvidesParseMethod_IgnoresConsecutiveSpaces() {
			PersonFullName personFullName = PersonFullName.Parse(" John  Doe ");
			Assert.That(personFullName, Is.EqualTo(new PersonFullName("Doe", "John")));
		}

		[Test]
		[TestCaseSource("ParseTestCases")]
		public void TryParse_ParsesFullName(string text, PersonFullName expected) {
			PersonFullName result;
			Assert.That(PersonFullName.TryParse(text, out result), Is.True);
			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TryParse_ReturnsFalseIfNameIsInvalid() {
			PersonFullName result;
			Assert.That(PersonFullName.TryParse(null, out result), Is.False);
			Assert.That(PersonFullName.TryParse("", out result), Is.False);
		}
	}
}