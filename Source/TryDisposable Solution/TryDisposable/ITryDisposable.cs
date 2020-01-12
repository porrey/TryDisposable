namespace System
{
	/// <summary>
	/// <see cref="IDisposable"/> wrapper for objects retrieved from a creation
	/// design pattern such as a factory. If the type interface does
	/// not expose <see cref="IDisposable"/>, but the concrete implementation does,
	/// this wrapper makes it possible to call Dispose() on the object
	/// in a consistent manner. It also allows the class to be used
	/// in a using statement.
	/// </summary>
	public interface ITryDisposable : IDisposable
	{
	}

	/// <summary>
	/// <see cref="IDisposable"/> wrapper for objects retrieved from a creation
	/// design pattern such as a factory. If the type interface does
	/// not expose <see cref="IDisposable"/>, but the concrete implementation does,
	/// this wrapper makes it possible to call Dispose() on the object
	/// in a consistent manner. It also allows the class to be used
	/// in a using statement.
	/// </summary>
	public interface ITryDisposable<TUnderlyingType> : ITryDisposable
	{
		/// <summary>
		/// Gets the underlying instance.
		/// </summary>
		TUnderlyingType Instance { get; }
	}
}