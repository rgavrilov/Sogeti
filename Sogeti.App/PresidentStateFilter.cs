using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App {
	public class PresidentStateFilter : IRecordFilter {
		private readonly Name _state;

		public PresidentStateFilter(string state) {
			_state = new Name(state);
		}

		public bool ShouldPass(Record record) {
			return new PresidentRecordReader(record).HomeState.Equals(_state);
		}
	}
}