using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App.Console {
    internal class ProgramRunner {
        private static void Main(string[] args) {
            var application = new Sogeti.App.Application();
            application.Run(args);
        }
    }
}