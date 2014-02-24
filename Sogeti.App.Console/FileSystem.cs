using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace Sogeti.App.Console {
	public sealed class FileSystem : IFileSystem {
		public Stream OpenFileForRead(string filepath) {
			return File.OpenRead(filepath);
		}
	}
}