using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace Sogeti.App {
	public class TextView : IView<ProcessResult> {
		private readonly IConsole _console;

		public TextView(IConsole console) {
			_console = console;
		}

		public void Render(ProcessResult result) {
			RenderRecords(result);
			RenderCount(result);
		}

		private void RenderCount(ProcessResult result) {
			_console.StandardOut.WriteLine("Total count: {0}.", result.TotalCount);
			_console.StandardOut.Flush();
		}

		private void RenderRecords(ProcessResult result) {
			const char paddingSymbol = ' ';
			const int columnMargin = 1;
			int[] columnWidths = CalculateColumnWidths(result.Records);
			TextWriter standardOut = _console.StandardOut;
			foreach (Record record in result.Records) {
				for (int fieldIndex = 0; fieldIndex < record.Fields.Length; fieldIndex++) {
					bool isLastColumn = fieldIndex == record.Fields.Length - 1;
					var field = record.Fields[fieldIndex];
					standardOut.Write(field);
					int padding = columnWidths[fieldIndex] - field.Length;
					standardOut.Write(new string(paddingSymbol, padding + (isLastColumn ? 0 : columnMargin)));
				}
				standardOut.WriteLine();
			}
			standardOut.Flush();
		}

		private int[] CalculateColumnWidths(Record[] records) {
			var widths = new int[records.Max(record => record.Fields.Length)];
			foreach (var record in records) {
				for (int fieldIndex = 0; fieldIndex < record.Fields.Length; fieldIndex++) {
					widths[fieldIndex] = Math.Max(widths[fieldIndex], record.Fields[fieldIndex].Length);
				}
			}
			return widths;
		}
	}
}