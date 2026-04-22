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
	public class TryDisposableExtensionsTests
	{
		[Fact]
		public void TryDispose_OnDisposableObject_CallsDispose()
		{
			TrackingDisposable item = new();

			item.TryDispose();

			Assert.True(item.WasDisposed);
		}

		[Fact]
		public void TryDispose_OnNonDisposableObject_DoesNotThrow()
		{
			NonDisposable item = new();

			Exception? ex = Record.Exception(() => item.TryDispose());

			Assert.Null(ex);
		}

		[Fact]
		public void TryDispose_OnNonDisposableInterface_DoesNotThrow()
		{
			// Interface reference to a disposable concrete type
			INonDisposableInterface item = new TrackingDisposable();

			Exception? ex = Record.Exception(() => item.TryDispose());

			Assert.Null(ex);
		}

		[Fact]
		public void TryDispose_OnNonDisposableInterfaceWithDisposable_StillDisposes()
		{
			// The underlying concrete type implements IDisposable even though the interface does not
			TrackingDisposable concrete = new();
			INonDisposableInterface item = concrete;

			item.TryDispose();

			Assert.True(concrete.WasDisposed);
		}

		[Fact]
		public void TryDispose_OnPlainObject_DoesNotThrow()
		{
			object obj = new();

			Exception? ex = Record.Exception(() => obj.TryDispose());

			Assert.Null(ex);
		}
	}
}
