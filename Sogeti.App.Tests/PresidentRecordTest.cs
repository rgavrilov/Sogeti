using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace Sogeti.App.Tests {
	[TestFixture]
	public class PresidentRecordReaderTest {
		[Test]
		public void WrapsGivenRecord() {
			// Presidency 	President 	Wikipedia Entry	Took office 	Left office 	Party 	Portrait	Thumbnail	Home State
			var record =
				new Record(new[] {
					1.ToString(CultureInfo.InvariantCulture), "firstName lastName", "http://wikipedia-link", "2003-12-31", "2004-12-31",
					"president-1-party", "president-1-portrait", "president-1-thumbnail", "president-1-homeState"
				});
			var adapter = new PresidentRecordReader(record);
			Assert.That(adapter.Presidency, Is.EqualTo(1), "Presidency");
			Assert.That(adapter.Name, Is.EqualTo(PersonFullName.Parse("firstName lastName")), "Name");
			Assert.That(adapter.WikipediaEntryUrl, Is.EqualTo(new Uri("http://wikipedia-link", UriKind.RelativeOrAbsolute)));
			Assert.That(adapter.TookOffice, Is.EqualTo(new DateTime(2003, 12, 31)));
			Assert.That(adapter.LeftOffice, Is.EqualTo(new DateTime(2004, 12, 31)));
			Assert.That(adapter.Party, Is.EqualTo("president-1-party"));
			Assert.That(adapter.PortraitImageFilename, Is.EqualTo("president-1-portrait"));
			Assert.That(adapter.ThumbnailImageFilename, Is.EqualTo("president-1-thumbnail"));
			Assert.That(adapter.HomeState, Is.EqualTo(new Name("president-1-homeState")));
		}

		[Test]
		[TestCaseSource("InvalidRecords")]
		[ExpectedException(typeof(FormatException))]
		public void ThrowsExceptionWhenInvalidRecordIsGiven(Record record) {
			new PresidentRecordReader(record);
		}

		private static IEnumerable<Record> InvalidRecords() {
			return TestCases.InvalidPresidentRecords.Split('\n').Select(line => new Record(line.Split(',')));
		}
	}
}