// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using Microsoft.Win32;

namespace ICSharpCode.SharpDevelop
{
	public static class DotnetDetection
	{
		/// <summary>
		/// Gets whether .NET 3.5 is installed and has at least SP1.  
		/// </summary>
		public static bool IsDotnet35SP1Installed()
		{
			using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5")) {
				return key != null && (key.GetValue("SP") as int?) >= 1;
			}
		}

		/// <summary>
		/// Gets whether any .NET 4.x runtime is installed.
		/// </summary>		
		public static bool IsDotnet40Installed()
		{
			return true; // required for SD to run
		}
		
		/// <summary>
		/// Gets whether the .NET 4.5 runtime (or a later version of .NET 4.x) is installed.
		/// </summary>
		public static bool IsDotnet45Installed()
		{
			return GetDotnet4Release() >= 378389;
		}
		
		/// <summary>
		/// Gets whether the .NET 4.5.1 runtime (or a later version of .NET 4.x) is installed.
		/// </summary>
		public static bool IsDotnet451Installed()
		{
			// According to: http://blogs.msdn.com/b/astebner/archive/2013/11/11/10466402.aspx
			// 378675 is .NET 4.5.1 on Win8
			// 378758 is .NET 4.5.1 on Win7
			return GetDotnet4Release() >= 378675;
		}
		
		public static bool IsDotnet452Installed()
		{
			// 379893 is .NET 4.5.2 on my Win7 machine
			return GetDotnet4Release() >= 379893;
		}
		
		public static bool IsDotnet46Installed()
		{
			// 393295 - On Windows 10
			// 393297 - On all other Windows operating systems
			return GetDotnet4Release() >= 393295;
		}

		public static bool IsDotnet461Installed()
		{
			// 394254 - On Windows 10 November Update systems
			// 394271 - On all other Windows operating systems (including Windows 10)
			return GetDotnet4Release() >= 394254;
		}
		
		public static bool IsDotnet462Installed()
		{
			// 394802 - On Windows 10 Anniversary Update and Windows Server 2016
			// 394806 - On all other Windows operating systems (including other Windows 10 operating systems)
			return GetDotnet4Release() >= 394802;
		}
		
		public static bool IsDotnet47Installed()
		{
			// 460798 - On Windows 10 Creators Update
			// 460805- On all other Windows operating systems (including other Windows 10 operating systems)
			return GetDotnet4Release() >= 460798;
		}
		
		public static bool IsDotnet471Installed()
		{
			// 461308 - On Windows 10 Fall Creators Update and Windows Server, version 1709
			// 461310 - On all other Windows operating systems (including other Windows 10 operating systems)
			return GetDotnet4Release() >= 461308;
		}
		
		public static bool IsDotnet472Installed()
		{
			// 461808 - On Windows 10 April 2018 Update and Windows Server, version 1803
			// 461814 - On all Windows operating systems other than Windows 10 April 2018 Update and Windows Server, version 1803
			return GetDotnet4Release() >= 461808;
		}
		
		public static bool IsDotnet48Installed()
		{
			// 528040 - On Windows 10 May 2019 Update and Windows 10 November 2019 Update
			// 528049 - On all other Windows operating systems (including other Windows 10 operating systems)
			// 528372 - On Windows 10 May 2020 Update, October 2020 Update, May 2021 Update, November 2021 Update, and 2022 Update
			// 528449 - On Windows 11 and Windows Server 2022
			return GetDotnet4Release() >= 528040;
		}
		
		public static bool IsDotnet481Installed()
		{
			// 533320 - On Windows 11 2022 Update
			// 533325 - All other Windows operating systems
			return GetDotnet4Release() >= 533320;
		}
		
		/// <summary>
		/// Gets the .NET 4.x release number.
		/// The numbers are documented on http://msdn.microsoft.com/en-us/library/hh925568.aspx
		/// </summary>
		static int? GetDotnet4Release()
		{
			using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full")) {
				if (key != null)
					return key.GetValue("Release") as int?;
			}
			return null;
		}
		
		/// <summary>
		/// Gets whether the Microsoft Build Tools 2013 (MSBuild 12.0) is installed.
		/// </summary>
		public static bool IsBuildTools2013Installed()
		{
			// HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\DevDiv\BuildTools\Servicing\12.0
			using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\DevDiv\BuildTools\Servicing\12.0\MSBuild")) {
				return key != null && key.GetValue("Install") as int? >= 1;
			}
		}
		
		/// <summary>
		/// Gets whether the Microsoft Build Tools 2015 (MSBuild 14.0) is installed.
		/// </summary>
		public static bool IsBuildTools2015Installed()
		{
			// HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\DevDiv\BuildTools\Servicing\14.0
			using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\DevDiv\BuildTools\Servicing\14.0\MSBuild")) {
				return key != null && key.GetValue("Install") as int? >= 1;
			}
		}
	}
}
