using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NUnit.Framework;

namespace Sogeti.App.Tests {
	public class PresidentsRecordReaderMock : IRecordReader {
		private static readonly string[] RecordTemplate = {
			"{0}", "firstName lastName", "http://wikipedia-link/president-{0}", "2003-12-31", "2004-12-31",
			"president-{0}-party", "president-{0}-portrait", "president-{0}-thumbnail", "president-{0}-homeState"
		};

		private readonly string _expectedFilepath;
		private readonly IList<Record> _generatedRecords;
		private readonly int _recordCount;
		private int _currentRecordIndex;

		public PresidentsRecordReaderMock(string expectedFilepath, int recordCount) {
			_recordCount = recordCount;
			_expectedFilepath = expectedFilepath;
			_generatedRecords = new List<Record>(Enumerable.Repeat<Record>(null, recordCount));
		}

		#region IRecordsReader

		public IList<Record> GeneratedRecords {
			get { return _generatedRecords; }
		}

		public void Open(string filepath) {
			Assert.That(_expectedFilepath, Is.EqualTo(filepath), "Filepath to open.");
			_currentRecordIndex = 0;
		}

		public Record CurrentRecord {
			get {
				Contract.Assert(_currentRecordIndex > 0, "Invalid state. Must call MoveNext before accessing current record.");
				var record = new Record(RecordTemplate.Select(field => string.Format(field, _currentRecordIndex)));
				GeneratedRecords[_currentRecordIndex - 1] = new Record(record.Fields);
				return record;
			}
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