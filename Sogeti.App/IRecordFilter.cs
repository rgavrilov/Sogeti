using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App {
	public interface IRecordFilter {
		bool ShouldPass(Record record);
	}

	public class Counter : IRecordFilter {
		public int Count { get; private set; }

		public bool ShouldPass(Record record) {
			++Count;
			return true;
		}
	}
}