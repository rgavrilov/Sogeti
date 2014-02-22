using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
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
			return new PresidentRecordAdapter(record).HomeState == _state;
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
			return new PresidentRecordAdapter(x).Presidency - new PresidentRecordAdapter(y).Presidency;
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

	public class PresidentRecordAdapter {
		public PresidentRecordAdapter(Record record) {
			if (record == null) {
				throw new ArgumentNullException("record");
			}
			if (record.Fields.Length != 9) {
				throw new ArgumentException(
					string.Format("President record must contain exactly 9 fields, but given record contains {0}.",
						record.Fields.Length), "record");
			}

			Presidency = ValidateAndGetNumber(record.Fields[0], "presidency");

			Name = ValidateAndGetString(record.Fields[1], "name");

			Uri wikiUrl;
			if (!Uri.TryCreate(record.Fields[2], UriKind.RelativeOrAbsolute, out wikiUrl)) {
				throw new FormatException("Wiki link is not a valid URL.");
			}
			WikipediaEntryUrl = wikiUrl;

			TookOffice = ValidateAndGetDateTime(record.Fields[3], "took office date");

			LeftOffice = ValidateAndGetDateTime(record.Fields[4], "left office date");

			Party = ValidateAndGetString(record.Fields[5], "party");

			PortraitImageFilename = ValidateAndGetString(record.Fields[6], "portrait image");

			ThumbnailImageFilename = ValidateAndGetString(record.Fields[7], "thumbnail image");

			HomeState = ValidateAndGetString(record.Fields[8], "home state");
		}

		private int ValidateAndGetNumber(string value, string fieldName) {
			int result;
			if (string.IsNullOrWhiteSpace(value)) {
				throw new FormatException(string.Format("Value {0} is missing.", fieldName));
			}
			if (!int.TryParse(value, out result)) {
				throw new FormatException(string.Format("Value {0} is not a valid number.", fieldName));
			}
			return result;
		}

		private DateTime ValidateAndGetDateTime(string value, string fieldName) {
			DateTime result;
			if (string.IsNullOrWhiteSpace(value)) {
				throw new FormatException(string.Format("Value {0} is missing.", fieldName));
			}
			if (!DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result)) {
				throw new FormatException(string.Format("Value {0} is invalid.", fieldName));
			}
			return result;
		}

		private string ValidateAndGetString(string field, string fieldName) {
			if (string.IsNullOrEmpty(field)) {
				throw new FormatException(string.Format("Value {0} is not specified.", fieldName));
			}
			return field;
		}

		public int Presidency { get; private set; }

		public string Name { get; private set; }

		public Uri WikipediaEntryUrl { get; private set; }

		public DateTime TookOffice { get; private set; }

		public DateTime LeftOffice { get; private set; }

		public string Party { get; private set; }

		public string PortraitImageFilename { get; private set; }

		public string ThumbnailImageFilename { get; private set; }

		public string HomeState { get; private set; }
	}
}