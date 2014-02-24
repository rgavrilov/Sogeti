using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App {
	public class Record {
		public Record(IEnumerable<string> fields) {
			Fields = fields.ToArray();
		}

		public Record(params string[] fields)
			: this((IEnumerable<string>) fields) {}

		public string this[int index] {
			get { return Fields[index]; }
		}

		public string[] Fields { get; private set; }

		public override string ToString() {
			return string.Join(", ", Fields);
		}

		protected bool Equals(Record other) {
			return this.Fields.SequenceEqual(other.Fields);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			}
			if (ReferenceEquals(this, obj)) {
				return true;
			}
			if (obj.GetType() != this.GetType()) {
				return false;
			}
			return Equals((Record) obj);
		}

		public override int GetHashCode() {
			return Fields.Aggregate(397, (hashCode, field) => hashCode << 1 + field.GetHashCode());
		}
	}
}