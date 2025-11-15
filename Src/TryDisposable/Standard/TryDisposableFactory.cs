//
// Copyright(C) 2017-2025, Daniel M. Porrey. All rights reserved.
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
