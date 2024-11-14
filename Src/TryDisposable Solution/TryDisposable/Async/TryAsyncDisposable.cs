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
#if (NET5_0 || NET6_0)
using System.Threading.Tasks;

namespace System
{
	/// <summary>
	/// <see cref="IAsyncDisposable"/> wrapper for objects retrieved from a creation
	/// design pattern such as a factory. If the type interface does
	/// not expose <see cref="IAsyncDisposable"/>, but the concrete implementation does,
	/// this wrapper makes it possible to call Dispose() on the object
	/// in a consistent manner. It also allows the class to be used
	/// in a using statement.
	/// </summary>
	/// <typeparam name="TUnderlyingType">The interface type of the concrete instance being disposed.</typeparam>
	public class TryAsyncDisposable<TUnderlyingType> : ITryAsyncDisposable<TUnderlyingType>
	{
		/// <summary>
		/// Creates an instance of <see cref="TryAsyncDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.
		/// </summary>
		/// <param name="instance">An instance of the object that <see cref="IAsyncDisposable"/>
		/// may be implemented on.</param>
		public TryAsyncDisposable(TUnderlyingType instance)
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
		public ValueTask DisposeAsync()
		{
			return this.Instance.TryDisposeAsync();
		}

		/// <summary>
		/// Attempts to dispose an object of the given type.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="instance">A concrete instance of the type specified.</param>
		public static ValueTask DisposeAsync<TItem>(TItem instance)
		{
			return instance.TryDisposeAsync();
		}
	}
}
#endif