using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App {
	public interface IRecordReader {
		void Open(string filepath);
		Record CurrentRecord { get; }
		bool MoveNext();
	}
}