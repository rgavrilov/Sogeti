using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NConsoler;
using Ninject;

namespace Sogeti.App {
	public class Application {
		public void Run(string[] args, IConsole console, IFileSystem fileSystem) {
			var kernel = new StandardKernel();
			kernel.Bind<IConsole>().ToConstant(console);
			kernel.Bind<IFileSystem>().ToConstant(fileSystem);
			kernel.Bind<IRecordReader>().To<CsvFileReader>();

			var viewFactory = kernel.Get<ViewFactory>();

			viewFactory.RegisterView("text", typeof (ProcessResult), typeof (TextView));
			viewFactory.RegisterView("json", typeof (ProcessResult), typeof (JsonView));
			viewFactory.RegisterView("xml", typeof (ProcessResult), typeof (XmlView));

			kernel.Bind<IViewFactory>().ToConstant(viewFactory);

			var controller = kernel.Get<Controller>();
			try {
				Consolery.Run(controller, args);
			}
			catch (System.Exception e) {
				console.ErrorOut.WriteLine(e.Message);
			}
		}
	}
}