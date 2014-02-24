using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App {
	public class ApplicationException : Exception {
		public ApplicationException(string message) : base(message) {}
	}
}