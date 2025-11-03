
using System;
using NuGet.Configuration;

namespace PackageManagement.UI
{
	/// <summary>
	/// Description of PackageSourceViewModel.
	/// </summary>
	public class PackageSourceViewModel
	{
		private readonly PackageSource packageSource;

		public PackageSourceViewModel(PackageSource packageSource)
		{
			this.packageSource = packageSource;
		}
		
		public string Name
		{
			get {
				return packageSource.Name;
			}
		}
		
		public string Url
		{
			get {
				return packageSource.Source;
			}
		}
	}
}
