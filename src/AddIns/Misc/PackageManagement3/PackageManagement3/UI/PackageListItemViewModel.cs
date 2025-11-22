
using System;
using NuGet.Protocol.Core.Types;

namespace PackageManagement.UI
{
	/// <summary>
	/// Description of PackageListItemViewModel.
	/// </summary>
	public class PackageListItemViewModel
	{
		readonly IPackageSearchMetadata packageMetadata;

		public PackageListItemViewModel(IPackageSearchMetadata packageMetadata)
		{
			this.packageMetadata = packageMetadata;
		}
		
		public string Title {
			get {
				return packageMetadata.Title;
			}
		}
		
		public string Description {
			get {
				return packageMetadata.Description;
			}
		}
	}
}
