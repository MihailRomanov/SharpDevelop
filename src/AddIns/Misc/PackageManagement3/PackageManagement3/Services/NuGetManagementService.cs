using System;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace PackageManagement.Services
{
	/// <summary>
	/// Description of PackageManagementService.
	/// </summary>
	public class NuGetManagementService : INuGetManagementService
	{
		public SourceRepository GetSourceRepository(PackageSource packageSource)
		{
			return Repository.Factory.GetCoreV3(packageSource);
		}

		public IPackageSourceProvider GetPackageSourceProvider()
		{
			var settings = Settings.LoadDefaultSettings(null);
			var packageSourcesProvider = new PackageSourceProvider(settings);
			
			return packageSourcesProvider;
		}		
	}
}
