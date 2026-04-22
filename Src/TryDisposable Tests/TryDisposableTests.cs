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
	public class TryDisposableTests
	{
		[Fact]
		public void Constructor_WithValidInstance_SetsInstance()
		{
			TrackingDisposable inner = new();

			TryDisposable<TrackingDisposable> wrapper = new(inner);

			Assert.Same(inner, wrapper.Instance);
		}

		[Fact]
		public void Constructor_WithNullInstance_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new TryDisposable<TrackingDisposable>(null!));
		}

		[Fact]
		public void Dispose_WhenInstanceIsDisposable_CallsDispose()
		{
			TrackingDisposable inner = new();
			TryDisposable<TrackingDisposable> wrapper = new(inner);

			wrapper.Dispose();

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public void Dispose_WhenInstanceIsNotDisposable_DoesNotThrow()
		{
			NonDisposable inner = new();
			TryDisposable<NonDisposable> wrapper = new(inner);

			Exception? ex = Record.Exception(() => wrapper.Dispose());

			Assert.Null(ex);
		}

		[Fact]
		public void Dispose_CalledTwice_OnlyDisposesInstanceOnce()
		{
			TrackingDisposable inner = new();
			TryDisposable<TrackingDisposable> wrapper = new(inner);

			wrapper.Dispose();
			wrapper.Dispose();

			Assert.Equal(1, inner.DisposeCount);
		}

		[Fact]
		public void Dispose_ThroughInterface_WorksViaUsingStatement()
		{
			TrackingDisposable inner = new();

			using (ITryDisposable<TrackingDisposable> wrapper = new TryDisposable<TrackingDisposable>(inner))
			{
				// use wrapper inside using block
				_ = wrapper.Instance;
			}

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public void Dispose_NonGenericInterface_WorksViaUsingStatement()
		{
			TrackingDisposable inner = new();

			using (ITryDisposable wrapper = new TryDisposable<TrackingDisposable>(inner))
			{
				// access through non-generic interface
			}

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public void StaticDispose_WhenInstanceIsDisposable_CallsDispose()
		{
			TrackingDisposable inner = new();

			TryDisposable<TrackingDisposable>.Dispose(inner);

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public void StaticDispose_WhenInstanceIsNotDisposable_DoesNotThrow()
		{
			NonDisposable inner = new();

			Exception? ex = Record.Exception(() => TryDisposable<NonDisposable>.Dispose(inner));

			Assert.Null(ex);
		}

		[Fact]
		public async Task StaticDisposeAsync_WhenInstanceIsAsyncDisposable_CallsDisposeAsync()
		{
			TrackingAsyncDisposable inner = new();

			await TryDisposable<TrackingAsyncDisposable>.DisposeAsync(inner);

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public async Task StaticDisposeAsync_WhenInstanceIsDisposableOnly_CallsDispose()
		{
			TrackingDisposable inner = new();

			await TryDisposable<TrackingDisposable>.DisposeAsync(inner);

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public async Task StaticDisposeAsync_WhenInstanceIsNotDisposable_DoesNotThrow()
		{
			NonDisposable inner = new();

			Exception? ex = await Record.ExceptionAsync(() => TryDisposable<NonDisposable>.DisposeAsync(inner).AsTask());

			Assert.Null(ex);
		}
	}
}
