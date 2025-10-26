
using System;
using NuGet.Configuration;

namespace PackageManagement.Services
{
	/// <summary>
	/// Description of IPackageManagemrntService.
	/// </summary>
	public interface INuGetManagementService
	{
		IPackageSourceProvider GetPackageSourceProvider();
	}
}
