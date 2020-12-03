// ***
// *** Copyright(C) 2019-2021, Daniel M. Porrey. All rights reserved.
// *** 
// *** This program is free software: you can redistribute it and/or modify
// *** it under the terms of the GNU Lesser General Public License as published
// *** by the Free Software Foundation, either version 3 of the License, or
// *** (at your option) any later version.
// *** 
// *** This program is distributed in the hope that it will be useful,
// *** but WITHOUT ANY WARRANTY; without even the implied warranty of
// *** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// *** GNU Lesser General Public License for more details.
// *** 
// *** You should have received a copy of the GNU Lesser General Public License
// *** along with this program. If not, see http://www.gnu.org/licenses/.
// ***
#if (NET5_0)
using System.Threading.Tasks;

namespace System
{
	/// <summary>
	/// Provides methods for creating instances of <see cref="ITryDisposable"/>.
	/// </summary>
	public class TryAsyncDisposableFactory
	{
		/// <summary>
		/// Creates an instance of <see cref="TryAsyncDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="instance">A concrete instance of the type specified.</param>
		/// <returns>An instance of <see cref="TryAsyncDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.</returns>
		public static ITryAsyncDisposable<TItem> Create<TItem>(TItem instance)
		{
			return new TryAsyncDisposable<TItem>(instance);
		}

		/// <summary>
		/// Creates an instance of <see cref="TryAsyncDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.
		/// </summary>
		/// <typeparam name="TItem">The interface type of the concrete instance being disposed.</typeparam>
		/// <param name="instance">A concrete instance of the type specified.</param>
		/// <returns>An instance of <see cref="TryAsyncDisposable{TUnderlyingType}"/> of the specified
		/// typed with the given underlying object instance.</returns>
		public static Task<ITryAsyncDisposable<TItem>> CreateAsync<TItem>(TItem instance)
		{
			return Task.FromResult<ITryAsyncDisposable<TItem>>(new TryAsyncDisposable<TItem>(instance));
		}
	}
}
#endif