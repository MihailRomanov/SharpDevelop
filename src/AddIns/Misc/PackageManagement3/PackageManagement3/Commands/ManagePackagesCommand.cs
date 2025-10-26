using System.Diagnostics;
using ICSharpCode.SharpDevelop;
using PackageManagement.Services;
using PackageManagement.UI;

namespace PackageManagement.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class ManagePackagesCommand : SimpleCommand
	{
		#region implemented abstract members of SimpleCommand

		public override void Execute(object parameter)
		{
			Debug.WriteLine("ManagePackagesCommand");
			var nugetService = SD.Services.GetRequiredService<INuGetManagementService>();
			var packageManagementViewContent = new PackageManagementViewContent(nugetService);
			
			SD.Workbench.ShowView(packageManagementViewContent);
		}

		#endregion

	}
}
