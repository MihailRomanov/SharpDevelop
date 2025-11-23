
using System;
using NuGet.Protocol.Core.Types;
using ReactiveUI;

namespace PackageManagement.UI
{
	/// <summary>
	/// Description of PackageDetailsViewModel.
	/// </summary>
	public class PackageDetailsViewModel : ReactiveObject
	{
		readonly IPackageSearchMetadata packageMetadata;		
		
		public PackageDetailsViewModel(IPackageSearchMetadata packageMetadata)
		{
			this.packageMetadata = packageMetadata;
		}
		
		public string Name {
			get {
				return packageMetadata.Title;
			}
		}
	}
}
