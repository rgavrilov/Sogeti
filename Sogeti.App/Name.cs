using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using JetBrains.Annotations;

namespace Sogeti.App {
	public sealed class Name {
		[NotNull] private readonly string _name;

		public Name(string name) {
			Contract.Assert(name != null);
			_name = name;
		}

		public override string ToString() {
			return _name;
		}

		private bool Equals(Name other) {
			return string.Equals(_name, other._name, StringComparison.OrdinalIgnoreCase);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			}
			if (ReferenceEquals(this, obj)) {
				return true;
			}
			return obj is Name && Equals((Name) obj);
		}

		public override int GetHashCode() {
			return _name.GetHashCode();
		}

		public static implicit operator Name(string name) {
			Contract.Assert(!string.IsNullOrWhiteSpace(name));
			return new Name(name);
		}
	}
}