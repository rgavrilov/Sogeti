using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Ninject;

namespace Sogeti.App {
	public class ViewFactory : IViewFactory {
		private readonly IKernel _kernel;

		private readonly Dictionary<Type, IDictionary<string, Type>> _views =
			new Dictionary<Type, IDictionary<string, Type>>();

		public ViewFactory(IKernel kernel) {
			_kernel = kernel;
		}

		public IView<T> CreateView<T>(string format) {
			Type viewType = _views[typeof (T)][format];
			return (IView<T>) _kernel.Get(viewType);
		}

		public void RegisterView(string format, Type resultType, Type viewType) {
			if (!_views.ContainsKey(resultType)) {
				_views.Add(resultType, new Dictionary<string, Type>());
			}
			_views[resultType].Add(format, viewType);
		}
	}
}