using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App {
	public class TextView : IView<ProcessResult> {
		private readonly IConsole _console;

		public TextView(IConsole console) {
			_console = console;
		}

		public void Render(ProcessResult result) {
			foreach (Record record in result.Records) {
				_console.StandardOut.WriteLine(string.Join("\t", record.Fields));
			}
			_console.StandardOut.WriteLine("Total count: {0}.", result.TotalCount);
		}
	}
}