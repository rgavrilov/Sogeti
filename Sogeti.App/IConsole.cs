using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace Sogeti.App {
	public interface IConsole {
		TextWriter StandardOut { get; }
		TextWriter ErrorOut { get; }
	}
}