using System;
using NuGet.Protocol.Core.Types;
using ReactiveUI;

namespace PackageManagement.UI
{
	/// <summary>
	/// Description of PackageListItemViewModel.
	/// </summary>
	public class PackageListItemViewModel : ReactiveObject
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
		
		internal IPackageSearchMetadata PackageMetadata {
			get {
				return packageMetadata;
			}
		}
	}
}
