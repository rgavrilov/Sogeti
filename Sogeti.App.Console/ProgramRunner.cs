using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App.Console {
	public class ProgramRunner {
		private static void Main(string[] args) {
			var application = new Application();
			application.Run(args, new Console(), new FileSystem());
		}
	}
}