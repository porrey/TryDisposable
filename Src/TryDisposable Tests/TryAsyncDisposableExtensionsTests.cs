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
	public class TryAsyncDisposableExtensionsTests
	{
		[Fact]
		public async Task TryDisposeAsync_OnAsyncDisposableObject_CallsDisposeAsync()
		{
			TrackingAsyncDisposable item = new();

			await item.TryDisposeAsync();

			Assert.True(item.WasDisposed);
		}

		[Fact]
		public async Task TryDisposeAsync_OnDisposableOnlyObject_CallsDispose()
		{
			TrackingDisposable item = new();

			await item.TryDisposeAsync();

			Assert.True(item.WasDisposed);
		}

		[Fact]
		public async Task TryDisposeAsync_OnNonDisposableObject_DoesNotThrow()
		{
			NonDisposable item = new();

			Exception? ex = await Record.ExceptionAsync(() => item.TryDisposeAsync().AsTask());

			Assert.Null(ex);
		}

		[Fact]
		public async Task TryDisposeAsync_OnNonDisposableInterfaceWithAsyncDisposable_StillDisposes()
		{
			TrackingAsyncDisposable concrete = new();
			INonDisposableInterface item = concrete;

			await item.TryDisposeAsync();

			Assert.True(concrete.WasDisposed);
		}

		[Fact]
		public async Task TryDisposeAsync_OnNonDisposableInterfaceWithSyncDisposable_StillDisposes()
		{
			TrackingDisposable concrete = new();
			INonDisposableInterface item = concrete;

			await item.TryDisposeAsync();

			Assert.True(concrete.WasDisposed);
		}

		[Fact]
		public async Task TryDisposeAsync_PrefersAsyncDisposableOverSyncDisposable()
		{
			// A type that implements both IAsyncDisposable and IDisposable
			BothDisposable item = new();

			await item.TryDisposeAsync();

			// Should use async path
			Assert.True(item.AsyncDisposeWasCalled);
			Assert.False(item.SyncDisposeWasCalled);
		}

		[Fact]
		public async Task TryDisposeAsync_OnPlainObject_DoesNotThrow()
		{
			object obj = new();

			Exception? ex = await Record.ExceptionAsync(() => obj.TryDisposeAsync().AsTask());

			Assert.Null(ex);
		}

		[Fact]
		public async Task TryDisposeAsync_OnNonDisposable_ReturnsCompletedTask()
		{
			NonDisposable item = new();

			ValueTask result = item.TryDisposeAsync();

			Assert.True(result.IsCompleted);
			await result;
		}
	}
}
