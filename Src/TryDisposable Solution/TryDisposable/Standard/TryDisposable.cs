//
// Copyright(C) 2019-2025, Daniel M. Porrey. All rights reserved.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see http://www.gnu.org/licenses/.
// 
using System.Threading.Tasks;

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
	/// <typeparam name="TUnderlyingType">The interface type of the concrete instance being disposed.</typeparam>
	public class TryDisposable<TUnderlyingType> : ITryDisposable<TUnderlyingType>
	{
		/// <summary>
		/// Creates an instance of <see cref="TryDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.
		/// </summary>
		/// <param name="instance">An instance of the object that <see cref="IDisposable"/>
		/// may be implemented on.</param>
		public TryDisposable(TUnderlyingType instance)
		{
			if (instance == null)
			{ throw new ArgumentNullException(nameof(instance)); }
			this.Instance = instance;
		}

		/// <summary>
		/// Gets the underlying instance.
		/// </summary>
		public TUnderlyingType Instance { get; }

		/// <summary>
		/// Disposes the underlying instance.
		/// </summary>
		public void Dispose()
		{
			this.Instance.TryDispose();
		}

#if (!NET5_0 && !NET6_0)
		/// <summary>
		/// Creates an instance of <see cref="TryDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="instance">A concrete instance of the type specified.</param>
		/// <returns>An instance of <see cref="TryDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.</returns>
		[Obsolete("Use System.TryDisposableFactory.Create() method instead.")]
		public static ITryDisposable<TItem> Create<TItem>(TItem instance)
		{
			return new TryDisposable<TItem>(instance);
		}
#endif

		/// <summary>
		/// Attempts to dispose an object of the given type.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="instance">A concrete instance of the type specified.</param>
		public static void Dispose<TItem>(TItem instance)
		{
			instance.TryDispose();
		}

#if (NET5_0 || NET6_0)
		/// <summary>
		/// Attempts to dispose an object of the given type.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="instance">A concrete instance of the type specified.</param>
		public static ValueTask DisposeAsync<TItem>(TItem instance)
		{
			return instance.TryDisposeAsync();
		}
#else
		/// <summary>
		/// Attempts to dispose an object of the given type.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="instance">A concrete instance of the type specified.</param>
		public static Task DisposeAsync<TItem>(TItem instance)
		{
			return instance.TryDisposeAsync();
		}
#endif
	}
}
