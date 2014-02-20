using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NConsoler;
using Ninject;

namespace Sogeti.App {
    public class Application {
        public void Run(string[] args) {
            var kernel = new StandardKernel();
            kernel.Bind<ICsvFileReader>().To<CsvFileReader>();
            Consolery.Run(kernel.Get<Controller>(), args);
        }
    }

    public class Controller {
        private readonly ICsvFileReader _fileReader;

        public Controller(ICsvFileReader fileReader) {
            this._fileReader = fileReader;
        }

        [Action(Description = "load and process CSV file with presidents")]
        public void Load(
            [NConsoler.Optional("sample.csv", Description = "file to read presidents from")] string inputFilepath) {
            _fileReader.Open();
            string[] line;
            while ((line = _fileReader.GetNextLine()) != null) {
                Console.WriteLine(line.First());
            }
        }
    }

    public interface ICsvFileReader {
        void Open();
        string[] GetNextLine();
        void Close();
    }


    public class CsvFileReader : ICsvFileReader {
        private int _remainingLineCount;

        public void Open() {
            _remainingLineCount = 5;
        }

        public string[] GetNextLine() {
            if (_remainingLineCount-- > 0) {
                return new[] {"A", "B", "C"};
            }
            return null;
        }

        public void Close() {}
    }
}