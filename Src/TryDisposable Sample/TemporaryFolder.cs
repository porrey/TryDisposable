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
using System;

#pragma warning disable DF0100

namespace ConsoleApp1
{
	public interface ITemporaryFolder
	{
		string Path { get; set; }
	}

	public class TemporaryFolder1: DisposableObject, ITemporaryFolder
	{
		public string Path { get; set; }

		protected override void OnDisposeManagedObjects()
		{
			base.OnDisposeManagedObjects();
		}
	}

	public class TemporaryFolder2 : ITemporaryFolder
	{
		public string Path { get; set; }
	}

	public static class TemporaryFolderFactory
	{
		public static ITemporaryFolder Create1()
		{
			return new TemporaryFolder1();
		}

		public static ITemporaryFolder Create2()
		{
			return new TemporaryFolder2();
		}
	}
}

#pragma warning restore DF0100