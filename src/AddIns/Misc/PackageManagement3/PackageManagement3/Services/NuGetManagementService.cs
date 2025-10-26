
using System;
using NuGet.Configuration;

namespace PackageManagement.Services
{
	/// <summary>
	/// Description of PackageManagementService.
	/// </summary>
	public class NuGetManagementService : INuGetManagementService
	{
		#region INuGetManagementService implementation
		public IPackageSourceProvider GetPackageSourceProvider()
		{
			var settings = Settings.LoadDefaultSettings(null);
			var packageSourcesProvider = new PackageSourceProvider(settings);
			
			return packageSourcesProvider;
		}
		#endregion
		
	}
}
