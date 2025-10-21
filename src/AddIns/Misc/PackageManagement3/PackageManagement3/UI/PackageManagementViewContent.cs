using System;
using ICSharpCode.SharpDevelop.Workbench;

namespace PackageManagement.UI
{
	/// <summary>
	/// Description of the view content
	/// </summary>
	public class PackageManagementViewContent : AbstractViewContent
	{
		readonly PackageManagementView packageManagementView = new PackageManagementView {
			ViewModel = new PackageManagementViewModel()
		};
		
		#region implemented abstract members of AbstractViewContent
		public override object Control {
			get {
				return packageManagementView;
			}
		}
		#endregion
	}
}
