using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace Sogeti.App {
	public class CsvFileReader : IRecordReader {
		private readonly IFileSystem _fileSystem;
		private Record _current;
		private Queue<Record> _records;

		public CsvFileReader(IFileSystem fileSystem) {
			_fileSystem = fileSystem;
		}

		public void Open(string filepath) {
			string[] lines =
				new StreamReader(_fileSystem.OpenFileForRead(filepath)).ReadToEnd().Split(new string[] {"\r\n", "\n"}, StringSplitOptions.None);
			_records = new Queue<Record>(lines.Where(line => !string.IsNullOrWhiteSpace(line)).Select(ParseRecord));
			_current = null;
		}

		public bool MoveNext() {
			if (_records.Any()) {
				_current = _records.Dequeue();
				return true;
			}
			return false;
		}

		public Record CurrentRecord {
			get { return _current; }
		}

		private static Record ParseRecord(string line) {
			return new Record(line.Split(',').Select(field => field.Trim(' ')));
		}
	}
}