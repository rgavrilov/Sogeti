namespace Sogeti.App {
	public interface IViewFactory {
		IView<T> CreateView<T>(string format);
	}
}