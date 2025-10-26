using System;
using ICSharpCode.SharpDevelop.Workbench;
using PackageManagement.Services;

namespace PackageManagement.UI
{
	/// <summary>
	/// Description of the view content
	/// </summary>
	public class PackageManagementViewContent : AbstractViewContent
	{
		readonly PackageManagementView packageManagementView;
		
		public PackageManagementViewContent(INuGetManagementService nugetService)
		{
			var packageSourceProvider = nugetService.GetPackageSourceProvider();
			
			var viewModel = new PackageManagementViewModel(packageSourceProvider);
			packageManagementView = new PackageManagementView 
			{
				ViewModel = viewModel,
			};
		}
		
		#region implemented abstract members of AbstractViewContent
		public override object Control {
			get {
				return packageManagementView;
			}
		}
		#endregion
	}
}
