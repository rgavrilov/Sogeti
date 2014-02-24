using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;

namespace Sogeti.App {
	public class PresidentRecordReader {
		public PresidentRecordReader(Record record) {
			if (record == null) {
				throw new ArgumentNullException("record");
			}
			if (record.Fields.Length != 9) {
				throw new FormatException(
					string.Format("President record must contain exactly 9 fields, but given record contains {0}.",
						record.Fields.Length));
			}

			Presidency = ValidateAndGetNumber(record.Fields[0], "presidency");

			Name = ValidateAndGetPersonFullName(record.Fields[1], "name");

			Uri wikiUrl;
			if (!Uri.TryCreate(record.Fields[2], UriKind.RelativeOrAbsolute, out wikiUrl)) {
				throw new FormatException("Wiki link is not a valid URL.");
			}
			WikipediaEntryUrl = wikiUrl;

			TookOffice = ValidateAndGetDateTime(record.Fields[3], "took office date");

			LeftOffice = ValidateAndGetDateTime(record.Fields[4], "left office date");

			Party = ValidateAndGetString(record.Fields[5], "party");

			PortraitImageFilename = ValidateAndGetString(record.Fields[6], "portrait image");

			ThumbnailImageFilename = ValidateAndGetString(record.Fields[7], "thumbnail image");

			HomeState = ValidateAndGetString(record.Fields[8], "home state");
		}

		public int Presidency { get; private set; }

		public PersonFullName Name { get; private set; }

		public Uri WikipediaEntryUrl { get; private set; }

		public DateTime TookOffice { get; private set; }

		public DateTime LeftOffice { get; private set; }

		public string Party { get; private set; }

		public string PortraitImageFilename { get; private set; }

		public string ThumbnailImageFilename { get; private set; }

		public Name HomeState { get; private set; }

		private int ValidateAndGetNumber(string value, string fieldName) {
			int result;
			if (string.IsNullOrWhiteSpace(value)) {
				throw new FormatException(string.Format("Value {0} is missing.", fieldName));
			}
			if (!int.TryParse(value, out result)) {
				throw new FormatException(string.Format("Value {0} is not a valid number.", fieldName));
			}
			return result;
		}

		private DateTime ValidateAndGetDateTime(string value, string fieldName) {
			DateTime result;
			if (string.IsNullOrWhiteSpace(value)) {
				throw new FormatException(string.Format("Value {0} is missing.", fieldName));
			}
			if (!DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result)) {
				throw new FormatException(string.Format("Value {0} is invalid.", fieldName));
			}
			return result;
		}

		private string ValidateAndGetString(string field, string fieldName) {
			if (string.IsNullOrEmpty(field)) {
				throw new FormatException(string.Format("Value {0} is not specified.", fieldName));
			}
			return field;
		}

		private PersonFullName ValidateAndGetPersonFullName(string field, string fieldName) {
			if (string.IsNullOrEmpty(field)) {
				throw new FormatException(string.Format("Value {0} is not specified.", fieldName));
			}
			PersonFullName name;
			if (!PersonFullName.TryParse(field, out name)) {
				throw new FormatException(string.Format("{0} is not a valid person full name.", field));
			}
			return name;
		}
	}
}