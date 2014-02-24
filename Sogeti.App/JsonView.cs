using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Newtonsoft.Json;
using Sogeti.App.Json;

namespace Sogeti.App {
	public class JsonView : IView<ProcessResult> {
		private readonly IConsole _console;

		public JsonView(IConsole console) {
			_console = console;
		}

		public void Render(ProcessResult result) {
			JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings {
				Formatting = Formatting.Indented
			});
			var jsonResult = new ResultJson {
				Presidents =
					result.Records.Select(record => new PresidentRecordReader(record)).Select(record => new PresidentRecordJson {
						Presidency = record.Presidency,
						Name = record.Name.ToString(),
						TookOffice = record.TookOffice,
						LeftOffice = record.LeftOffice,
						State = record.HomeState.ToString()
					}).ToArray(),
				TotalCount = result.TotalCount
			};
			serializer.Serialize(_console.StandardOut, jsonResult);
		}
	}
}