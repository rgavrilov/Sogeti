using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;

namespace Sogeti.App.Xml {
	[DataContract(Name = "President", Namespace = "http://schemas.sogeti.com/rgavrilov/2014-02-23")]
	public class PresidentRecordXml {
		[DataMember]
		public int Presidency { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public DateTime TookOffice { get; set; }

		[DataMember]
		public DateTime LeftOffice { get; set; }

		[DataMember]
		public string State { get; set; }
	}
}