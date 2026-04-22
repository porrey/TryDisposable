// Copyright(C) 2017-2026, Daniel M. Porrey. All rights reserved.
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
using System;

namespace TryDisposable_Tests
{
	/// <summary>
	/// An interface that intentionally does not extend IDisposable or IAsyncDisposable.
	/// Simulates the scenario where an interface hides the disposability of its concrete implementation.
	/// </summary>
	public interface INonDisposableInterface
	{
	}

	/// <summary>
	/// A concrete class that implements IDisposable and tracks calls to Dispose().
	/// </summary>
	public sealed class TrackingDisposable : INonDisposableInterface, IDisposable
	{
		public bool WasDisposed => this.DisposeCount > 0;
		public int DisposeCount { get; private set; }

		public void Dispose()
		{
			this.DisposeCount++;
		}
	}

	/// <summary>
	/// A concrete class that implements IAsyncDisposable and tracks calls to DisposeAsync().
	/// </summary>
	public sealed class TrackingAsyncDisposable : INonDisposableInterface, IAsyncDisposable
	{
		public bool WasDisposed => this.DisposeCount > 0;
		public int DisposeCount { get; private set; }

		public ValueTask DisposeAsync()
		{
			this.DisposeCount++;
			return ValueTask.CompletedTask;
		}
	}

	/// <summary>
	/// A concrete class that implements both IDisposable and IAsyncDisposable.
	/// Tracks which path was taken during disposal.
	/// </summary>
	public sealed class BothDisposable : IDisposable, IAsyncDisposable
	{
		public bool SyncDisposeWasCalled { get; private set; }
		public bool AsyncDisposeWasCalled { get; private set; }

		public void Dispose()
		{
			this.SyncDisposeWasCalled = true;
		}

		public ValueTask DisposeAsync()
		{
			this.AsyncDisposeWasCalled = true;
			return ValueTask.CompletedTask;
		}
	}

	/// <summary>
	/// A concrete class that does not implement any disposal interface.
	/// </summary>
	public sealed class NonDisposable : INonDisposableInterface
	{
	}
}
