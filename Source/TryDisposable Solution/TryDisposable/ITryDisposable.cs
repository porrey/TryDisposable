namespace System
{
	/// <summary>
	/// IDisposable wrapper for objects retrieved from a creation
	/// design pattern such as a factory. If the type interface does
	/// not expose IDisposable, but the concrete implementation does,
	/// this wrapper makes it possible to call IDispose the object
	/// in a consistent manner. It also allows the class to be used
	/// in a using statement.
	/// </summary>
	public interface ITryDisposable<T> : IDisposable
	{
		/// <summary>
		/// Gets the underlying instance.
		/// </summary>
		T Instance { get; }
	}
}