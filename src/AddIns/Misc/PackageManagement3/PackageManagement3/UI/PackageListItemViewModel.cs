
using System;
using NuGet.Protocol.Core.Types;

namespace PackageManagement.UI
{
	/// <summary>
	/// Description of PackageListItemViewModel.
	/// </summary>
	public class SearchPackageListItemViewModel
	{
		readonly IPackageSearchMetadata packageMetadata;

		public SearchPackageListItemViewModel(IPackageSearchMetadata packageMetadata)
		{
			this.packageMetadata = packageMetadata;
		}
	}
}
