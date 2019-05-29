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
	/// <typeparam name="T">The interface type of the concrete instance being disposed.</typeparam>
	public class TryDisposable<T> : ITryDisposable<T>
	{
		/// <summary>
		/// Creates an instance of TryDisposable of the specified
		/// typed with the given underlying object instance.
		/// </summary>
		/// <param name="instance">An instance of the object that IDispose
		/// may be implemented on.</param>
		public TryDisposable(T instance)
		{
			if (instance == null) { throw new ArgumentNullException(nameof(instance)); }
			this.Instance = instance;
		}

		/// <summary>
		/// Gets the underlying instance.
		/// </summary>
		public T Instance { get; }

		/// <summary>
		/// Disposes the underlying instance.
		/// </summary>
		public void Dispose()
		{
			TryDisposable<T>.Dispose(this.Instance);
		}

		/// <summary>
		/// Creates an instance of TryDisposable of the specified
		/// typed with the given underlying object instance.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="instance">A concrete instance of the type specified.</param>
		/// <returns>An instance of TryDisposable of the specified
		/// typed with the given underlying object instance.</returns>
		public static ITryDisposable<TItem> Create<TItem>(TItem instance)
		{
			return new TryDisposable<TItem>(instance);
		}

		/// <summary>
		/// Attempts to dispose an object of the given type.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="instance">A concrete instance of the type specified.</param>
		public static void Dispose<TItem>(TItem instance)
		{
			(instance as IDisposable)?.Dispose();
		}
	}
}
