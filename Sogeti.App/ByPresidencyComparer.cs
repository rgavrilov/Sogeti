using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App {
	public class ByPresidencyComparer : IComparer<Record> {
		public int Compare(Record x, Record y) {
			return new PresidentRecordReader(x).Presidency - new PresidentRecordReader(y).Presidency;
		}
	}
}