
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NuGet.Configuration;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol;
using ReactiveUI;
using System.Linq;
using System.Threading;

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

		private string searchString;
		public string SearchString {
			get {
				return searchString;
			}
			set {
				this.RaiseAndSetIfChanged(ref searchString, value);
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
		
		private async Task<IEnumerable<SearchPackageListItemViewModel>> SearchPackages(
			string packageSourceName, string query, CancellationToken token)
		{
			var packageSource = packageSourceProvider.GetPackageSourceByName(packageSourceName);
			var sourceRepository = Repository.Factory.GetCoreV3(packageSource);
			
			var searchResource = sourceRepository.GetResource<PackageSearchResource>();
			
			var searchResult = await searchResource.SearchAsync(
				query, 
				new SearchFilter(false),
				0, 100, new NuGet.Common.NullLogger(), token).ConfigureAwait(false);
						
			return searchResult.Select(x => new SearchPackageListItemViewModel(x));
		}
	}
}
