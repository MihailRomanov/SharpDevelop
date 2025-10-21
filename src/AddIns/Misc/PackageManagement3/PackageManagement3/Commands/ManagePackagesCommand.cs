using System.Diagnostics;
using ICSharpCode.SharpDevelop;

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
			SD.Workbench.ShowView(new PackageManagement.UI.PackageManagementViewContent());
		}

		#endregion

	}
}
