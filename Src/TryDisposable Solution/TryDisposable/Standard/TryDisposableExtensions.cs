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

#if (!NET5_0 && !NET6_0)
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
#endif
	}
}
