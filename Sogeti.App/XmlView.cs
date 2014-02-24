using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using Sogeti.App.Xml;

namespace Sogeti.App {
	public class XmlView : IView<ProcessResult> {
		private readonly IConsole _console;

		public XmlView(IConsole console) {
			_console = console;
		}

		public void Render(ProcessResult result) {
			ProcessResultXml xmlResult = MapResult(result);

			var serializer = new DataContractSerializer(typeof (ProcessResultXml));
			XmlWriter xmlWriter = XmlWriter.Create(_console.StandardOut, new XmlWriterSettings {Indent = true});
			serializer.WriteObject(xmlWriter, xmlResult);
			xmlWriter.Flush();
		}

		private static ProcessResultXml MapResult(ProcessResult result) {
			var xmlResult = new ProcessResultXml {
				Records =
					result.Records.Select(record => new PresidentRecordReader(record)).Select(record => new PresidentRecordXml {
						Presidency = record.Presidency,
						Name = record.Name.ToString(),
						TookOffice = record.TookOffice,
						LeftOffice = record.LeftOffice,
						State = record.HomeState.ToString()
					}).ToArray(),
				TotalCount = result.TotalCount
			};
			return xmlResult;
		}
	}
}