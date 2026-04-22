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
	public class TryAsyncDisposableFactoryTests
	{
		[Fact]
		public void Create_WithValidInstance_ReturnsWrapper()
		{
			TrackingAsyncDisposable inner = new();

			ITryAsyncDisposable<TrackingAsyncDisposable> wrapper = TryAsyncDisposableFactory.Create(inner);

			Assert.NotNull(wrapper);
			Assert.Same(inner, wrapper.Instance);
		}

		[Fact]
		public void Create_WithNullInstance_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => TryAsyncDisposableFactory.Create<TrackingAsyncDisposable>(null!));
		}

		[Fact]
		public async Task Create_UsedInAwaitUsingBlock_DisposesInstance()
		{
			TrackingAsyncDisposable inner = new();

			await using (TryAsyncDisposableFactory.Create(inner))
			{
			}

			Assert.True(inner.WasDisposed);
		}

		[Fact]
		public async Task CreateAsync_WithValidInstance_ReturnsWrapper()
		{
			TrackingAsyncDisposable inner = new();

			ITryAsyncDisposable<TrackingAsyncDisposable> wrapper = await TryAsyncDisposableFactory.CreateAsync(inner);

			Assert.NotNull(wrapper);
			Assert.Same(inner, wrapper.Instance);
		}

		[Fact]
		public async Task CreateAsync_WithNullInstance_ThrowsArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(() => TryAsyncDisposableFactory.CreateAsync<TrackingAsyncDisposable>(null!));
		}

		[Fact]
		public async Task CreateAsync_UsedInAwaitUsingBlock_DisposesInstance()
		{
			TrackingAsyncDisposable inner = new();

			await using (await TryAsyncDisposableFactory.CreateAsync(inner))
			{
			}

			Assert.True(inner.WasDisposed);
		}
	}
}
