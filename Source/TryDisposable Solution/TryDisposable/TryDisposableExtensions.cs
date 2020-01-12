using System.Threading.Tasks;

namespace System
{
	/// <summary>
	/// Extensions methods.
	/// </summary>
	public static class TryDisposableExtensions
	{
		/// <summary>
		/// Attempts to dispose an object of the given type.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="item">A concrete instance of the type specified.</param>
		public static void TryDispose<TItem>(this TItem item)
		{
			(item as IDisposable)?.Dispose();
		}

		/// <summary>
		/// Attempts to dispose an object of the given type.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="item">A concrete instance of the type specified.</param>
		public static Task TryDisposeAsync<TItem>(this TItem item)
		{
			(item as IDisposable)?.Dispose();
			return Task.FromResult(0);
		}
	}
}
