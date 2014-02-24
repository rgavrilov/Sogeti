using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;

namespace Sogeti.App.Tests {
	[TestFixture]
	public class CsvFileReaderTest {
		private void AssertRecordsAreEqual(IList<Record> actualRecords, params string[] expectedRecords) {
			Assert.That(actualRecords, Is.Not.Null, "actualRecords");
			Assert.That(actualRecords.Count, Is.EqualTo(expectedRecords.Length));

			IEnumerable<object> expectedRecordsAsRecords = expectedRecords.Select(ParseRecord);

			var pairs = actualRecords.Zip(expectedRecordsAsRecords, (actual, expected) => new {Actual = actual, Expected = expected});
			foreach (var pair in pairs) {
				Assert.That(pair.Actual, Is.EqualTo(pair.Expected));
			}
		}

		private object ParseRecord(string record) {
			var recordAsRecord = new Record(record.Split(','));
			return recordAsRecord;
		}

		private static IList<Record> LoadRecords(string fileContent) {
			const string filepath = "filepath";
			var mocks = new MockRepository(MockBehavior.Strict);
			Mock<IFileSystem> fileSystemMock = mocks.Create<IFileSystem>();
			fileSystemMock.Setup(it => it.OpenFileForRead(filepath))
				.Returns(new MemoryStream(Encoding.ASCII.GetBytes(fileContent)));
			IFileSystem fileSystem = fileSystemMock.Object;
			var reader = new CsvFileReader(fileSystem);
			reader.Open(filepath);
			IList<Record> records = new List<Record>();
			while (reader.MoveNext()) {
				records.Add(reader.CurrentRecord);
			}
			return records;
		}

		[Test]
		public void IgnoresEmptyLines() {
			const string fileContent = "A\n\nB\n  \n";
			IList<Record> records = LoadRecords(fileContent);
			AssertRecordsAreEqual(records, "A", "B");
		}

		[Test]
		public void SupportsBothUnixAndWindowsLineEndings() {
			const string fileContent = "A\r\nB\n";
			IList<Record> records = LoadRecords(fileContent);
			AssertRecordsAreEqual(records, "A", "B");
		}

		[Test]
		public void TrimsSpacesAroundFields() {
			const string fileContent = " A , B\r\n  C  \n";
			IList<Record> records = LoadRecords(fileContent);
			AssertRecordsAreEqual(records, "A,B", "C");
		
		}
	}
}