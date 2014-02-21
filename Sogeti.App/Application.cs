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
            kernel.Bind<IRecordsReader>().To<CsvFileReader>();
            Consolery.Run(kernel.Get<Controller>(), args);
        }
    }
    
    public class Controller {
        private readonly IRecordsReader _fileReader;

        public Controller(IRecordsReader fileReader) {
            _fileReader = fileReader;
        }

        [Action(Description = "load and process CSV file with presidents")]
        public void Process([NConsoler.Optional("sample.csv", Description = "file to read presidents from")] string inputFilepath) {
            _fileReader.Open(inputFilepath);
            while (_fileReader.MoveNext()) {
                var line = _fileReader.CurrentRecord();
                Console.WriteLine(line.First());
            }
        }
    }

    public interface IRecordsReader {
        void Open(string filepath);
        string[] CurrentRecord();
        bool MoveNext();
    }
    
    public class CsvFileReader : IRecordsReader {
        private int _remainingLineCount;

        public void Open(string filepath) {
            _remainingLineCount = 5;
        }

        public bool MoveNext() {
            return _remainingLineCount-- > 0;
        }

        public string[] CurrentRecord() {
            return new[] {"A", "B", "C"};
        }
    }
}