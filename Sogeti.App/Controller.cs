using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NConsoler;

namespace Sogeti.App {
	public sealed class ProcessResult {
		public Record[] Records { get; set; }
		public int TotalCount { get; set; }
	}

	public class Controller {
		private readonly IRecordReader _fileReader;
		private readonly IViewFactory _viewFactory;

		public Controller(IRecordReader fileReader, IViewFactory viewFactory) {
			_fileReader = fileReader;
			_viewFactory = viewFactory;
		}

		[Action(Description = "load and process CSV file with presidents")]
		public void Process(
			[Optional("sample.csv", "i", Description = "file to read presidents from")] string inputFilepath,
			[Optional("New York", "s", "state", Description = "state to filter presidents to")] string filterState,
			[Optional("text", "f", Description = "format to output result in")] string format) {
			IEnumerable<Record> records = LoadRecords(inputFilepath);

			int totalCount = records.Count();

			if (!string.IsNullOrWhiteSpace(filterState)) {
				var stateFilter = new PresidentStateFilter(filterState);
				records = records.Where(stateFilter.ShouldPass);
			}

			List<Record> filteredRecords = records.ToList();
			filteredRecords.Sort(new ByPresidencyComparer());

			var result = new ProcessResult {
				Records = filteredRecords.ToArray(),
				TotalCount = totalCount
			};

			IView<ProcessResult> view = _viewFactory.CreateView<ProcessResult>(format);
			view.Render(result);
		}

		private List<Record> LoadRecords(string inputFilepath) {
			_fileReader.Open(inputFilepath);
			var records = new List<Record>();
			while (_fileReader.MoveNext()) {
				Record record = _fileReader.CurrentRecord;
				records.Add(record);
			}
			return records;
		}
	}
}