using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Sogeti.App.Tests {
    [TestFixture]
    public class ControllerTest {
        [Test]
        public void ProcessesFile() {
            var mocks = new MockRepository(MockBehavior.Strict);
            Mock<IRecordsReader> readerMock = mocks.Create<IRecordsReader>();

            const string inputFilepath = "testFile.csv";
            readerMock.Setup(it => it.Open(inputFilepath));
            readerMock.Setup(it => it.MoveNext()).Returns(true);
            readerMock.Setup(it => it.CurrentRecord()).Returns(new[] { "F11", "F12" });
            readerMock.Setup(it => it.MoveNext()).Returns(true);
            readerMock.Setup(it => it.CurrentRecord()).Returns(new[] { "F21", "F22" });
            readerMock.Setup(it => it.MoveNext()).Returns(false);

            var controller = new Controller(readerMock.Object);

            controller.Process(inputFilepath);

            readerMock.VerifyAll();
        }
    }
}