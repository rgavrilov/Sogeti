using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Configuration;
using NUnit.Framework;

namespace Sogeti.App.Tests {
	public class PresidentsRecordReaderMock : IRecordReader {
		private static readonly string[] RecordTemplate = {
			"{0}", "firstName lastName", "http://wikipedia-link/president-{0}", "2003-12-31", "2004-12-31",
			"president-{0}-party", "president-{0}-portrait", "president-{0}-thumbnail", "president-{0}-homeState"
		};

		private static readonly Record HeaderRecord = new Record(new[]{
			"Presidency", "President", "Wikipedia Entry", "Took office",
			"Left office", "Party", "Portrait", "Thumbnail", "Home State"
		});

		private readonly string _expectedFilepath;
		private IList<Record> _accessedRecords;
		private int _currentRecordIndex;
		private readonly List<Record> _records;

		public PresidentsRecordReaderMock(string expectedFilepath, int recordCount, bool includeHeaders = false) {
			_expectedFilepath = expectedFilepath;
	
			_records = new List<Record>();
			if (includeHeaders) {
				_records.Add(HeaderRecord);
			}
			for (int presidency = 1; presidency <= recordCount; presidency++) {
				int presidencyLocalVariable = presidency;
				var record = new Record(RecordTemplate.Select(field => string.Format(field, presidencyLocalVariable)));
				_records.Add(record);
			}
			_accessedRecords = null;
		}

		#region IRecordsReader

		public IList<Record> AccessedRecords {
			get { return _accessedRecords; }
		}

		public void Open(string filepath) {
			Assert.That(_expectedFilepath, Is.EqualTo(filepath), "Filepath to open.");
			_currentRecordIndex = -1;
			_accessedRecords = new List<Record>(Enumerable.Repeat<Record>(null, _records.Count));
		}

		public Record CurrentRecord {
			get {
				Contract.Assert(_currentRecordIndex >= 0, "Invalid state. Must call MoveNext before accessing current record.");
				var currentRecord = _records[_currentRecordIndex];
				_accessedRecords[_currentRecordIndex] = new Record(currentRecord.Fields);
				return currentRecord;
			}
		}

		public bool MoveNext() {
			if (_currentRecordIndex < _records.Count) {
				++_currentRecordIndex;
				return _currentRecordIndex < _records.Count;
			}
			return false;
		}

		#endregion
	}
}