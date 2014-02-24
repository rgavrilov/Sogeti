namespace Sogeti.App {
	public interface IView<in T> {
		void Render(T result);
	}
}