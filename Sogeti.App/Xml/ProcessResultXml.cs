using System.Runtime.Serialization;

namespace Sogeti.App.Xml {
	[DataContract(Name = "Result", Namespace = "http://schemas.sogeti.com/rgavrilov/2014-02-23")]
	public class ProcessResultXml {
		[DataMember]
		public PresidentRecordXml[] Records { get; set; }

		[DataMember]
		public int TotalCount { get; set; }
	}
}