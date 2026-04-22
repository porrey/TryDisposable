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
	public class TryAsyncDisposableTests
	{
		[Fact]
		public void Constructor_WithValidInstance_SetsInstance()
		{
			TrackingAsyncDisposable inner = new();

			TryAsyncDisposable<TrackingAsyncDisposable> wrapper = new(inner);

			Assert.Same(inner, wrapper.Instance);
		}

		[Fact]
		public void Constructor_WithNullInstance_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new TryAsyncDisposable<TrackingAsyncDisposable>(null!));
		}

		[Fact]
		public async Task DisposeAsync_WhenInstanceIsAsyncDisposable_CallsDisposeAsync()
		{
			TrackingAsyncDisposable inner = new();
			TryAsyncDisposable<TrackingAsyncDisposable> wrapper = new(inner);

			await wrapper.DisposeAsync();

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public async Task DisposeAsync_WhenInstanceIsSyncDisposableOnly_CallsDispose()
		{
			TrackingDisposable inner = new();
			TryAsyncDisposable<TrackingDisposable> wrapper = new(inner);

			await wrapper.DisposeAsync();

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public async Task DisposeAsync_WhenInstanceIsNotDisposable_DoesNotThrow()
		{
			NonDisposable inner = new();
			TryAsyncDisposable<NonDisposable> wrapper = new(inner);

			Exception? ex = await Record.ExceptionAsync(() => wrapper.DisposeAsync().AsTask());

			Assert.Null(ex);
		}

		[Fact]
		public async Task DisposeAsync_CalledTwice_OnlyDisposesInstanceOnce()
		{
			TrackingAsyncDisposable inner = new();
			TryAsyncDisposable<TrackingAsyncDisposable> wrapper = new(inner);

			await wrapper.DisposeAsync();
			await wrapper.DisposeAsync();

			Assert.Equal(1, inner.DisposeCount);
		}

		[Fact]
		public async Task DisposeAsync_ThroughInterface_WorksViaAwaitUsingStatement()
		{
			TrackingAsyncDisposable inner = new();

			await using (ITryAsyncDisposable<TrackingAsyncDisposable> wrapper = new TryAsyncDisposable<TrackingAsyncDisposable>(inner))
			{
				_ = wrapper.Instance;
			}

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public async Task DisposeAsync_NonGenericInterface_WorksViaAwaitUsingStatement()
		{
			TrackingAsyncDisposable inner = new();

			await using (ITryAsyncDisposable wrapper = new TryAsyncDisposable<TrackingAsyncDisposable>(inner))
			{
			}

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public async Task StaticDisposeAsync_WhenInstanceIsAsyncDisposable_CallsDisposeAsync()
		{
			TrackingAsyncDisposable inner = new();

			await TryAsyncDisposable<TrackingAsyncDisposable>.DisposeAsync(inner);

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public async Task StaticDisposeAsync_WhenInstanceIsNotDisposable_DoesNotThrow()
		{
			NonDisposable inner = new();

			Exception? ex = await Record.ExceptionAsync(() => TryAsyncDisposable<NonDisposable>.DisposeAsync(inner).AsTask());

			Assert.Null(ex);
		}
	}
}
