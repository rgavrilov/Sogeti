using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Sogeti.App.Tests {
	[TestFixture]
	public class ControllerTest {
		[Test]
		public void PrintsRecords_NoFilters() {
			const string inputFilepath = "testFile.csv";
			const int recordCount = 3;
			var recordsReaderMock = new RecordsReaderMock(inputFilepath, recordCount, 3);
			var printerMock = new RecordPrinterMock();
			var controller = new Controller(recordsReaderMock, printerMock);
			controller.Process(inputFilepath);
			printerMock.AssertPrintedRecordCount(recordCount);
		}

		[Test]
		public void PrintsRecords_WithFilters() {
			const string inputFilepath = "testFile.csv";
			const int recordCount = 3;
			var readerMock = new RecordsReaderMock(inputFilepath, recordCount, 3);
			var printerMock = new RecordPrinterMock();
			
			var mocks = new MockRepository(MockBehavior.Strict);

			Mock<IRecordFilter> filterMock = mocks.Create<IRecordFilter>();
			filterMock.SetupSequence(it => it.ShouldPass(It.IsNotNull<Record>())).Returns(true).Returns(false).Returns(true);

			var controller = new Controller(readerMock, printerMock);
			controller.Process(inputFilepath);
			printerMock.AssertPrintedRecordCount(recordCount - 1);
		}
	}

	public class RecordPrinterMock : IRecordPrinter {
		private int _printRecordCount;

		public void Print(Record record) {
			++_printRecordCount;
		}

		public void AssertPrintedRecordCount(int expectedCount) {
			Assert.That(_printRecordCount, Is.EqualTo(expectedCount), "Printed record count.");
		}
	}

	public class RecordsReaderMock : IRecordsReader {
		private readonly string _expectedFilepath;
		private readonly int _fieldCount;
		private readonly int _recordCount;
		private int _currentRecordIndex;

		public RecordsReaderMock(string expectedFilepath, int recordCount, int fieldCount) {
			_recordCount = recordCount;
			_fieldCount = fieldCount;
			_expectedFilepath = expectedFilepath;
		}

		#region IRecordsReader

		public void Open(string filepath) {
			Assert.That(_expectedFilepath, Is.EqualTo(filepath), "Filepath to open.");
			_currentRecordIndex = 0;
		}

		public Record CurrentRecord() {
			return
				new Record(
					Enumerable.Range(1, _fieldCount)
						.Select(fieldIndex => string.Format("R{0}F{1}", _currentRecordIndex, fieldIndex)));
		}

		public bool MoveNext() {
			if (_currentRecordIndex < _recordCount) {
				++_currentRecordIndex;
				return true;
			}
			return false;
		}

		#endregion
	}
}