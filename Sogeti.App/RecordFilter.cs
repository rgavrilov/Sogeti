using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App {
	public class RecordFilter : IRecordFilter {
		private readonly Func<Record, bool> _predicate;

		public RecordFilter(Func<Record, bool> predicate) {
			_predicate = predicate;
		}

		public bool ShouldPass(Record record) {
			return _predicate(record);
		}
	}
}