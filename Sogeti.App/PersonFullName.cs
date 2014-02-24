using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App {
	public sealed class PersonFullName {
		public PersonFullName(Name surname, params Name[] givenNames) {
			Surname = surname;
			GivenNames = givenNames;
		}

		public Name FirstName {
			get { return GivenNames[0]; }
		}

		public Name[] GivenNames { get; set; }
		public Name Surname { get; set; }

		private static void DoParse(string value, out PersonFullName result, out System.Exception exception) {
			if (string.IsNullOrWhiteSpace(value)) {
				result = null;
				exception = new FormatException("Value is null, empty or whitespace.");
				return;
			}

			string[] parts = value.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

			if (parts.Length < 2) {
				result = null;
				exception = new FormatException("Full name shall have at least 2 names.");
				return;
			}

			var surname = new Name(parts.Last());
			IEnumerable<Name> givenNames = parts.Take(parts.Length - 1).Select(name => new Name(name));
			result = new PersonFullName(surname, givenNames.ToArray());
			exception = null;
		}

		public static PersonFullName Parse(string fullName) {
			PersonFullName result;
			System.Exception exception;
			DoParse(fullName, out result, out exception);
			if (exception != null) {
				throw exception;
			}
			return result;
		}


		public static bool TryParse(string fullName, out PersonFullName result) {
			System.Exception exception;
			DoParse(fullName, out result, out exception);
			return result != null;
		}

		private bool Equals(PersonFullName other) {
			Contract.Assert(other != null);
			return Surname.Equals(other.Surname) && GivenNames.SequenceEqual(other.GivenNames);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			}
			if (ReferenceEquals(this, obj)) {
				return true;
			}
			return obj is PersonFullName && Equals((PersonFullName) obj);
		}

		public override int GetHashCode() {
			unchecked {
				return (FirstName.GetHashCode()*397) ^
				       GivenNames.Aggregate(0, (hashCode, name) => (hashCode << 1) ^ name.GetHashCode());
			}
		}

		public override string ToString() {
			return string.Join(" ", GivenNames.Concat(new[] {Surname}));
		}
	}
}