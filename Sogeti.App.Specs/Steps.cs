using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Sogeti.App.Specs {
	public class FileSystem : IFileSystem {
		private readonly IDictionary<string, string> _map = new Dictionary<string, string>();

		public Stream OpenFileForRead(string filepath) {
			if (!_map.ContainsKey(filepath)) {
				const string message = "File {0} not found in the file system.";
				throw new Exception(string.Format(message, filepath));
			}
			return new MemoryStream(Encoding.ASCII.GetBytes(_map[filepath]));
		}

		public void Setup(string filename, string content) {
			_map[filename] = content;
		}
	}

	public class Console : IConsole {
		public StringBuilder CommonOutput = new StringBuilder();

		public TextWriter StandardOut {
			get { return new StringWriter(CommonOutput); }
		}

		public TextWriter ErrorOut {
			get { return new StringWriter(CommonOutput); }
		}
	}

	[Binding]
	public class Steps {
		private Console _console;
		private FileSystem _fileSystem;

		[BeforeScenario]
		public void BeforeScenario() {
			_console = new Console();
			_fileSystem = new FileSystem();
		}

		[Given(@"following file (.*):")]
		public void GivenFollowingFile(string filename, string content) {
			_fileSystem.Setup(filename, content);
		}

		[When(@"I run application with (.*)")]
		public void WhenIRunApplicationWith(string args) {
			string[] argsAsArray = args.Split(' ');
			new Application().Run(argsAsArray, _console, _fileSystem);
		}

		[Then(@"the result should match (.*)")]
		public void ThenTheResultShouldMatch(string expectedOutput) {
			string expectedOutputContent = ReferenceOutputs.ResourceManager.GetString(expectedOutput, ReferenceOutputs.Culture);

			Assert.IsNotNull(expectedOutputContent, "BUG IN TEST: expected output resource {0} not found.", expectedOutput);

			string actualOutput = _console.CommonOutput.ToString();

			actualOutput = NormalizeOutput(actualOutput);
			expectedOutputContent = NormalizeOutput(expectedOutputContent);

			if (!string.Equals(expectedOutputContent, actualOutput)) {
				System.Console.WriteLine("Actual and expected outputs do not match.\n\nActual:\n{0}\n\nExpected:\n{1}", actualOutput, expectedOutputContent);
				Assert.AreEqual(expectedOutputContent, actualOutput);
			}
		}

		private string NormalizeOutput(string output) {
			// fix EOLs
			output = output.Replace("\r", "").Replace("\n", "\r\n");
			// add EOL at the end, if missing
			if (!output.EndsWith("\r\n")) output += "\r\n";
			// trim trailing whitespaces on each line
			var trailingSpacesPattern = new Regex(@"\s*(?=\r\n)", RegexOptions.Singleline);
			output = trailingSpacesPattern.Replace(output, "");
			return output;
		}
	}
}