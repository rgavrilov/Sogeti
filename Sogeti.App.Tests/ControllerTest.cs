using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Sogeti.App.Tests {
	[TestFixture]
	public class ControllerTest {
		private MockRepository _mocks;
		private Mock<IViewFactory> _viewFactoryMock;
		private Mock<IView<ProcessResult>> _viewMock;
		private ProcessResult _renderedResult;

		private const string InputFilepath = "testFile.csv";

		[SetUp]
		public void SetUp() {
			_mocks = new MockRepository(MockBehavior.Strict);
			_viewFactoryMock = _mocks.Create<IViewFactory>();
			_viewFactoryMock.Setup(it => it.CreateView<ProcessResult>("text")).Returns(_viewMock.Object);
			_viewMock = _mocks.Create<IView<ProcessResult>>();
			_renderedResult = null;
			_viewMock.Setup(it => it.Render(It.IsNotNull<ProcessResult>()))
				.Callback<ProcessResult>(
					result => { _renderedResult = result; }
				);
		}

		[Test]
		public void Process_FiltersPresdinetsByState() {
			const int recordCount = 3;
			var readerMock = new PresidentsRecordReaderMock(InputFilepath, recordCount);

			var controller = new Controller(readerMock, _viewFactoryMock.Object);

			controller.Process(InputFilepath, "president-2-homeState", "text");

			Assert.That(_renderedResult.TotalCount, Is.EqualTo(recordCount));
			Assert.That(_renderedResult.Records.Count(), Is.EqualTo(1));
			Assert.That(_renderedResult.Records,
				Is.EquivalentTo(readerMock.GeneratedRecords.Skip(1).Take(1)));
		}

		[Test]
		public void Process_PrintsAllRecordsIfFilterIsNull() {
			const int recordCount = 3;
			var readerMock = new PresidentsRecordReaderMock(InputFilepath, recordCount);

			var controller = new Controller(readerMock, _viewFactoryMock.Object);

			controller.Process(InputFilepath, null, "text");

			Assert.That(_renderedResult.TotalCount, Is.EqualTo(recordCount));
			Assert.That(_renderedResult.Records, Is.EquivalentTo(readerMock.GeneratedRecords));
		}
	}
}