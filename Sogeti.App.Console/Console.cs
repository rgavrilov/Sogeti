using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace Sogeti.App.Console {
	public sealed class Console : IConsole {
		public TextWriter StandardOut {
			get { return System.Console.Out; }
		}

		public TextWriter ErrorOut {
			get { return System.Console.Error; }
		}
	}
}