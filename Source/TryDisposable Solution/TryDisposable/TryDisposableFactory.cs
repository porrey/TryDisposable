﻿using System.Threading.Tasks;

namespace System
{
	/// <summary>
	/// Provides methods for creating instances of <see cref="ITryDisposable"/>.
	/// </summary>
	public class TryDisposableFactory
	{
		/// <summary>
		/// Creates an instance of <see cref="TryDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="instance">A concrete instance of the type specified.</param>
		/// <returns>An instance of <see cref="TryDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.</returns>
		public static ITryDisposable<TItem> Create<TItem>(TItem instance)
		{
			return new TryDisposable<TItem>(instance);
		}

		/// <summary>
		/// Creates an instance of <see cref="TryDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="instance">A concrete instance of the type specified.</param>
		/// <returns>An instance of <see cref="TryDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.</returns>
		public static Task<ITryDisposable<TItem>> CreateAsync<TItem>(TItem instance)
		{
			return Task.FromResult<ITryDisposable<TItem>>(new TryDisposable<TItem>(instance));
		}
	}
}
