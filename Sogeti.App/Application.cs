using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using NConsoler;
using Ninject;

namespace Sogeti.App {
	public class Application {
		public void Run(string[] args) {
			var kernel = new StandardKernel();
			kernel.Bind<IRecordsReader>().To<CsvFileReader>();
			kernel.Bind<IRecordPrinter>().To<RecordPrinter>();
			Consolery.Run(kernel.Get<Controller>(), args);
		}
	}

	public class PresidentStateFilter : IRecordFilter {
		private readonly string _state;

		public PresidentStateFilter(string state) {
			_state = state;
		}

		public bool ShouldPass(Record record) {
			return new PresidentRecord(record.Fields).HomeState == _state;
		}
	}

	public class Controller {
		private readonly Counter _counter;
		private readonly IRecordsReader _fileReader;
		private readonly IRecordFilter[] _filters;
		private readonly IRecordPrinter _recordPrinter;
		private readonly IComparer<Record> _sort;

		public Controller(IRecordsReader fileReader, IRecordPrinter recordPrinter) {
			_fileReader = fileReader;
			_recordPrinter = recordPrinter;
			_counter = new Counter();
			_filters = new IRecordFilter[] {_counter, new PresidentStateFilter("New York")};
			_sort = new ByPresidencyComparer();
		}

		[Action(Description = "load and process CSV file with presidents")]
		public void Process(
			[NConsoler.Optional("sample.csv", "i", Description = "file to read presidents from")] string inputFilepath) {
			_fileReader.Open(inputFilepath);
			var records = new List<Record>();
			while (_fileReader.MoveNext()) {
				Record record = _fileReader.CurrentRecord();
				records.Add(record);
			}

			List<Record> filteredRecords = records.Where(record => _filters.All(filter => filter.ShouldPass(record))).ToList();

			filteredRecords.Sort(_sort);

			foreach (Record record in filteredRecords) {
				_recordPrinter.Print(record);
			}

			Console.WriteLine("Total count: {0}.", _counter.Count);
		}
	}

	public class ByPresidencyComparer : IComparer<Record> {
		public int Compare(Record x, Record y) {
			return new PresidentRecord(x.Fields).Presidency - new PresidentRecord(y.Fields).Presidency;
		}
	}


	public interface IRecordPrinter {
		void Print(Record record);
	}

	internal class RecordPrinter : IRecordPrinter {
		public void Print(Record record) {
			Console.WriteLine(string.Join(", ", record.Fields));
		}
	}

	public class Record {
		public Record(IEnumerable<string> fields) {
			Fields = fields.ToArray();
		}

		public string this[int index] {
			get { return Fields[index]; }
		}

		public string[] Fields { get; private set; }
	}

	public interface IRecordsReader {
		void Open(string filepath);
		Record CurrentRecord();
		bool MoveNext();
	}

	public class CsvFileReader : IRecordsReader {
		private Record _current;
		private Queue<Record> _records;

		public void Open(string filepath) {
			string[] lines = File.ReadAllLines(filepath);
			_records = new Queue<Record>(lines.Select(ParseRecord));
			_current = null;
		}

		public bool MoveNext() {
			if (_records.Any()) {
				_current = _records.Dequeue();
				return true;
			}
			return false;
		}

		public Record CurrentRecord() {
			return _current;
		}

		private static Record ParseRecord(string line) {
			return new Record(line.Split(',').Select(field => field.Trim(' ')));
		}
	}

	public class RecordFilter : IRecordFilter {
		private readonly Func<Record, bool> _predicate;

		public RecordFilter(Func<Record, bool> predicate) {
			_predicate = predicate;
		}

		public bool ShouldPass(Record record) {
			return _predicate(record);
		}
	}

	public class PresidentRecord : Record {
		public PresidentRecord(IEnumerable<string> fields) : base(new string[9]) {
			if (fields == null) {
				throw new ArgumentNullException("fields");
			}
			List<string> fieldsAsList = fields.ToList();
			if (fieldsAsList.Count != 9) {
				throw new ArgumentException(
					string.Format("President record must contain exactly 9 fields, but {0} given.", fieldsAsList.Count), "fields");
			}
			int fieldIndex = 0;
			foreach (string field in fieldsAsList) {
				base.Fields[fieldIndex] = field;
				++fieldIndex;
			}
		}

		public int Presidency {
			get { return int.Parse(Fields[0]); }
		}

		public string Name {
			get { return Fields[1]; }
		}

		public Uri WikipediaEntryUrl {
			get { return new Uri(Fields[2], UriKind.RelativeOrAbsolute); }
		}

		public DateTime TookOffice {
			get { return DateTime.Parse(Fields[3]); }
		}

		public DateTime LeftOffice {
			get { return DateTime.Parse(Fields[4]); }
		}

		public string Party {
			get { return Fields[5]; }
		}

		public string PortraitImageFilename {
			get { return Fields[6]; }
		}

		public string ThumbnailImageFilename {
			get { return Fields[7]; }
		}

		public string HomeState {
			get { return Fields[8]; }
		}
	}
}