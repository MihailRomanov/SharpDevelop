using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using PackageManagement.Services;
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
		private readonly INuGetManagementService nugetService;
		
		private readonly ObservableAsPropertyHelper<IEnumerable<PackageSourceViewModel>> packageSources;
		public IEnumerable<PackageSourceViewModel> PackageSources {
			get {
				return packageSources.Value;
			}
		}
		
		private PackageSourceViewModel currentPackageSource;
		public PackageSourceViewModel CurrentPackageSource {
			get {
				return currentPackageSource;
			}
			set {
				this.RaiseAndSetIfChanged(ref currentPackageSource, value);
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
		
		private readonly ObservableAsPropertyHelper<IEnumerable<PackageListItemViewModel>> searchResults;
		public IEnumerable<PackageListItemViewModel> SearchResults {
			get {
				return searchResults.Value;
			}
		}
		
		private PackageListItemViewModel currentPackageListItem;
		public PackageListItemViewModel CurrentPackagListItem{
			get {
				return currentPackageListItem;
			}
			set {
				this.RaiseAndSetIfChanged(ref currentPackageListItem, value);
			}
		}
		
		
		private ObservableAsPropertyHelper<PackageDetailsViewModel> currentPackageDetails;
		public PackageDetailsViewModel CurrentPackageDetails{
			get {
				return currentPackageDetails.Value;
			}
		}
		
		
		public PackageManagementViewModel(INuGetManagementService nugetService)
		{
			this.nugetService = nugetService;
			packageSourceProvider = nugetService.GetPackageSourceProvider();
						
			packageSources = Observable
				.FromEvent<EventHandler, IEnumerable<PackageSource>>(
					handler => (sender, e) => handler(packageSourceProvider.LoadPackageSources()),
					handler => packageSourceProvider.PackageSourcesChanged += handler,
					handler => packageSourceProvider.PackageSourcesChanged -= handler)
				.StartWith(packageSourceProvider.LoadPackageSources())
				.Select(t => t.Where(x => x.IsEnabled).Select(x => new PackageSourceViewModel(x)))
				.ToProperty(this, t => t.PackageSources);
			
			searchResults = this
				.WhenAnyValue(x => x.SearchString)
				.Throttle(TimeSpan.FromMilliseconds(800))
				.Select(s => s?.Trim())
				.DistinctUntilChanged()
				.Where(s => !string.IsNullOrWhiteSpace(s))
				.SelectMany(SearchPackagesAsync)
				.ObserveOn(RxApp.MainThreadScheduler)
				.ToProperty(this, t => t.SearchResults);
			
			// TODO Разобраться с ошибкой - почему прерывается работа если ошибка
			searchResults.ThrownExceptions.Subscribe(e => Debug.WriteLine("Error: " + e.Message));
			
			currentPackageDetails = this
				.WhenAnyValue(x => x.CurrentPackagListItem)
				.Where(t => t != null)
				.SelectMany(GetPackageDetailsAsync)
				//.ObserveOn(RxApp.MainThreadScheduler)
				.ToProperty(this, x => x.CurrentPackageDetails);
			
			this.PropertyChanging += (sender, e) => Debug.WriteLine("Changing: " + e.PropertyName);
		}
				
		private async Task<IEnumerable<PackageListItemViewModel>> SearchPackagesAsync(
			string query, CancellationToken token)
		{
			Debug.WriteLine("SearchPackages: " + query);
			if (CurrentPackageSource == null)
				return Enumerable.Empty<PackageListItemViewModel>();
			
			var packageSource = packageSourceProvider.GetPackageSourceByName(CurrentPackageSource.Name);
			var sourceRepository = nugetService.GetSourceRepository(packageSource);
			
			var searchResource = sourceRepository.GetResource<PackageSearchResource>();
			var searchResult = await searchResource.SearchAsync(
				query, 
				new SearchFilter(false),
				0, 100, new NuGet.Common.NullLogger(), token);
			
			Debug.WriteLine("Search result: " + searchResult.Count());
			return searchResult.Select(x => new PackageListItemViewModel(x));
		}
		
		private async Task<PackageDetailsViewModel> GetPackageDetailsAsync(
			PackageListItemViewModel listItem, CancellationToken token)
		{
			return new PackageDetailsViewModel(listItem.PackageMetadata);
		}
	}
}
