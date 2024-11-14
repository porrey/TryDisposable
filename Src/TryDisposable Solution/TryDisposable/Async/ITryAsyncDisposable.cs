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
#if (NET5_0 || NET6_0)
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
	public interface ITryAsyncDisposable : IAsyncDisposable
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
	public interface ITryAsyncDisposable<TUnderlyingType> : ITryAsyncDisposable
	{
		/// <summary>
		/// Gets the underlying instance.
		/// </summary>
		TUnderlyingType Instance { get; }
	}
}
#endif