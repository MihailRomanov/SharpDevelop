
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using NuGet.Configuration;
using ReactiveUI;
using System.Linq;

namespace PackageManagement.UI
{
	/// <summary>
	/// Description of PackageManagementViewModel.
	/// </summary>
	public class PackageManagementViewModel : ReactiveObject
	{
		private readonly IPackageSourceProvider packageSourceProvider;
		
		private readonly ObservableAsPropertyHelper<IEnumerable<PackageSourceViewModel>> packageSources;
		
		public IEnumerable<PackageSourceViewModel> PackageSources {
			get {
				return packageSources.Value;
			}
		}

		public PackageManagementViewModel(IPackageSourceProvider packageSourceProvider)
		{
			this.packageSourceProvider = packageSourceProvider;
			
			packageSources = Observable
				.FromEvent<EventHandler, IEnumerable<PackageSource>>(
					handler => (sender, e) => handler(packageSourceProvider.LoadPackageSources()),
					handler => packageSourceProvider.PackageSourcesChanged += handler,
					handler => packageSourceProvider.PackageSourcesChanged -= handler)
				.StartWith(packageSourceProvider.LoadPackageSources())
				.Select(t => t.Where(x => x.IsEnabled).Select(x => new PackageSourceViewModel(x)))
				.ToProperty(this, t => t.PackageSources);
		}
	}
}
