using System;
using NuGet.Configuration;
using NuGet.Protocol.Core.Types;

namespace PackageManagement.Services
{
	/// <summary>
	/// Description of IPackageManagemrntService.
	/// </summary>
	public interface INuGetManagementService
	{
		IPackageSourceProvider GetPackageSourceProvider();
		
		SourceRepository GetSourceRepository(PackageSource packageSource);
	}
}
