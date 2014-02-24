using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App {
	public class Exception : System.Exception {
		public Exception(string message) : base(message) {}
	}
}