﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Sogeti.App {
	public interface IRecordFilter {
		bool ShouldPass(Record record);
	}
}